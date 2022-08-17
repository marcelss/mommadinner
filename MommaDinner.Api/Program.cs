using MommaDinner.Application;
using MommaDinner.InfraStructure;
using MommaDinner.Api;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
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