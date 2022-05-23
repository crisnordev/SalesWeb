namespace SalesWeb.Models;

public class Product
{
    public Product(){}

    public Product(int productId, ProductName productName, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }

    public int ProductId { get; set; }
    
    public ProductName ProductName { get; set; } 
    
    [Range(0.01D, 1000.00D)]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
}