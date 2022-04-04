namespace SalesWeb.ViewModels.SaleViewModels;

public class GetSaleViewModel
{
    [DisplayName("SALE IDENTIFICATION")] public Guid Id { get; set; }

    [DisplayName("CUSTOMER NAME")] public string CustomerName { get; set; }

    [DisplayName("SELLER NAME")] public string SellerName { get; set; }

    [DisplayName("TOTAL")] public decimal TotalAmount { get; set; }
    
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
    
    public static implicit operator GetSaleViewModel(Sale sale)
    {
        var getSale = new GetSaleViewModel
        {
            Id = sale.Id,
            CustomerName = sale.Customer.Name,
            SellerName = sale.Seller.Name,
            TotalAmount = sale.TotalAmount,
            SoldProducts = sale.SoldProducts
        };
        return getSale;
    }
}