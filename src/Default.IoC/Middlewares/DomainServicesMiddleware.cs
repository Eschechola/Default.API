using Default.Domain.Interfaces.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Default.DomainServices;

namespace Default.IoC.Middlewares;

public static class DomainServicesMiddleware
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IDomainServiceAssemblyMaker>()
            .AddClasses(cl => cl.AssignableTo<IDomainService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}