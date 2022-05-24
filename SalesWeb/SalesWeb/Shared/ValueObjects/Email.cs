using System.Text.RegularExpressions;

namespace SalesWeb.Shared.ValueObjects;

public class Email : ValueObject
{ 
    public Email() {}
    
    public Email(string address, bool confirmed)
    {
        Address = address;
        Confirmed = confirmed;
        Validate();
    }
    
    [Required(ErrorMessage = "E-mail is required.")]
    [EmailAddress(ErrorMessage = "This e-mail is invalid.")]
    [DataType(DataType.EmailAddress)]
    [StringLength(160, ErrorMessage = "E-mail must have a 160 characters maximum.")]
    [DisplayName("E-mail")] 
    public string Address { get; private set; } 

    public bool Confirmed { get; private set; }

    public VerificationCode? VerificationCode { get; private set; }

    private void Validate()
    {
        if (string.IsNullOrEmpty(Address))
            throw new ArgumentNullException(null, "Email can not be null or empty.");

        if (Address.Length > 160)
            throw new InvalidEmailLengthException("E-mail must have a 160 characters maximum.");
        
        Address = Address.ToLower().Trim();
        const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        if (!Regex.IsMatch(Address, pattern))
            throw new InvalidEmailException("This is not a valid e-mail address.");

        if (Confirmed) return;
        VerificationCode = new VerificationCode();
        ConfirmEmail(VerificationCode);
    }

    private void ConfirmEmail(VerificationCode code)
    {
        if (code.Verified)
            Confirmed = true;
    }
    
    public static implicit operator string(Email email) => email.Address;
    public static implicit operator Email(string address) => new() {Address = address};

}