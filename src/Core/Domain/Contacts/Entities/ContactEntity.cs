namespace Domain.Contacts.Entities;

public class ContactEntity : Entity
{
    //Properties
    public string Name { get; }
    public EmailAddress EmailAddress { get; }


    //Construction
    public ContactEntity(){ }
    private ContactEntity(string name, EmailAddress emailAddress)
    {
        Name = name;
        EmailAddress = emailAddress;
    }

    public static ContactEntity Create(string name, EmailAddress emailAddress)
    {
        return new ContactEntity(name, emailAddress); 
    }

}
