namespace eCommerce.Products.Domain.Shared;

public class Error
{
    private readonly string _errorCode = string.Empty;
    private readonly string _errorMessage = string.Empty;

    public string ErrorMessage => $"{_errorCode}: {_errorMessage}";

    public Error(string code, string message) => (_errorCode, _errorMessage) = (code, message);
}
