namespace SalesWeb.Models;

public class SoldProduct
{
    public SoldProduct(){}

    public SoldProduct(Guid soldProductId, Guid productId, ProductName productName, int quantity, decimal price)
    {
        SoldProductId = soldProductId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public Guid SoldProductId { get; set; }
    
    public Guid ProductId { get; set; }

    public ProductName ProductName { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}