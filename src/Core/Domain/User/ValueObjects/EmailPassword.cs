using Domain.User.Exceptions;

namespace Domain.User.ValueObjects;

public class EmailPassword : ValueObject
{
    //Fields
    public const int MINIMUMPASSWORDLENGTH = 8; // 8 ALPHA-NUMERIC CHARACTERS

    //Properties
    public string Password { get; private set; }

    //Contructions
    private EmailPassword(string value):base()
    {
        Password = value;
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
    public override string ToString() => Password;
    protected override IEnumerable<object> GetEqualityComponents(){ yield return Password; }
}
