namespace SalesWeb.ViewModels.ProductViewModels;

public class GetProductViewModel
{
    public GetProductViewModel(){}

    public GetProductViewModel(int productId, string productName, decimal price)
    {
        ProductId = productId;
        ProductName = productName;
        Price = price;
    }

    [DisplayName("Product Id")] public int ProductId { get; set; }
    
    [DisplayName("Name")] public string ProductName { get; set; }
    
    [DisplayName("Price")] public decimal Price { get; set; }

    public static implicit operator GetProductViewModel(Product product)
    {
        var getProduct = new GetProductViewModel
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Price = product.Price
        };
        return getProduct;
    }
}