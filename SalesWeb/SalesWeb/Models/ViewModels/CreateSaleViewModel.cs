namespace SalesWeb.Models.ViewModels;

public class CreateSaleViewModel
{
    [DisplayName("SALE IDENTIFICATION")]
    public Guid SaleId { get; set; }

    [DisplayName("SELLER IDENTIFICATION")] public Guid SellerId { get; set; }

    [DisplayName("CUSTOMER IDENTIFICATION")]
    public Guid CustomerId { get; set; }

    [DisplayName("PRODUCT IDENTIFICATION")]
    public Guid ProductId { get; set; }

    [DisplayName("QUANTITY")]
    public int ProductQuantity { get; set; }
}