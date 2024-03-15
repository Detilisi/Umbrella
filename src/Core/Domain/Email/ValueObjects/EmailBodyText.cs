using Domain.Email.Exceptions;

namespace Domain.Email.ValueObjects;

public class EmailBodyText : ValueObject
{
    //Fields
    public const int MAXBODYLENGTH = 10000;

    //Properties
    public string Text { get; private set; }

    //Contructions
    private EmailBodyText(string value): base()
    {
        Text = value;
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
    public override string ToString() => Text;
    protected override IEnumerable<object> GetEqualityComponents(){ yield return Text; }
}