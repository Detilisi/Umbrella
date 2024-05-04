﻿using Domain.Common.ValueObjects;
using Domain.Email.ValueObjects;
using System.Text.Json;

namespace Persistence.Email.EntityConfigs;

public class EmailEntityConfiguration : IEntityTypeConfiguration<EmailEntity>
{
    //Config method
    public void Configure(EntityTypeBuilder<EmailEntity> builder)
    {
        builder.ToTable(nameof(EmailEntity));
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.EmailStatus).IsRequired();
        builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(e => e.ModifiedAt).HasDefaultValueSql("GETUTCDATE()");

        ConfigureValueObjects(builder);
    }

    //Helper methods
    private static void ConfigureValueObjects(EntityTypeBuilder<EmailEntity> builder)
    {
        builder
            .OwnsOne(e => e.Sender, emailBuilder => { emailBuilder.Property(email => email.Value).HasColumnName("Sender"); });

        builder.OwnsOne(e => e.Body, bodyBuilder =>{bodyBuilder.Property(body => body.Value).HasColumnName("Body")
            .HasMaxLength(EmailBodyText.MAXBODYLENGTH);});
        
        builder.OwnsOne(e => e.Subject, subjectBuilder =>{subjectBuilder.Property(subject => subject.Value).HasColumnName("Subject")
            .HasMaxLength(EmailSubjectLine.MAXSUBJECTLINELENGTH);});

        var jsonOption = new JsonSerializerOptions();
        builder.Property(e => e.Recipients).HasColumnName("Recipients")
            .HasConversion(v => JsonSerializer.Serialize(v, jsonOption),v => JsonSerializer.Deserialize<List<EmailAddress>>(v, jsonOption));
    }
}
