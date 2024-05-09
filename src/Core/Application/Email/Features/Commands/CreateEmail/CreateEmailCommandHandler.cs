﻿using Application.Email.Models;

namespace Application.Email.Features.Commands.CreateEmail;

public class CreateEmailCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateEmailCommand, Result<int>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle methods
    public async Task<Result<int>> Handle(CreateEmailCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var emailEntity = EmailEntity.Create
            (
                EmailAddress.Create(request.Sender),
                request.Recipients.Select(EmailAddress.Create).ToList(),
                EmailSubjectLine.Create(request.Subject),
                EmailBodyText.Create(request.Body)
            );

            emailEntity.CreatedAt = request.CreatedAt;

            _dbContext.Emails.Add(emailEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(emailEntity.Id);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
        
    }
}
