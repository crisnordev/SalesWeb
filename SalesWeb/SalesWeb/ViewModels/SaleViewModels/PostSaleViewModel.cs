namespace SalesWeb.ViewModels.SaleViewModels;

public class PostSaleViewModel
{
    public PostSaleViewModel(){}
    
    public PostSaleViewModel(Guid customerId, Guid sellerId, int productId, int productQuantity, decimal totalAmount)
    {
        CustomerId = customerId;
        SellerId = sellerId;
        ProductId = productId;
        ProductQuantity = productQuantity;
        TotalAmount = totalAmount;
    }

    public Guid Id { get; set; }
    [DisplayName("Customer")] public Guid CustomerId { get; set; }

    [DisplayName("Seller")] public Guid SellerId { get; set; }

    [DisplayName("Product")] public int ProductId { get; set; }
    
    [DisplayName("Quantity")] public int ProductQuantity { get; set; }

    [DisplayName("Total")] 
    [DataType(DataType.Currency)]
    public decimal TotalAmount { get; set; }
}