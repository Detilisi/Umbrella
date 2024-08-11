using Domain.Contacts.Entities;

namespace Application.Contatcs.Dtos;

public class ContactDto : Dto
{
    //Properties
    public required string Name { get; set; } = string.Empty;
    public required string EmailAddress { get; set; } = string.Empty;

    //Helper
    internal static ContactDto CreateFromEntity(ContactEntity contactEntity)
    {
        return new ContactDto() 
        {
            Name = contactEntity.Name,
            EmailAddress = contactEntity.Email.Value 
        };
    }
}
