﻿namespace Persistence.User.EntityConfigs;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    //Config method
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(nameof(UserEntity));
        builder.Property(e => e.UserName).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");

        ConfigureValueObjects(builder);
    }

    //Helper methods
    private static void ConfigureValueObjects(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .OwnsOne(e => e.EmailAddress, emailBuilder => { emailBuilder.Property(email => email.Value).HasColumnName(nameof(UserEntity.EmailAddress)); })
            .OwnsOne(e => e.EmailPassword, passwordBuilder => { passwordBuilder.Property(password => password.Value).HasColumnName(nameof(UserEntity.EmailPassword)); });
    }
}
