using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TraqCop.auth.Components.Policy;
using TraqCop.auth.Configs;
using TraqCop.auth.Ef;
using TraqCop.auth.Model;

namespace TraqCop.auth
{
    public static partial class Startup
    {
        public static WebApplicationBuilder RegisterDI(this WebApplicationBuilder builder)
        {

            builder.Services.AddDbContext<TraqCopDbContext>(options =>
            {
                var connectionstring = builder.Configuration.GetConnectionString("Default");
                options
                //.UseLazyLoadingProxies()
                .UseSqlServer(connectionstring, b =>
                {
                    b.EnableRetryOnFailure();
                });
            });

            builder.Services.AddIdentity<UserModel, AppRoles>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<TraqCopDbContext>()
         .AddDefaultTokenProviders();

            //builder.Services.AddTransient<IUserService, UserService>();
            //builder.Services.AddTransient<IRoleService, RoleService>();
            builder.Services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

            var appSettings = new AppSettings();
            builder.Configuration.Bind(nameof(AppSettings), appSettings);
            builder.Services.AddSingleton(appSettings);

            return builder;
        }
    }
}
