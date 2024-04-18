using Domain.Email.Exceptions;

namespace Domain.Email.ValueObjects;

public class EmailBodyText : ValueObject<string>
{
    //Fields
    public const int MAXBODYLENGTH = 10000;

    //Contructions
    private EmailBodyText() : base(default) 
    {
        //Required EF
    }
    private EmailBodyText(string value): base(value)
    {
        Value = value;
    }

    public static EmailBodyText Create(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            throw new EmptyValueException(nameof(EmailBodyText));
        }

        if (body.Length > MAXBODYLENGTH)
        {
            throw new EmailBodyTooLongException(MAXBODYLENGTH);
        }

        return new EmailBodyText(body);
    }

    //Override methods
    public override string ToString() => Value;
    protected override IEnumerable<object> GetEqualityComponents(){ yield return Value; }
}