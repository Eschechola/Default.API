using Microsoft.Extensions.DependencyInjection;
using Default.Infrastructure.Context;

namespace Default.IoC.Middlewares;

public static class HealthCheckMiddleware
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, string connectionString)
    {
        services.AddHealthChecks()
            .AddSqlServer(connectionString)
            .AddDbContextCheck<OtanimesContext>();

        return services;
    }
}