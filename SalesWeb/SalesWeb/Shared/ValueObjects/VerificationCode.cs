using SalesWeb.Shared.Exceptions;

namespace SalesWeb.Shared.ValueObjects;

public class VerificationCode : ValueObject
{
    public VerificationCode()
    {
        Code = Guid.NewGuid().ToString().Replace("-", "")[..8].ToUpper();
        ExpirationDate = DateTime.UtcNow.Date.AddMinutes(2);
        Verified = false;
    }

    //this constructor makes development easier, must be deleted later 
    public VerificationCode(string verificationCode, bool verified)
    {
        Code = verificationCode;
        Verified = verified;
    }
    
    [DisplayName("Verification code")] public string Code { get; private set; }

    public DateTime ExpirationDate { get; }

    public bool Verified { get; private set; }
    
    private void VerifySentCode(string code)
    {
        if (string.IsNullOrEmpty(Code))
            throw new ArgumentNullException(null, "Verification code can not be null or empty.");
        
        if (Code.Length != 8) throw new InvalidCodeLengthException("Verification code must have 8 characters.");

        if (Code != code) throw new InvalidVerificationCodeException("This verification code is invalid.");

        if (DateTime.UtcNow.Date > ExpirationDate)
            throw new ExceededDateTimeVerificationCodeException("Expired code. Should I send you a new code?");

        Verified = true;
    }

    public static implicit operator string(VerificationCode verificationCode) => verificationCode.Code;

    public static implicit operator VerificationCode(string verificationCodeString)
    {
        VerificationCode verificationCode = new() {Code = verificationCodeString};
        return verificationCode;
    }
}