namespace SalesWeb.ViewModels.CustomerViewModels;

public class EditorCustomerViewModel
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
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; } = DateTime.Now;

    public static implicit operator EditorCustomerViewModel(Customer customer)
    {
        var editorCustomer = new EditorCustomerViewModel
        {
            Name = customer.Name,
            Email = customer.Email,
            DocumentId = customer.DocumentId,
            BirthDate = customer.BirthDate
        };
        return editorCustomer;
    }

    public static implicit operator Customer(EditorCustomerViewModel editorCustomerViewModel)
    {
        var customer = new Customer
        {
            Id = new Guid(),
            Name = editorCustomerViewModel.Name,
            Email = editorCustomerViewModel.Email,
            DocumentId = editorCustomerViewModel.DocumentId,
            BirthDate = editorCustomerViewModel.BirthDate
        };
        return customer;
    }
        
}