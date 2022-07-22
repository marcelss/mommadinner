using MommaDinner.Application;
using MommaDinner.InfraStructure;
using MommaDinner.Api.Middleware;
using MommaDinner.Api.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MommaDinner.Api.Common.Errors;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    
    builder.Services.AddControllers();

    //Global Error Handling - Via exception fitler attribute
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    //Global Error Handling - Custom Problem Details Factory
    builder.Services.AddSingleton<ProblemDetailsFactory, MommaDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    //Global Error Handling - Via Middleware
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    //Global Error Handling - Via error endpoint
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}