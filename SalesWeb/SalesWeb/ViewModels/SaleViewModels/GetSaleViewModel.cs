namespace SalesWeb.ViewModels.SaleViewModels;

public class GetSaleViewModel
{
    public GetSaleViewModel(){}

    public GetSaleViewModel(Guid saleId, Customer customer, Seller seller, decimal totalAmount)
    {
        SaleId = saleId;
        Customer = customer;
        Seller = seller;
        TotalAmount = totalAmount;
    }

    [DisplayName("Sale Id")] public Guid SaleId { get; set; }

    [DisplayName("Customer")] public Customer Customer { get; set; }

    [DisplayName("Seller")] public Seller Seller { get; set; }

    [DisplayName("Total")] public decimal TotalAmount { get; set; }
    
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
    
    public static implicit operator GetSaleViewModel(Sale sale)
    {
        var getSale = new GetSaleViewModel
        {
            SaleId = sale.SaleId,
            Customer = sale.Customer,
            Seller = sale.Seller,
            TotalAmount = sale.TotalAmount,
            SoldProducts = sale.SoldProducts
        };
        return getSale;
    }
}