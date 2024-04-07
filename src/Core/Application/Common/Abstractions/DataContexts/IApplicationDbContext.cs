using Domain.Email.Entities;
using Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Abstractions.DataContexts;

public interface IApplicationDbContext
{
    //Data sets
    DbSet<UserEntity> Users { get; }
    DbSet<EmailEntity> Emails { get; }

}
