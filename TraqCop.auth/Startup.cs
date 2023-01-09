using DbUp;
using DbUp.Engine.Output;
using DbUp.Helpers;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using TraqCop.auth.Dapper;
using TraqCop.auth.DI;
using TraqCop.auth.Extensions;
using TraqCop.auth.Helpers;
using TraqCop.auth.Interface;

namespace TraqCop.auth
{
    public static partial class Startup
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            // swagger documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            builder.Services.StartUpExtensionsService(builder.Configuration, xmlPath, "Identity Provider API");
            builder.Services.AddHttpClient();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            builder.Services.AddSingleton<IDbConnection>(db =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Default");
                var connection = new SqlConnection(connectionString);
                return connection;
            });

           // builder.Services.AddScoped<ErpFilterException>();

            return builder;
        }
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseSwagger();
            //  app.UseSwaggerUI();
            app.StartUpExtensionsAppBuilder("Identity Provider API");
            app.MapControllers();

            TableMigrationScript(app);
            StoredProcedureMigrationScript(app);
            WebHelpers.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());
            ServiceLocator.SetLocatorProvider(app.Services);
            app.OpenIddictSeeder();

            return app;
        }

        /// <summary>
        /// Sql migration for table Schema
        /// </summary>
        public static void TableMigrationScript(this WebApplication app)
        {
            string dbConnStr = app.Configuration.GetConnectionString("Default");
            EnsureDatabase.For.SqlDatabase(dbConnStr);

            var upgrader = DeployChanges.To.SqlDatabase(dbConnStr)
            .WithScriptsFromFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql", "Tables"))
            .WithTransactionPerScript()
            .JournalToSqlTable("dbo", "TableMigration")
             .LogToConsole()
            .LogTo(new SerilogDbUpLog(app.Logger))
            .WithVariablesDisabled()
            .Build();

            upgrader.PerformUpgrade();
        }

        /// <summary>
        /// Sql migration for stored procedure
        /// </summary>
        public static void StoredProcedureMigrationScript(this WebApplication app)
        {
            string dbConnStr = app.Configuration.GetConnectionString("Default");
            EnsureDatabase.For.SqlDatabase(dbConnStr);

            var upgrader = DeployChanges.To.SqlDatabase(dbConnStr)
            .WithScriptsFromFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sql", "Sprocs"))
            .WithTransactionPerScript()
            .JournalTo(new NullJournal())
            .JournalToSqlTable("dbo", "SprocsMigration")
            .LogTo(new SerilogDbUpLog(app.Logger))
            .LogToConsole()
            .Build();

            upgrader.PerformUpgrade();
        }
    }
    public class SerilogDbUpLog : IUpgradeLog
    {
        private readonly ILogger _logger;

        public SerilogDbUpLog(ILogger logger)
        {
            _logger = logger;
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.LogError(format, args);
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.LogInformation(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.LogWarning(format, args);
        }
    }
}

