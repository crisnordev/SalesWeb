namespace SalesWeb.ViewModels.SellerViewModels;

public class PutSellerViewModel
{
    public PutSellerViewModel(){}
    
    public PutSellerViewModel(string firstName, string lastName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }
    
    [DisplayName("Seller Id")] public Guid SellerId { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "First name must have between 2 and 60 characters.")]
    [DisplayName("First name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Last name must have between 2 and 120 characters.")]
    [DisplayName("Last name")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "E-mail is required.")]
    [EmailAddress(ErrorMessage = "This e-mail is invalid.")]
    [DataType(DataType.EmailAddress)]
    [StringLength(160, ErrorMessage = "E-mail must have a 160 characters maximum.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } 
    
    [Required(ErrorMessage = "Password is required.")]
    [DisplayName("Password")]
    public string Password { get; set; }

    public static implicit operator PutSellerViewModel(Seller seller)
    {
        var putSellerViewModel = new PutSellerViewModel(seller.Name.FirstName, seller.Name.LastName,
            seller.Email.Address, seller.Password);
        return putSellerViewModel;
    }
}