namespace SalesWeb.Models;

public class Customer
{
    public Guid Id { get; set; } 
    
    public string Name { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string DocumentId { get; set; } = string.Empty;
    
    public DateTime BirthDate { get; set; } = DateTime.Now;
}