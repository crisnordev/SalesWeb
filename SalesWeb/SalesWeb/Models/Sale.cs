namespace SalesWeb.Models;

public class Sale
{
    [DisplayName("SALE IDENTIFICATION")] public Guid Id { get; set; }

    [DisplayName("SELLER IDENTIFICATION")] public Seller Seller { get; set; }

    [DisplayName("CUSTOMER IDENTIFICATION")] public Customer Customer { get; set; }
    
    [Range(0.01D, 300000.00D)]
    [DataType(DataType.Currency)]
    [DisplayName("TOTAL")]
    public decimal TotalAmount { get; set; }

    [DisplayName("PRODUCTS")] public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}