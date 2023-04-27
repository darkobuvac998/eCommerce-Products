using eCommerce.Products.Domain.Shared;
using FluentValidation;
using MediatR;

namespace eCommerce.Products.Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResposne>
    : IPipelineBehavior<TRequest, TResposne>
    where TRequest : IRequest<TResposne>
    where TResposne : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) =>
        _validators = validators;

    public async Task<TResposne> Handle(
        TRequest request,
        RequestHandlerDelegate<TResposne> next,
        CancellationToken cancellationToken
    )
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationTasks = await Task.WhenAll(
            _validators.Select(
                async validator => await validator.ValidateAsync(request, cancellationToken)
            )
        );

        var errors = validationTasks
            .SelectMany(validationResult => validationResult.Errors)
            .Where(x => x is not null)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Any())
        {
            return CreateValidationResult<TResposne>(errors);
        }

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : class
    {
        object validationResult = typeof(ValidationResult)
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
}
