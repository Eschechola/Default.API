using System.Text;
using Default.API.Authorization.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Default.API.Middlewares;

public static class SecurityMiddleware
{
    public static void AddBearerAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateIssuer = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"])),
                    ValidateIssuerSigningKey = true
                };
            });
        
        services.AddAuthorization();
        services.AddTokenService();
    }
    
    private static void AddTokenService(this IServiceCollection services)
        => services.AddScoped<ITokenService, TokenService>();
}