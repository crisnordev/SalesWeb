namespace SalesWeb.Shared.Exceptions;

public class InvalidCodeLengthException : Exception
{
    public InvalidCodeLengthException(string message) : base (message)
    {
    }
}