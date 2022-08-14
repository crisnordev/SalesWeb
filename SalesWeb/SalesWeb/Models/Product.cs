namespace SalesWeb.Models;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
}