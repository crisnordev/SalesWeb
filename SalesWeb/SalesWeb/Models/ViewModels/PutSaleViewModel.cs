namespace SalesWeb.Models.ViewModels;

public class PutSaleViewModel
{
    [DisplayName("SALE IDENTIFICATION")]
    public Guid SaleId { get; set; }

    [DisplayName("CUSTOMER IDENTIFICATION")]
    public Guid CustomerId { get; set; }

    [DisplayName("CUSTOMER NAME")]
    public string CustomerName { get; set; } = String.Empty;

    [DisplayName("SELLER NAME")]
    public Guid SellerId { get; set; }

    [DisplayName("SELLER NAME")]
    public string SellerName { get; set; } = String.Empty;

    [DisplayName("TOTAL")]
    public decimal TotalAmount { get; set; }
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();

    [DisplayName("PRODUCT IDENTIFICATION")]
    public Guid ProductId { get; set; }

    [DisplayName("QUANTITY")] public int ProductQuantity { get; set; }
}