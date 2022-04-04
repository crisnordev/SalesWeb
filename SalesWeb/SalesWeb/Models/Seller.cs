namespace SalesWeb.Models;
public class Seller
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; } 
    
    public string DocumentId { get; set; } 
    
    public string Password { get; set; }

    public DateTime BirthDate { get; set; } = DateTime.Now;
}