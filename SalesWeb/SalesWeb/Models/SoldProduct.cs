namespace SalesWeb.Models;

public class SoldProduct
{
    [DisplayName("Product Id")]
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public int Quantity { get; set; } 
    public decimal Price { get; set; }
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}