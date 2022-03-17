namespace SalesWeb.Models.ViewModels;

public class ListSaleViewModel
{
    [DisplayName("Sale Id")]
    public Guid SaleId { get; set; }
    [DisplayName("Customer Name")]
    public string CustomerName { get; set; }
    [DisplayName("Seller Name")]
    public string SellerName { get; set; }
    [DisplayName("Total Amount")]
    public decimal TotalAmount { get; set; }
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}