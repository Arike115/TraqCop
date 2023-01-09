using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace TraqCop.auth.Extensions
{
    public static class StartUpExtensions
    {
        public static IServiceCollection StartUpExtensionsService(this IServiceCollection services, ConfigurationManager configuration, string xmlPath, string Name)
        {

            #region Swagger
            services.AddSwaggerGen(option =>
            {
                option.IncludeXmlComments(xmlPath);

                option.SwaggerDoc("v1", new OpenApiInfo { Title = Name, Version = "v1" });

                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter 'Bearer'  and then your token in the text input " +
                    "below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

            });
            #endregion
            return services;
        }
        public static IApplicationBuilder StartUpExtensionsAppBuilder(this WebApplication app, string Name)
        {

            #region Swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentname}/swagger.json";

            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", Name);
                c.DocExpansion(DocExpansion.None);
            });
            #endregion

            // TableMigrationScript(app);
            //   StoredProcedureMigrationScript(app);
            return app;
        }
        public static void SerilogExtensions(this WebApplicationBuilder builder, string[] args)
        {
            if (builder.Environment.IsDevelopment())
            {
                string subenv = Environment.GetEnvironmentVariable("ASPNET_SUBENVIRONMENT");
                subenv ??= builder.Configuration["ASPNET_SUBENVIRONMENT"];

                if (!string.IsNullOrEmpty(subenv))
                {
                    string subEnvironmentJsonRootFilePath = Path.Combine(builder.Environment.ContentRootPath, $"appsettings.Development.{subenv}.json");

                    if (File.Exists(subEnvironmentJsonRootFilePath))
                        builder.Configuration.AddJsonFile($"appsettings.Development.{subenv}.json", optional: true, reloadOnChange: true);
                };
            };

            builder.Host.ConfigureDefaults(args)
                  .UseSerilog((hostingContext, loggerConfiguration) =>
                  {
                      var logConfig = loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                     .Enrich.FromLogContext()
                     .WriteTo.File(@"logs\log.txt", rollingInterval: RollingInterval.Day,
                     restrictedToMinimumLevel: LogEventLevel.Information,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                   shared: true);

                      //if (!hostingContext.HostingEnvironment.IsDevelopment())
                      //{
                      //    logConfig.WriteTo.Sentry(o =>
                      //    {
                      //        o.Environment = hostingContext.HostingEnvironment.EnvironmentName;
                      //        // Debug and higher are stored as breadcrumbs (default is Information)
                      //        o.MinimumBreadcrumbLevel = LogEventLevel.Information;
                      //        // Warning and higher is sent as event (default is Error)
                      //        o.MinimumEventLevel = LogEventLevel.Error;
                      //        o.Dsn = hostingContext.Configuration.GetValue<string>("AppSettings:SentryUrl");
                      //    });
                      //}
                  });

        }
    }
}
