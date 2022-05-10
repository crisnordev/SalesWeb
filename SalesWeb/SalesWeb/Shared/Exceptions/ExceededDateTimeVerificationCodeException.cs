namespace SalesWeb.Shared.Exceptions;

public class ExceededDateTimeVerificationCodeException : Exception
{
    public ExceededDateTimeVerificationCodeException(string message) : base(message)
    {
    }
}