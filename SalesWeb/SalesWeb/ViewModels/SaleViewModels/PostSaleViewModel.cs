namespace SalesWeb.ViewModels.SaleViewModels;

public class PostSaleViewModel
{
    public PostSaleViewModel(){}

    public PostSaleViewModel(Guid saleId, Guid customerId, Guid sellerId, Guid productId, int productQuantity, decimal totalAmount)
    {
        SaleId = saleId;
        CustomerId = customerId;
        SellerId = sellerId;
        ProductId = productId;
        ProductQuantity = productQuantity;
        TotalAmount = totalAmount;
    }
    
    [DisplayName("Sale Id")] public Guid SaleId { get; set; }

    [DisplayName("Customer")] public Guid CustomerId { get; set; }

    [DisplayName("Seller")] public Guid SellerId { get; set; }

    [DisplayName("Product")] public Guid ProductId { get; set; }
    
    [DisplayName("Quantity")] public int ProductQuantity { get; set; }

    [DisplayName("Total")] 
    [DataType(DataType.Currency)]
    public decimal TotalAmount { get; set; }
}