using CallWach.Domain.Entities;

namespace CallWatch.Domain.Repositories;

public interface IUsersReadOnlyRepository
{
  Task<User?> GetByNumber(string number);
  // Task<User> Create(User user);
}
