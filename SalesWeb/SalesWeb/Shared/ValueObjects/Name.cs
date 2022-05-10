using SalesWeb.Shared.Exceptions;

namespace SalesWeb.Shared.ValueObjects;

public class Name : ValueObject
{
    public Name(){}

    public Name(string name)
    {
        FullName = name;
        SplitName(name);
        Validate();
    }
    
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        FullName = FirstName + " " + LastName;
        Validate();
    }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "First name must have between 2 and 60 characters.")]
    [DisplayName("First name")]
    public string FirstName { get; private set; } 

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Last name must have between 2 and 120 characters.")]
    [DisplayName("Last name")] 
    public string LastName { get; private set; } 
    
    [DisplayName("Name")] public string FullName { get; private set; } 

    private void Validate()
    {
        if (FirstName == null | LastName == null | FullName == null)
            throw new ArgumentNullException(null, "First name can not be null or empty.");

        switch (FirstName.Length)
        {
            case <= 2:
                throw new InvalidNameLengthException("First name must have more than 2 characters.");
            
            case >= 60:
                throw new InvalidNameLengthException("First name must have less than 60 characters.");
        }
        
        switch (LastName.Length)
        {
            case <= 2:
                throw new InvalidNameLengthException("First name must have more than 2 characters.");
            case >= 120:
                throw new InvalidNameLengthException("First name must have less than 120 characters.");
        }
    }

    public void SplitName(string name)
    {
        FirstName = name.Split(" ")[0];
        foreach (var lastName in name.Split(" ")[..])
        {
            LastName += lastName + " ";
        }
    }

    public static implicit operator string(Name name) => name.FullName;

    public static implicit operator Name(string name)
    {
        var fullName = new Name(name);
        fullName.SplitName(name);
        return fullName;
    }

    public override string ToString() => FullName;
}