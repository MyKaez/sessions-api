using Application.Installers;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Installers;

public class RepositoryInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ISessionRepository, SessionRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IConnectionRepository, ConnectionRepository>();
    }
}