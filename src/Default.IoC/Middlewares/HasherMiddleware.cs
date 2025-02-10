using System.Text;
using EscNet.IoC.Hashers;
using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Default.IoC.Middlewares;

public static class HasherMiddleware
{
    public static IServiceCollection AddHasher(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new Argon2Config
        {
            Type = Argon2Type.DataIndependentAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = int.Parse(configuration["Hash:TimeCost"]),
            Threads = int.Parse(configuration["Hash:Threads"]),
            Lanes = int.Parse(configuration["Hash:Lanes"]),
            Salt = Encoding.UTF8.GetBytes(configuration["Hash:Salt"]),
        };
        
        services.AddArgon2IdHasher(config);
        
        return services;
    }
}