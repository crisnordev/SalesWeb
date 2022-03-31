namespace SalesWeb.ViewModels.ProductViewModels;

public class GetProductViewModel
{
    [DisplayName("PRODUCT IDENTIFICATION")] public Guid Id { get; set; }
    
    [DisplayName("NAME")] public string Name { get; set; }
    
    [DisplayName("PRICE")] public decimal Price { get; set; }

    public static implicit operator GetProductViewModel(Product product)
    {
        var getProduct = new GetProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
        return getProduct;
    }
}