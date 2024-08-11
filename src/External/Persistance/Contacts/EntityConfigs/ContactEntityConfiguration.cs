namespace Persistence.Contacts.EntityConfigs;

public class ContactEntityConfiguration : IEntityTypeConfiguration<ContactEntity>
{
    public void Configure(EntityTypeBuilder<ContactEntity> builder)
    {
        builder.ToTable(nameof(UserEntity));
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.Email).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.OwnsOne(e => e.Email, contactBuilder => 
            { contactBuilder.Property(email => email.Value).HasColumnName(nameof(UserEntity.EmailAddress)); }
        );
    }
}
