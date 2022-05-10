namespace SalesWeb.ViewModels.SaleViewModels;

public class GetSaleViewModel
{
    public GetSaleViewModel()
    {
    }

    public GetSaleViewModel(Guid saleId, string customer, string seller, decimal totalAmount)
    {
        SaleId = saleId;
        Customer = customer;
        Seller = seller;
        TotalAmount = totalAmount;
        SoldProducts = new List<SoldProduct>();
    }

    [DisplayName("Sale Id")] public Guid SaleId { get; set; }

    [DisplayName("Customer")] public string Customer { get; set; }

    [DisplayName("Seller")] public string Seller { get; set; }

    [DisplayName("Total")] public decimal TotalAmount { get; set; }

    public List<SoldProduct> SoldProducts { get; set; }
}