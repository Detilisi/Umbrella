﻿using Domain.User.Entities;
using Domain.User.ValueObjects;

namespace Application.User.Features.Commands.RegisterUser;

public class RegisterUserCommandHandler(IApplicationDbContext dbContext, IEmailFetcher emailFetcher) : IRequestHandler<RegisterUserCommand, Result<int>>
{
    //Fields
    private readonly IEmailFetcher _emailFetcher = emailFetcher;
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            //Connect to email server
            var connectResult = await _emailFetcher.ConnectAsync(request.EmailAddress, request.EmailPassword, cancellationToken);
            if (connectResult.IsFailure) return Result.Failure<int>(connectResult.Error);
            _emailFetcher.Dispose();

            //Save loaded emails to database
            var userEntity = UserEntity.Create
            (
                EmailAddress.Create(request.EmailAddress),
                EmailPassword.Create(request.EmailPassword),
                request.UserName
            );

            _dbContext.Users.Add(userEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(userEntity.Id);
        }
        catch (Exception ex)
        {
            var error = new Error($"{this}.Failed", ex.Message);
            return Result.Failure<int>(error);
        }
        
    }
}
