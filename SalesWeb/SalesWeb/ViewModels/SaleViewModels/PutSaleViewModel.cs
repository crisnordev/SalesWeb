namespace SalesWeb.ViewModels.SaleViewModels;

public class PutSaleViewModel
{
    [DisplayName("SALE IDENTIFICATION")] public Guid Id { get; set; }

    [DisplayName("CUSTOMER IDENTIFICATION")] public Guid CustomerId { get; set; }

    [DisplayName("CUSTOMER NAME")] public string CustomerName { get; set; } = String.Empty;

    [DisplayName("SELLER NAME")] public Guid SellerId { get; set; }

    [DisplayName("SELLER NAME")] public string SellerName { get; set; } = String.Empty;

    [DisplayName("TOTAL")] public decimal TotalAmount { get; set; }
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();

    [DisplayName("PRODUCT IDENTIFICATION")] public Guid ProductId { get; set; }

    [DisplayName("QUANTITY")] public int ProductQuantity { get; set; }
    
    public static implicit operator PutSaleViewModel(Sale sale)
    {
        var putSale = new PutSaleViewModel
        {
            Id = sale.Id,
            CustomerId = sale.Customer.Id,
            CustomerName = sale.Customer.FirstName + " " + sale.Customer.LastName,
            SellerId = sale.Seller.Id,
            SellerName = sale.Seller.FirstName + " " + sale.Seller.LastName,
            TotalAmount = sale.TotalAmount,
            SoldProducts = sale.SoldProducts,
            ProductId = new Guid(),
            ProductQuantity = new int()
        };
        return putSale;
    }
}