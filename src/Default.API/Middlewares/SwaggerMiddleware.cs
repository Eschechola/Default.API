using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Default.API.Middlewares;

public static class SwaggerMiddleware
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(sgo =>
        {
            sgo.CustomSchemaIds(type => type.ToString());
            sgo.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Otanimes API",
                Version = "V1",
                Description = "Otanimes API build with .NET 9 from Otastore LTDA.",
                TermsOfService = new Uri("https://otanimes.com.br/terms"),
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/license/mit")
                },
                Contact = new OpenApiContact
                {
                    Name = "Otanimes Support",
                    Email = "suporte@otastore.com.br",
                    Url = new Uri("https://otanimes.com.br/contact")
                }
            });
            sgo.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            sgo.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    }, []
                }
            }); 
        });
    }
}