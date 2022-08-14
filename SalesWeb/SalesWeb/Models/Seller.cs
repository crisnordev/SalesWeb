namespace SalesWeb.Models;
public class Seller
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public string DocumentId { get; set; } = string.Empty; 
    
    public string Password { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; } = DateTime.Now;
}