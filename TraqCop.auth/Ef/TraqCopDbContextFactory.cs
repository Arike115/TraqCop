using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using TraqCop.auth.Model.OpenIddict;

namespace TraqCop.auth.Ef
{
    public class TraqCopDbContextFactory : IDesignTimeDbContextFactory<TraqCopDbContext>
    {
        public TraqCopDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
               .Build();

            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.json", optional: true)
               .AddJsonFile("appsettings.Development.json", optional: true)
               .AddJsonFile($"appsettings.Development.{config["ASPNET_SUBENVIRONMENT"]}.json", optional: true)
               .Build();

            var builder = new DbContextOptionsBuilder<TraqCopDbContext>();
            builder.EnableSensitiveDataLogging(true);

            var connectionString = configuration["ConnectionStrings:Default"];

            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly(this.GetType().Assembly.FullName));
            builder.UseOpenIddict<TraqOpenIddictApplication,
                TraqOpenIddictAuthorization, TraqOpenIddictScope,
                TraqOpenIddictToken, Guid>();

            var dbContext = new TraqCopDbContext(builder.Options);
            return dbContext;
        }

     
    }
}
