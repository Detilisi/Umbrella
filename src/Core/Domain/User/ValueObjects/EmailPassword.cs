using Domain.User.Exceptions;

namespace Domain.User.ValueObjects;

public class EmailPassword : ValueObject<string>
{
    //Fields
    public const int MINIMUMPASSWORDLENGTH = 8; // 8 ALPHA-NUMERIC CHARACTERS

    //Contructions
    private EmailPassword() : base(string.Empty)
    {
        //Required EF
    }
    private EmailPassword(string value) : base(value)
    {
        Value = value;
    }

    public static EmailPassword Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new EmptyValueException(nameof(EmailAddress));
        }

        if (password.Length < MINIMUMPASSWORDLENGTH)
        {
            throw new PasswordTooShortException(MINIMUMPASSWORDLENGTH);
        }

        return new EmailPassword(password);
    }

    //Override methods
    public override string ToString() => Value;
    protected override IEnumerable<object> GetEqualityComponents() { yield return Value; }
}
