namespace SalesWeb.Models;

public class Sale
{
    public Guid Id { get; set; }
    
    public Customer Customer { get; set; }
    
    public Seller Seller { get; set; }

    public decimal TotalAmount { get; set; }

    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}