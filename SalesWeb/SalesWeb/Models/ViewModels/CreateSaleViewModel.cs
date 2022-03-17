namespace SalesWeb.Models.ViewModels;

public class CreateSaleViewModel
{
    public Guid SaleId { get; set; }
    [DisplayName("Seller Id")]
    public Guid SellerId { get; set; } 
    [DisplayName("Customer Id")]
    public Guid CustomerId { get; set; }
    [DisplayName("Product Id")]
    public Guid ProductId { get; set; }
    [DisplayName("Quantity")]
    public int ProductQuantity { get; set; }
}