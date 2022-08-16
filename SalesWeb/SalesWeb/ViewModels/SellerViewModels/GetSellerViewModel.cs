namespace SalesWeb.ViewModels.SellerViewModels;

public class GetSellerViewModel
{
    [DisplayName("SELLER IDENTIFICATION")] public Guid SellerId { get; set; }

    [DisplayName("NAME")] public string Name { get; set; }

    [DisplayName("E-MAIL")] public string Email { get; set; } 
    
    [DisplayName("DOCUMENT IDENTIFICATION")] public string DocumentId { get; set; } 

    [DisplayName("PASSWORD")] public string Password { get; set; } 
    
    [DisplayName("BIRTH DATE")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; } = DateTime.Now;
    
    public static implicit operator GetSellerViewModel(Seller seller)
    {
        var getSeller = new GetSellerViewModel
        {
            SellerId = seller.SellerId,
            Name = seller.FirstName + " " + seller.LastName,
            Email = seller.Email,
            DocumentId = seller.DocumentId,
            Password = seller.Password,
            BirthDate = seller.BirthDate
        };
        return getSeller;
    }
}