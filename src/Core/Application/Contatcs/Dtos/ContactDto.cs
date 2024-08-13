namespace Application.Contatcs.Dtos;

public class ContactDto : Dto
{
    //Properties
    public required string Name { get; set; } = string.Empty;
    public required string Email { get; set; } = string.Empty;

    //Helper
    internal static ContactDto CreateFromEntity(ContactEntity contactEntity)
    {
        return new ContactDto() 
        {
            EntityId = contactEntity.Id,
            Name = contactEntity.Name,
            Email = contactEntity.EmailAddress.Value,
            CreatedAt = contactEntity.CreatedAt,
            ModifiedAt = contactEntity.ModifiedAt,
        };
    }

    internal ContactEntity ToContactEntity()
    {
        var contactEntity = ContactEntity.Create(Name, EmailAddress.Create(Email));
        contactEntity.Id = EntityId;

        return contactEntity;
    }
}
