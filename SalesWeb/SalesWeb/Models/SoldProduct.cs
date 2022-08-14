namespace SalesWeb.Models;

public class SoldProduct
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [DisplayName("PRODUCT IDENTIFICATION")] public string ProductId { get; set; } = string.Empty;

    [DisplayName("NAME")] public string Name { get; set; } = string.Empty;
    
    [DisplayName("QUANTITY")] public int Quantity { get; set; }

    [DisplayName("PRICE")] public decimal Price { get; set; }
    
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}