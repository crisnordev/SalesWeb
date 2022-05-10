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
    
    public decimal Price { get; set; }
}