namespace SalesWeb.ViewModels.CustomerViewModels;

public class PutCustomerViewModel
{
    public PutCustomerViewModel(){}
    
    public PutCustomerViewModel(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    [DisplayName("Customer Id")] public Guid CustomerId { get; set; }

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
    

    public static implicit operator PutCustomerViewModel(Customer customer)
    {
        var putCustomerViewModel = new PutCustomerViewModel(customer.Name.FirstName, customer.Name.LastName,
            customer.Email.Address);
        return putCustomerViewModel;
    }
}