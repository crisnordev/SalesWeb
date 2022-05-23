namespace SalesWeb.ViewModels.SellerViewModels;

public class GetSellerViewModel
{
    public GetSellerViewModel(){}

    public GetSellerViewModel(Guid sellerId, string name, string email, string documentIdentificationNumber, 
        string password, DateTime birthDate)
    {
        SellerId = sellerId;
        Name = name;
        Email = email;
        DocumentIdentificationNumber = documentIdentificationNumber;
        Password = password;
        BirthDate = birthDate;
    }

    [DisplayName("Seller Id")] public Guid SellerId { get; set; } 

    [DisplayName("Name")] public string Name { get; set; }

    [DisplayName("E-mail")] public string Email { get; set; } 
    
    [DisplayName("Document Id")] public string DocumentIdentificationNumber { get; set; } 

    [DisplayName("Password")] public string Password { get; set; } 
    
    [DisplayName("Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; } = DateTime.Now;
    
    public static implicit operator GetSellerViewModel(Seller seller)
    {
        var getSeller = new GetSellerViewModel
        {
            SellerId = seller.SellerId,
            Name = seller.Name.ToString(),
            Email = seller.Email,
            DocumentIdentificationNumber = seller.DocumentIdentificationNumber,
            BirthDate = seller.BirthDate
        };
        return getSeller;
    }
}