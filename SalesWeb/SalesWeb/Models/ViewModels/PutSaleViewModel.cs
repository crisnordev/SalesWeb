namespace SalesWeb.Models.ViewModels;

public class PutSaleViewModel
{
    [DisplayName("Sale Id")]
    public Guid SaleId { get; set; }
    [DisplayName("Customer Id")]
    public Guid CustomerId { get; set; }
    [DisplayName("Customer Name")]
    public string CustomerName { get; set; } = String.Empty;
    [DisplayName("Seller Id")] 
    public Guid SellerId { get; set; }
    [DisplayName("Seller Name")]
    public string SellerName { get; set; } = String.Empty;
    [DisplayName("Total Amount")]
    public decimal TotalAmount { get; set; }
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
    [DisplayName("Product Id")]
    public Guid ProductId { get; set; }
    [DisplayName("Product Quantity")] 
    public int ProductQuantity { get; set; } 
}