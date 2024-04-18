using Domain.Email.Exceptions;

namespace Domain.Email.ValueObjects;

public class EmailSubjectLine : ValueObject<string>
{
    //Fields
    public const int MAXSUBJECTLINELENGTH = 200;

    //Contructions
    private EmailSubjectLine() : base(default)
    {
        //Required EF
    }
    private EmailSubjectLine(string value) : base(value)
    {
        Value = value;
    }

    public static EmailSubjectLine Create(string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new EmptyValueException(nameof(EmailSubjectLine));
        }

        if (subject.Length > MAXSUBJECTLINELENGTH)
        {
            throw new SubjectLineTooLongException(MAXSUBJECTLINELENGTH);
        }

        if (subject.Contains('\n') || subject.Contains('\r'))
        {
            throw new InvalidSubjectException();
        }

        return new EmailSubjectLine(subject);
    }

    //Override methods
    public override string ToString() => Value;
    protected override IEnumerable<object> GetEqualityComponents() { yield return Value; }
}
