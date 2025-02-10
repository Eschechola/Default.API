using Microsoft.Extensions.DependencyInjection;
using Otanimes.Infrastructure.Context;

namespace Otanimes.IoC.Middlewares;

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