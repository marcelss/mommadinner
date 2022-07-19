using Microsoft.Extensions.DependencyInjection;
using MommaDinner.Application.Services.Authentication;

namespace MommaDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}