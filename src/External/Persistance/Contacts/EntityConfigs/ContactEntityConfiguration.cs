namespace Persistence.Contacts.EntityConfigs;

public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.ToTable(nameof(ContactEntity));
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.OwnsOne(e => e.EmailAddress, contactBuilder => 
            { contactBuilder.Property(email => email.Value).HasColumnName(nameof(ContactEntity.EmailAddress)); }
        );
    }
}
