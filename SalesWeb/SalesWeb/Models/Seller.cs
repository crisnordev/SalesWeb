namespace SalesWeb.Models;
public class Seller
{
    public Seller() {}

    public Seller(Guid sellerId, Name name, Email email, DocumentIdentificationNumber documentIdentificationNumber, string password, DateTime birthDate)
    {
        SellerId = sellerId;
        Name = name;
        Email = email;
        DocumentIdentificationNumber = documentIdentificationNumber;
        Password = password;
        BirthDate = birthDate;
    }

    public Guid SellerId { get; set; }
    
    public Name Name { get; set; } 

    public Email Email { get; set; } 

    public DocumentIdentificationNumber DocumentIdentificationNumber { get; set; } 

    public string Password { get; set; } 

    public DateTime BirthDate { get; set; } 
}