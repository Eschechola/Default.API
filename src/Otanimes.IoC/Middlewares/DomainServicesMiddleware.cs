using Microsoft.Extensions.DependencyInjection;
using Otanimes.Domain.Interfaces.DomainServices;
using Otanimes.DomainServices;

namespace Otanimes.IoC.Middlewares;

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