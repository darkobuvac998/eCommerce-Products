namespace eCommerce.Products.Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError =
        new(ErrorCodes.ValidationError, "A validation problem occured");

    ICollection<Error> Errors { get; }
}
