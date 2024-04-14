using Domain.User.Entities;
using Domain.User.ValueObjects;

namespace Application.User.Features.Commands.RegisterUser;

public class RegisterUserCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<RegisterUserCommand, Result<int>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = UserEntity.Create
        (
            EmailAddress.Create(request.User.EmailAddress), 
            EmailPassword.Create(request.User.EmailPassword), 
            request.User.UserName
        );

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return (Result<int>)await Task.FromResult(Result.OK<int>(user.Id));
    }
}
