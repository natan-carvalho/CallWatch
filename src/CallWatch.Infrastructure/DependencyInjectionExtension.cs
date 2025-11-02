using CallWatch.Domain.Interfaces;
using CallWatch.Domain.Repositories;
using CallWatch.Infrastructure.DataAccess;
using CallWatch.Infrastructure.DataAccess.Repositories;
using CallWatch.Infrastructure.MessageSender;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CallWatch.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    AddDbContext(services, configuration);
    AddRepository(services);
  }

  private static void AddRepository(IServiceCollection services)
  {
    services.AddScoped<IUsersReadOnlyRepository, UsersRepository>();
    services.AddScoped<IMessageSender, WhatsAppSender>();
  }

  private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
  {
    var dbConnection = configuration.GetConnectionString("Connection");
    services.AddDbContext<CallWatchDbContext>(options =>
    {
      options.UseSqlite(@"Data Source=C:\Users\natan\projects\CallWatch\src\CallWatch.Infrastructure\database\db.db");
    });
  }
}
