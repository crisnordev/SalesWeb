using SalesWeb.Shared.Exceptions;

namespace SalesWeb.Shared.ValueObjects;

public class ProductName
{
    public ProductName(string productFullName)
    {
        ProductFullName = productFullName;
        Validate();
    }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(160, MinimumLength = 2, ErrorMessage = "Product name must have between 2 and 160 characters.")]
    [DisplayName("Product name")]
    public string ProductFullName { get; private set; } 

    private void Validate()
    {
        if (ProductFullName == null)
            throw new ArgumentNullException(null, "Product name can not be null or empty.");

        switch (ProductFullName.Length)
        {
            case <= 2:
                throw new InvalidNameLengthException("Product name must have more than 2 characters.");
            case >= 160:
                throw new InvalidNameLengthException("Product name must have less than 160 characters.");
        }
    }
    
    public static implicit operator string(ProductName productName) => productName.ProductFullName;

    public static implicit operator ProductName(string productName) => new (productName);
    
    public override string ToString() => ProductFullName;
}