using Microsoft.EntityFrameworkCore;

namespace Application.User.Features.Queries.LoginUser;

public class LoginUserQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<LoginUserQuery, Result<UserModel>>
{
    //Fields
    private readonly IApplicationDbContext _dbContext = dbContext;

    //Handle method
    public async Task<Result<UserModel>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        var userEntity = await _dbContext.Users.Where(
            x=>x.EmailAddress.Address == request.EmailAddress && x.EmailPassword.Key == request.EmailPassword)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (userEntity == null)
        {
            return (Result<UserModel>)await Task.FromResult(Result.Fail<UserModel>(
                new UserModel(), new Error("EntityNotFound", "User Entity not found"))
            );
        }

        var userModel = new UserModel()
        {
            EntityId = userEntity.Id,
            EmailAddress = userEntity.EmailAddress.Address,
            EmailPassword = userEntity.EmailPassword.Key,
            CreatedAt = userEntity.CreatedAt,
            ModifiedAt = userEntity.ModifiedAt,
            UserName = userEntity.UserName,
        };

        return (Result<UserModel>)await Task.FromResult(Result.OK<UserModel>(userModel));
    }
}
