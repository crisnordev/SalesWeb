namespace SalesWeb.ViewModels.SaleViewModels;

public class PostSaleViewModel
{
    [DisplayName("SALE IDENTIFICATION")] public Guid Id { get; set; }
    
    [DisplayName("CUSTOMER IDENTIFICATION")] public Guid CustomerId { get; set; }

    [DisplayName("SELLER IDENTIFICATION")] public Guid SellerId { get; set; }

    [DisplayName("PRODUCT IDENTIFICATION")] public Guid ProductId { get; set; } 

    [DisplayName("QUANTITY")] public int ProductQuantity { get; set; }

    
    public static implicit operator Sale(PostSaleViewModel model)
    {
        var createSale = new Sale
        {
            Id = model.Id,
            Customer = new Customer(),
            Seller = new Seller(),
            TotalAmount = new decimal(),
            SoldProducts = new List<SoldProduct>()
        };
        return createSale;
    }
}