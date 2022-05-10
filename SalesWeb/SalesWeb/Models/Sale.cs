namespace SalesWeb.Models;

public class Sale
{
    public Sale(){}

    public Sale(Guid saleId, Customer customer, Seller seller, decimal totalAmount, SoldProduct soldProduct)
    {
        SaleId = saleId;
        Customer = customer;
        Seller = seller;
        TotalAmount = totalAmount;
        SoldProduct = soldProduct;
    }

    public Guid SaleId { get; set; }
    
    public Customer Customer { get; set; }
    
    public Seller Seller { get; set; }

    public decimal TotalAmount { get; set; }
    
    [NotMapped] public SoldProduct SoldProduct { get; set; }

    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}