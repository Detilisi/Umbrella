namespace Domain.UnitTests.Email.ValueObjectsTests;

[TestFixture]
public class EmailSubjectLineTests
{
    [Test]
    public void Create_ThrowsEmptyValueException_ForEmptyString()
    {
        Assert.Throws<EmptyValueException>(() => EmailSubjectLine.Create(""));
    }

    [Test]
    public void Create_ThrowsEmptyValueException_ForWhitespaceString()
    {
        Assert.Throws<EmptyValueException>(() => EmailSubjectLine.Create(" "));
    }

    [Test]
    public void Create_ThrowsSubjectLineTooLongException_ForExceedingMaxSubjectLength()
    {
        var excessivelyLongSubject = new string('x', EmailSubjectLine.MAXSUBJECTLINELENGTH + 1);
        Assert.Throws<SubjectLineTooLongException>(() => EmailSubjectLine.Create(excessivelyLongSubject));
    }

    [Test]
    public void Create_ThrowsInvalidSubjectException_ForNewlineCharacters()
    {
        string subjectWithNewline = "This is a subject\nwith a newline";
        Assert.Throws<InvalidSubjectException>(() => EmailSubjectLine.Create(subjectWithNewline));

        string subjectWithCarriageReturn = "This is a subject\rwith a carriage return";
        Assert.Throws<InvalidSubjectException>(() => EmailSubjectLine.Create(subjectWithCarriageReturn));
    }

    [Test]
    public void Create_SetsText_ForValidSubject()
    {
        var subjectText = "This is a valid subject line.";
        var subjectLine = EmailSubjectLine.Create(subjectText);
        Assert.That(subjectLine.Text, Is.EqualTo(subjectText));
    }

    [Test]
    public void ToString_ReturnsText()
    {
        var subjectText = "This is a valid subject line.";
        var subjectLine = EmailSubjectLine.Create(subjectText);
        Assert.That(subjectLine.ToString(), Is.EqualTo(subjectText));
    }
}
