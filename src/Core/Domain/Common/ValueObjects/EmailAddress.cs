using System.ComponentModel.DataAnnotations;

namespace Domain.Common.ValueObjects;

public class EmailAddress : ValueObject
{
    //Properties
    public string Address { get; private set; }
    public string Domain { get; private set; }

    //Contructions
    private EmailAddress(){ }
    private EmailAddress(string value) : base()
    {
        Address = value;
        Domain = Address[(Address.LastIndexOf('@') + 1)..];
    }

    public static EmailAddress Create(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            throw new EmptyValueException(nameof(EmailAddress));
        }

        if (!IsValidEmail(emailAddress))
        {
            throw new InvalidEmailAddressException();
        }

        return new EmailAddress(emailAddress);
    }

    //Helper methods
    private static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }

    //Override methods
    public override string ToString()=> Address;
    protected override IEnumerable<object> GetEqualityComponents(){ yield return Address;}
}