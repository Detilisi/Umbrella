namespace Domain.Contacts.Entities;

public class ContactEntity : Entity
{
    //Properties
    public string Name { get; }
    public EmailAddress EmailAddress { get; }


    //Construction
    public ContactEntity(){ }//Required for Entity framework
    private ContactEntity(int id, string name, EmailAddress emailAddress) : base(id)
    {
        Name = name;
        EmailAddress = emailAddress;
    }

    public static ContactEntity Create(string name, EmailAddress emailAddress)
    {
        return new ContactEntity(0, name, emailAddress); 
    }

}
