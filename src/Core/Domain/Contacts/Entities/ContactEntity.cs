namespace Domain.Contacts.Entities;

public class ContactEntity : Entity
{
    //Properties
    public string Name { get; }
    public EmailAddress Email { get; }


    //Construction
    public ContactEntity(){ }
    private ContactEntity(string name, EmailAddress emailAddress)
    {
        Name = name;
        Email = emailAddress;
    }

    public static ContactEntity Create(string name, EmailAddress emailAddress)
    {
        return new ContactEntity(name, emailAddress); 
    }

}
