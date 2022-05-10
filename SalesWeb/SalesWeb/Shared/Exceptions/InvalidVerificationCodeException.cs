namespace SalesWeb.Shared.Exceptions;

public class InvalidVerificationCodeException : Exception
{
    public InvalidVerificationCodeException(string message) : base(message)
    {
    }
}