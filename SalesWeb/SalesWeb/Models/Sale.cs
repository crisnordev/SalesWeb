namespace SalesWeb.Models;

public class Sale
{
    public Sale()
    {
        SoldProducts = new List<SoldProduct>();
    }
    
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Customer Customer { get; set; }
    
    public Seller Seller { get; set; }

    public decimal TotalAmount { get; set; }

    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}