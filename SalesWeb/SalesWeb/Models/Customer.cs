namespace SalesWeb.Models;

public class Customer
{
    protected Customer(){}
    
    public Customer(Guid customerId, Name name, Email email, DocumentIdentificationNumber documentIdentificationNumber, DateTime birthDate)
    {
        CustomerId = customerId;
        Name = name;
        Email = email;
        DocumentIdentificationNumber = documentIdentificationNumber;
        BirthDate = birthDate;
    }

    public Guid CustomerId { get; set; }
    
    public Name Name { get; set; } 
    
    public Email Email { get; set; } 
    
    public DocumentIdentificationNumber DocumentIdentificationNumber { get; set; } 
    
    public DateTime BirthDate { get; set; } = DateTime.Now;
}