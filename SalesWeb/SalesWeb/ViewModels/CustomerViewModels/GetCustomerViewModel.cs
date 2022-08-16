namespace SalesWeb.ViewModels.CustomerViewModels;

public class GetCustomerViewModel
{
    [DisplayName("CUSTOMER IDENTIFICATION")] public Guid CustomerId { get; set; }

    [DisplayName("FIRST NAME")] public string FirstName { get; set; }
    
    [DisplayName("LAST NAME")] public string LastName { get; set; }
    
    [DisplayName("E-MAIL")] public string Email { get; set; }
    
    [DisplayName("DOCUMENT IDENTIFICATION")]  public string DocumentId { get; set; }
    
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    public static implicit operator GetCustomerViewModel(Customer customer)
    {
        var getCustomer = new GetCustomerViewModel
        {
            CustomerId = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            DocumentId = customer.DocumentId,
            BirthDate = customer.BirthDate
        };
        return getCustomer;
    }
}