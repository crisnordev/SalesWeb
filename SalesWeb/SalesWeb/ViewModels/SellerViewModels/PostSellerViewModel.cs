namespace SalesWeb.ViewModels.SellerViewModels;

public class PostSellerViewModel
{
    public PostSellerViewModel() {}

    public PostSellerViewModel(string firstName, string lastName, string email, string documentId, string password, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DocumentIdentificationNumber = documentId;
        Password = password;
        BirthDate = birthDate;
    }
    
    [DisplayName("Seller Id")] public Guid SellerId { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "This field must have between 2 and 60 characters.")]
    [DisplayName("First Name")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "This field must have between 2 and 120 characters.")]
    [DisplayName("Last Name")]
    public string LastName { get; set; }
    
    [Required(ErrorMessage = "E-mail is required.")]
    [EmailAddress(ErrorMessage = "This e-mail is invalid.")]
    [DataType(DataType.EmailAddress)]
    [StringLength(160, ErrorMessage = "E-mail must have a 160 characters maximum.")]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Document Id is required.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "Document Id must have 14 characters, including \".\" and \"-\".")]
    [DisplayName("Document Id")]
    public string DocumentIdentificationNumber { get; set; } 
    
    [Required(ErrorMessage = "Your password is required.")]
    [DisplayName("Password")]
    public string Password { get; set; } 
    
    [Required(ErrorMessage = "Birth date is required.")]
    [DisplayName("Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; } = DateTime.Now;
}