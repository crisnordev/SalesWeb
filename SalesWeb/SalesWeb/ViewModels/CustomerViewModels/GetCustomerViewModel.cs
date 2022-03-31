namespace SalesWeb.ViewModels.CustomerViewModels;

public class GetCustomerViewModel
{
    [DisplayName("CUSTOMER IDENTIFICATION")] public Guid Id { get; set; }

    [DisplayName("NAME")] public string Name { get; set; }
    
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
            Id = customer.Id,
            Name = customer.Name,
            Email = customer.Email,
            DocumentId = customer.DocumentId,
            BirthDate = customer.BirthDate
        };
        return getCustomer;
    }
}