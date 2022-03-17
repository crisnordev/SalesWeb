using System.ComponentModel;

namespace SalesWeb.Models;

public class Sale
{
    public Guid Id { get; set; }
    [DisplayName("Seller Id")]
    public Seller Seller { get; set; } 
    [DisplayName("Customer Id")]
    public Customer Customer { get; set; } 
    [Range(0.01D, 300000.00D)]
    [DataType(DataType.Currency)]
    [DisplayName("Total Amount")]
    public decimal TotalAmount { get; set; } 
    [DisplayName("Products")]
    public IList<SoldProduct> SoldProducts { get; set; } = new List<SoldProduct>();
}