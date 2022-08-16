namespace SalesWeb.Models;

public class Customer
{
    public Guid CustomerId { get; set; } = Guid.NewGuid();
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string DocumentId { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; } = DateTime.Now;
}