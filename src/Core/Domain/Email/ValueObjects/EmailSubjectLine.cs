using Domain.Email.Exceptions;

namespace Domain.Email.ValueObjects;

public class EmailSubjectLine : ValueObject
{
    public const int MAXSUBJECTLINELENGTH = 200;

    //Properties
    public string Text { get; private set; }

    //Contructions
    private EmailSubjectLine(string value) : base()
    {
        Text = value;
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
    public override string ToString() => Text;
    protected override IEnumerable<object> GetEqualityComponents() { yield return Text; }
}
