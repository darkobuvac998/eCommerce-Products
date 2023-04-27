namespace eCommerce.Products.Domain.Shared;

public sealed class ValidationResult : IValidationResult
{
    public ICollection<Error> Errors { get; }

    private ValidationResult(Error[] errors) => Errors = errors;

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
