namespace SalesWeb.ViewModels.SellerViewModels;

public class EditorSellerViewModel
{
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "This field must have between 8 and 80 characters.")]
    [DisplayName("NAME")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    [EmailAddress(ErrorMessage = "This e-mail is invalid.")]
    [DataType(DataType.EmailAddress)]
    [DisplayName("E-MAIL")]
    public string Email { get; set; } 
    
    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(11, ErrorMessage = "This field must have 11 characters.")]
    [MinLength(11, ErrorMessage = "This field must have 11 characters.")]
    [DisplayName("DOCUMENT IDENTIFICATION")]
    public string DocumentId { get; set; } 
    
    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("PASSWORD")]
    public string Password { get; set; } 
    
    [Required(ErrorMessage = "This field is required.")]
    [DisplayName("BIRTH DATE")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; } = DateTime.Now;
    
    public static implicit operator EditorSellerViewModel(Seller seller)
    {
        var editorSeller = new EditorSellerViewModel
        {
            Name = seller.Name,
            Email = seller.Email,
            DocumentId = seller.DocumentId,
            Password = seller.Password,
            BirthDate = seller.BirthDate
        };
        return editorSeller;
    }
}