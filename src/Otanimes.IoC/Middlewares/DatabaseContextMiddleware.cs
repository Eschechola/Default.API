using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Otanimes.Infrastructure.Context;

namespace Otanimes.IoC.Middlewares;

public static class DatabaseContextMiddleware
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OtanimesContext>(options =>
        {
            options.UseSqlServer(connectionString, 
                providerOptions => providerOptions.EnableRetryOnFailure());
        });

        return services;
    }
}