namespace SalesWeb.ViewModels.ProductViewModels;

public class EditorProductViewModel
{
    public EditorProductViewModel(){}
    
    [DisplayName("Product Id")] public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "Product name must have between 8 and 80 characters.")]
    [DisplayName("Name")]
    public string ProductName { get; set; }
    
    [Range(0.01D, 1000.00D)]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    public static implicit operator EditorProductViewModel(Product product)
    {
        var editorProduct = new EditorProductViewModel
        {
            ProductName = product.ProductName,
            Price = product.Price
        };
        return editorProduct;
    }
}