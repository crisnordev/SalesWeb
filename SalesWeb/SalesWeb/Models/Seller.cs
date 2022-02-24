namespace SalesWeb.Models;
public class Seller
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    [StringLength(80, MinimumLength = 8, ErrorMessage = "This field must have between 8 and 80 characters.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "This field is required.")]
    [EmailAddress(ErrorMessage = "This e-mail is invalid.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } 
    
    [Required(ErrorMessage = "This field is required.")]
    public string Password { get; set; } 
    
    [Required(ErrorMessage = "This field is required.")]
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; } = DateTime.Now;


}