using CallWach.Domain.Entities;
using CallWatch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CallWatch.Infrastructure.DataAccess.Repositories;

public class UsersRepository(CallWatchDbContext dbContext) : IUsersReadOnlyRepository
{
  private readonly CallWatchDbContext _dbContext = dbContext;

  public async Task<User?> GetByName(string responsibleName)
  {
    var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Name == responsibleName);
    await _dbContext.SaveChangesAsync();
    return new User
    {
      Id = user!.Id,
      Name = user!.Name,
      Number = user!.Number
    };
  }
}
