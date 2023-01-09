using TraqCop.auth;
using TraqCop.auth.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .RegisterServices()
    .RegisterServices();

//Generic way to call serilog
builder.SerilogExtensions(args);

builder.Configuration.AddEnvironmentVariables();
builder.ConfigureAuth();


var app = builder.Build();

app.ConfigureMiddleware();
app.Run();
