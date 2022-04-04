namespace SalesWeb.ViewModels.ProductViewModels;

public class EditorProductViewModel
{
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "This field must have between 8 and 80 characters.")]
    [DisplayName("NAME")]
    public string Name { get; set; }
    
    [Range(0.01D, 1000.00D)]
    [Column(TypeName = "decimal(18, 2)")]
    [DataType(DataType.Currency)]
    [DisplayName("PRICE")]
    public decimal Price { get; set; }

    public static implicit operator EditorProductViewModel(Product product)
    {
        var editorProduct = new EditorProductViewModel
        {
            Name = product.Name,
            Price = product.Price
        };
        return editorProduct;
    }
}