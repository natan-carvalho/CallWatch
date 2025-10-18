using CallWach.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CallWatch.Infrastructure.DataAccess;

public class CallWatchDbContext(DbContextOptions options) : DbContext(options)
{
  public DbSet<User> Users { get; set; }
}
