using Microsoft.EntityFrameworkCore;
using TraqCop.auth.Model.Maps;
using TraqCop.auth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TraqCop.auth.Model.OpenIddict;

namespace TraqCop.auth.Ef
{
    public class TraqCopDbContext : IdentityDbContext<UserModel, AppRoles, Guid, UserClaims, AppUserRoles, UserLogins, AppRoleClaims, AppUserTokens>
    {
        public TraqCopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<VisitorModel>? visitors { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppRoleClaimMap());
            modelBuilder.ApplyConfiguration(new AppRoleMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new AppUserRoleMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new AppUserTokenMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
           

            modelBuilder.UseOpenIddict<TraqOpenIddictApplication,
                TraqOpenIddictAuthorization, TraqOpenIddictScope,
                TraqOpenIddictToken, Guid>();

            var typesToRegister = typeof(BaseEntity).Assembly.GetTypes().Where(type => !String.IsNullOrEmpty(type.Namespace))
          .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));

            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.ApplyConfiguration((dynamic)configurationInstance);
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}


