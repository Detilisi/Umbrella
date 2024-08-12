using Domain.Email.ValueObjects;

namespace Persistence.Email.EntityConfigs;

public class EmailEntityConfiguration : IEntityTypeConfiguration<EmailEntity>
{
    //Config method
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.ToTable(nameof(EmailEntity));
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.EmailStatus).IsRequired();
        builder.Property(e => e.SenderName).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");

        ConfigureValueObjects(builder);
    }

    //Helper methods
    private static void ConfigureValueObjects(EntityTypeBuilder<EmailEntity> builder)
    {
        builder
            .OwnsOne(e => e.Sender, emailBuilder => { emailBuilder.Property(email => email.Value).HasColumnName(nameof(EmailEntity.Sender)); })
            .OwnsOne(e => e.Recipient, emailBuilder => { emailBuilder.Property(email => email.Value).HasColumnName(nameof(EmailEntity.Recipient)); })
            .OwnsOne(e => e.Body, bodyBuilder =>{bodyBuilder.Property(body => body.Value).HasColumnName(nameof(EmailEntity.Body)).HasMaxLength(EmailBodyText.MAXBODYLENGTH);})
            .OwnsOne(e => e.Subject, subjectBuilder =>{subjectBuilder.Property(subject => subject.Value).HasColumnName(nameof(EmailEntity.Subject)).HasMaxLength(EmailSubjectLine.MAXSUBJECTLINELENGTH);});
    }
}
