using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MommaDinner.Application.Common.Interfaces.Authentication;
using MommaDinner.InfraStructure.Authentication;
using MommaDinner.Application.Common.Interfaces.Services;
using MommaDinner.InfraStructure.Services;
using MommaDinner.Application.Common.Interfaces.Persistence;
using MommaDinner.InfraStructure.Persistence;

namespace MommaDinner.InfraStructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}