using Domain.User.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.User.ValueObjects;

public class EmailPassword : ValueObject
{
    //Fields
    public const int MINIMUMPASSWORDLENGTH = 8; // 8 ALPHA-NUMERIC CHARACTERS

    //Properties
    public string Key { get; private set; }

    //Contructions
    private EmailPassword() { }
    private EmailPassword(string value):base()
    {
        Key = value;
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
    public override string ToString() => Key;
    protected override IEnumerable<object> GetEqualityComponents(){ yield return Key; }
}
