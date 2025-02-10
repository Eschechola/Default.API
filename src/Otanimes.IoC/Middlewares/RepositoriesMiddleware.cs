using Microsoft.Extensions.DependencyInjection;
using Otanimes.Infrastructure;
using Scrutor;

namespace Otanimes.IoC.Middlewares;

public static class RepositoriesMiddleware
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IInfrastructureAssemblyMaker>()
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}