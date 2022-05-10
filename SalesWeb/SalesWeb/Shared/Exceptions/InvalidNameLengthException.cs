namespace SalesWeb.Shared.Exceptions;

public class InvalidNameLengthException : Exception
{
    public InvalidNameLengthException(string message) : base(message)
    {
    }
}