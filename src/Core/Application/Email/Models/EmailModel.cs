﻿namespace Application.Email.Models;

public class EmailModel : Model
{
    //Properties
    public EmailType Type { get; set; }
    public EmailStatus EmailStatus { get; set; }
    
    public required string Body { get; set; }
    public required string Subject { get; set; }

    public required string Sender { get; set; }
    public List<string> Recipients { get; set; } = [];

    //Methods
    public static EmailModel CreateFromEntity(EmailEntity emailEntity)
    {
        return new EmailModel
        {
            EntityId = emailEntity.Id,
            CreatedAt = emailEntity.CreatedAt,
            ModifiedAt = emailEntity.ModifiedAt,
            Type = emailEntity.Type,
            EmailStatus = emailEntity.EmailStatus,
            Body = emailEntity.Body.Value,
            Subject = emailEntity.Subject.Value,
            Sender = emailEntity.Sender.Value,
            Recipients = emailEntity.Recipients.Select(r => r.Value).ToList(),
        };
    }

}
