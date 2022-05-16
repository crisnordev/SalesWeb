namespace SalesWeb.Models;

public class Sale
{
    public Sale(){}

    public Sale(Guid saleId, Customer customer, Seller seller, decimal totalAmount)
    {
        SaleId = saleId;
        Customer = customer;
        Seller = seller;
        TotalAmount = totalAmount;
        SoldProducts = new List<SoldProduct>();
    }

    public Guid SaleId { get; set; }
    
    public Customer Customer { get; set; }
    
    public Seller Seller { get; set; }

    public decimal TotalAmount { get; set; }
    
    public IList<SoldProduct> SoldProducts { get; set; } 
}