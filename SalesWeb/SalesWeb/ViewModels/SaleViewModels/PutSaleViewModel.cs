namespace SalesWeb.ViewModels.SaleViewModels;

public class PutSaleViewModel
{
    public PutSaleViewModel(){}
    
    public PutSaleViewModel(string customer, string seller, decimal totalAmount)
    {
        Customer = customer;
        Seller = seller;
        TotalAmount = totalAmount;
    }
    
    public PutSaleViewModel(Guid saleId, string customer, string seller, Guid productId, decimal totalAmount, int productQuantity)
    {
        SaleId = saleId;
        Customer = customer;
        Seller = seller;
        ProductId = productId;
        TotalAmount = totalAmount;
        ProductQuantity = productQuantity;
    }
    
    [DisplayName("Sale Id")] public Guid SaleId { get; set; }
    
    [DisplayName("Customer")] public string Customer { get; set; }

    [DisplayName("Seller")] public string Seller { get; set; }

    [DisplayName("Product")] public Guid ProductId { get; set; }
    
    [DisplayName("Quantity")] public int ProductQuantity { get; set; }

    [DisplayName("Total")] public decimal TotalAmount { get; set; }

    public List<SoldProduct> SoldProducts { get; set; } = new ();
}