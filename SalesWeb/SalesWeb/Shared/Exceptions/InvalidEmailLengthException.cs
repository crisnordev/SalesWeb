namespace SalesWeb.Shared.Exceptions;

public class InvalidEmailLengthException : Exception
{
    public InvalidEmailLengthException(string message) : base(message)
    {
    }
}