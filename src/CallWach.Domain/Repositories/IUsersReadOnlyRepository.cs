using CallWach.Domain.Entities;

namespace CallWatch.Domain.Repositories;

public interface IUsersReadOnlyRepository
{
  Task<User?> GetByName(string responsibleName);
}
