namespace SalesWeb.Models;

public class SoldProduct
{
    public SoldProduct(){}

    public SoldProduct(Guid soldProductId, int productId, ProductName productName, int quantity, decimal price)
    {
        SoldProductId = soldProductId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
    }

    public Guid SoldProductId { get; set; }
    
    [DisplayName("Product Id")] public int ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Product name must have between 2 and 160 characters.")]
    [DisplayName("Name")]
    public ProductName ProductName { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal Price { get; set; }
    
    public IList<Sale> Sales { get; set; } = new List<Sale>();
}