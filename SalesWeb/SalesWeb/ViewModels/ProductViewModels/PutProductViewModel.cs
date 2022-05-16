namespace SalesWeb.ViewModels.ProductViewModels;

public class PutProductViewModel
{
    public PutProductViewModel(){}

    public PutProductViewModel(int productId, string productName, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }

    [DisplayName("Product Id")] public int ProductId { get; set; }
    
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Product name must have between 2 and 160 characters.")]
    [DisplayName("Name")]
    public string ProductName { get; set; }
    
    [Range(0.01D, 1000.00D)]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    public static implicit operator PutProductViewModel(Product product)
    {
        var postProduct = new PutProductViewModel
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Price = product.Price
        };
        return postProduct;
    }
}