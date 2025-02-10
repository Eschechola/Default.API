using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otanimes.Api;
using Otanimes.API.Middlewares;
using Otanimes.IoC.Middlewares;

namespace Otanimes.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IWebHostEnvironment environment)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        if(environment.EnvironmentName is "Local")
            builder.AddUserSecrets<Startup>();

        _configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var sqlServerConnectionString = _configuration.GetConnectionString("SqlServer")!;
    
        services.AddControllers();
        services.AddCors();
        services.AddSingleton(_ => _configuration);
        services.AddSwaggerConfiguration();
        services.AddEndpointsApiExplorer();
        services.AddHealthCheck(sqlServerConnectionString);
        services.AddDatabaseContext(sqlServerConnectionString);
        services.AddRepositories();
        services.AddDomainServices();
        services.AddApplicationServices();
        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());
        services.AddMediatorHandlers();
        services.AddBearerAuthorization(_configuration);
        services.AddHasher(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });
        }
        
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseCors();
        app.UseResponseCaching();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCustomExceptionHandler();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
    }
}