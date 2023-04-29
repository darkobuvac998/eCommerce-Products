namespace eCommerce.Products.Domain.Exceptions;

public abstract class BaseException : Exception
{
    protected readonly string _code;
    protected readonly string _message;

    protected BaseException(string code, string message)
        : base($"ERORR: {code} \n Message: {message}")
    {
        _code = code;
        _message = message;
    }

    protected BaseException(string code, string message, Exception inner)
        : base($"ERORR: {code} \n Message: {message}", inner)
    {
        _code = code;
        _message = message;
    }
}
