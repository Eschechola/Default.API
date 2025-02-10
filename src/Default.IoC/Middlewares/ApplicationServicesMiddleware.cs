using Microsoft.Extensions.DependencyInjection;
using Default.ApplicationServices;
using Default.Domain.Interfaces.ApplicationServices;

namespace Default.IoC.Middlewares;

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