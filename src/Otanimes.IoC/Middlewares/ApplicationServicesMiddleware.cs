using Microsoft.Extensions.DependencyInjection;
using Otanimes.ApplicationServices;
using Otanimes.Domain.Interfaces.ApplicationServices;
using Otanimes.DomainServices;
using Scrutor;

namespace Otanimes.IoC.Middlewares;

public static class ApplicationServicesMiddleware
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblyOf<IApplicationServiceAssemblyMaker>()
            .AddClasses(cl => cl.AssignableTo<IApplicationService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}