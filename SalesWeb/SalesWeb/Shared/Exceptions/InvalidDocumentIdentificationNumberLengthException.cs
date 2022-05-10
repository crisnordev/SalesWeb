namespace SalesWeb.Shared.Exceptions;

public class InvalidDocumentIdentificationNumberLengthException : Exception
{
    public InvalidDocumentIdentificationNumberLengthException(string message) : base (message)
    {
    }
}