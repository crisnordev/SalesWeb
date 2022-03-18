namespace SalesWeb.Models;

public class SoldProduct
{
    [DisplayName("PRODUCT IDENTIFICATION")]
    public Guid Id { get; set; }

    [DisplayName("NAME")] public string Name { get; set; }
    [DisplayName("QUANTITY")] public int Quantity { get; set; }

    [DisplayName("PRICE")]
    public decimal Price { get; set; }
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}