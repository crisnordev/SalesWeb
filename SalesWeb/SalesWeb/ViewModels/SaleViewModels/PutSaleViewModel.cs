namespace SalesWeb.ViewModels.SaleViewModels;

public class PutSaleViewModel
{
    public PutSaleViewModel(){}

    public PutSaleViewModel(Guid saleId, string customer, string seller, int productId, int productQuantity, decimal totalAmount)
    {
        SaleId = saleId;
        Customer = customer;
        Seller = seller;
        ProductId = productId;
        ProductQuantity = productQuantity;
        TotalAmount = totalAmount;
        SoldProducts = new List<SoldProduct>();
    }

    [DisplayName("Sale Id")] public Guid SaleId { get; set; }
    
    [DisplayName("Customer")] public string Customer { get; set; }

    [DisplayName("Seller")] public string Seller { get; set; }

    [DisplayName("Product")] public int ProductId { get; set; }
    
    [DisplayName("Quantity")] public int ProductQuantity { get; set; }

    [DisplayName("Total")] public decimal TotalAmount { get; set; }

    public IReadOnlyCollection<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}