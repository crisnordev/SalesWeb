namespace SalesWeb.ViewModels.CustomerViewModels;

public class GetCustomerViewModel
{
    public GetCustomerViewModel(){}
    
    [DisplayName("Customer Id")] public Guid CustomerId { get; set; }

    [DisplayName("Customer name")] public string Name { get; set; } 
    
    [DisplayName("E-mail")] public string Email { get; set; }
    
    [DisplayName("Document Id")] public DocumentIdentificationNumber DocumentIdentificationNumber { get; set; }
    
    [DisplayName("Birth Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}")]
    public DateTime BirthDate { get; set; }

    public static implicit operator GetCustomerViewModel(Customer customer)
    {
        var getCustomer = new GetCustomerViewModel
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Email = customer.Email,
            DocumentIdentificationNumber = customer.DocumentIdentificationNumber,
            BirthDate = customer.BirthDate
        };
        return getCustomer;
    }
}