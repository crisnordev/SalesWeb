namespace SalesWeb.Models.ViewModels;

public class ListSaleViewModel
{
    [DisplayName("SALE IDENTIFICATION")]
    public Guid SaleId { get; set; }

    [DisplayName("CUSTOMER NAME")]
    public string CustomerName { get; set; }

    [DisplayName("SELLER NAME")]
    public string SellerName { get; set; }

    [DisplayName("TOTAL")]
    public decimal TotalAmount { get; set; }
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}