using Domain.Common.ValueObjects;

namespace Persistence.User.EntityConfigs;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(UserEntity));
        builder.Property(e => e.UserName).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");

        ConfigureValueObjects(builder);
    }

    //Helper methods
    private void ConfigureValueObjects(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .OwnsOne(e => e.EmailAddress, emailBuilder =>{ emailBuilder.Property(email => email.Address).HasColumnName(nameof(EmailAddress));})
            .OwnsOne(e => e.EmailPassword, passwordBuilder =>{ passwordBuilder.Property(password => password.Key).HasColumnName(nameof(EmailPassword));});
    }
}
