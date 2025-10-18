using CallWatch.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CallWatch.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    var dbConnection = configuration.GetConnectionString("Connection");
    services.AddDbContext<CallWatchDbContext>(options =>
    {
      options.UseSqlite(dbConnection);
    });
  }
}
