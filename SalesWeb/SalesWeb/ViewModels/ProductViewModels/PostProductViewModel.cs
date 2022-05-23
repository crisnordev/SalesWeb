namespace SalesWeb.ViewModels.ProductViewModels;

public class PostProductViewModel
{
    public PostProductViewModel(){}
    
    public PostProductViewModel(string productName, decimal price)
    {
        ProductName = productName;
        Price = price;
    }
    
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Product name must have between 2 and 160 characters.")]
    [DisplayName("Name")]
    public string ProductName { get; set; }
    
    [Range(0.01D, 1000.00D)]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal Price { get; set; }
}