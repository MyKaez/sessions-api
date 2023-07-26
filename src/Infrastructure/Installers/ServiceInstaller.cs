using Application.Installers;
using Application.Services;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Installers;

public class ServiceInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IUpdateService, UpdateService>();
        services.AddTransient<IConnectionService, ConnectionService>();
        services.AddTransient<ISessionService, SessionService>();
        services.AddTransient<IUserService, UserService>();
    }
}