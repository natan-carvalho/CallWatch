using CallWatch.Application.UseCases;
using CallWatch.Application.UseCases.GetAllCalls;
using CallWatch.Application.UseCases.Login;
using Microsoft.Extensions.DependencyInjection;

namespace CallWatch.Application;

public static class DependencyInjectionExtension
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddScoped<ILoginUseCase, LoginUseCase>();
    services.AddScoped<IGetAllCallsUseCase, GetAllCallsUseCase>();

    return services;
  }
}
