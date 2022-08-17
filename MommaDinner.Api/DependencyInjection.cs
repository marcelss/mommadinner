using Microsoft.AspNetCore.Mvc.Infrastructure;
using MommaDinner.Api.Common.Errors;
using MommaDinner.Api.Commom.Mapping;

namespace MommaDinner.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        //Global Error Handling - Via exception fitler attribute
        //services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
        //Global Error Handling - Custom Problem Details Factory
        services.AddSingleton<ProblemDetailsFactory, MommaDinnerProblemDetailsFactory>();
        services.AddMappings();
        return services;
    }
}
