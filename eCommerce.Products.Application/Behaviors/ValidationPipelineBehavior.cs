using FluentValidation;
using MediatR;
using Newtonsoft.Json.Linq;

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
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) =>
                    new { Key = propertyName, Values = errorMessages.Distinct().ToArray() }
            )
            .ToDictionary(x => x.Key, x => x.Values);

        if (errors.Any())
        {
            throw new ValidationException(JObject.FromObject(errors).ToString());
        }

        return await next();
    }
}
