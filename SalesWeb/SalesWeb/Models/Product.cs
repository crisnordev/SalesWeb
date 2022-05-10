namespace SalesWeb.Models;

public class Product
{
    public Product(){}

    public Product(Guid productId, ProductName productName, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }

    public Guid ProductId { get; set; }
    
    public ProductName ProductName { get; set; }
    
    [DataType(DataType.Currency)] 
    [DisplayName("Price")]
    public decimal Price { get; set; }
}