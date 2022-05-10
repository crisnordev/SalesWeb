using SalesWeb.Shared.Exceptions;

namespace SalesWeb.Shared.ValueObjects;

public class DocumentIdentificationNumber : ValueObject
{
    public DocumentIdentificationNumber() {}
    
    public DocumentIdentificationNumber(string number)
    {
        Number = number;
        Validate();
    }

    public string Number { get; private set; } 

    private void Validate()
    {
        if (string.IsNullOrEmpty(Number))
            throw new ArgumentNullException(null, "Document identification can not be null or empty.");

        if (Number.Length != 14)
            throw new InvalidDocumentIdentificationNumberLengthException("Document identification must have 14 characters, including \".\" and \"-\".");
    }


    public static implicit operator string(DocumentIdentificationNumber documentIdentificationNumber) => documentIdentificationNumber.Number;

    public static implicit operator DocumentIdentificationNumber(string name) => new (name);
}