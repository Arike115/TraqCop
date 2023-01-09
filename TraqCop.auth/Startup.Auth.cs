using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using TraqCop.auth.Configs;
using TraqCop.auth.Ef;
using TraqCop.auth.Model.OpenIddict;
using TraqCop.auth.OpenIddict;
using static OpenIddict.Abstractions.OpenIddictConstants;


namespace TraqCop.auth
{
    public static partial class Startup
    {
        public static void OpenIddictSeeder(this WebApplication app)
        {

            Task.Run(app.SeedOpenIddictClient).GetAwaiter().GetResult();
            //using (var scope = builder.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{

            // await builder.SeedOpenIddictClient();
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureAuth(this WebApplicationBuilder builder)
        {
            var authSettings = new AuthSettings();
            builder.Configuration.Bind(nameof(AuthSettings), authSettings);

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
            });

            var tokenExpiry = TimeSpan.FromMinutes(authSettings.TokenExpiry);
            var publicUrl = builder.Configuration.GetSection("Auth").GetValue<string>("PublicHost");

            builder.Services.AddOpenIddict()
                .AddDefaultAuthorizationController(options =>
                    options.SetConfiguration(builder.Configuration.GetSection("OpenId"))
                            .SetPublicUrl(publicUrl))
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<TraqCopDbContext>()
                         .ReplaceDefaultEntities<TraqOpenIddictApplication,
                         TraqOpenIddictAuthorization, TraqOpenIddictScope, TraqOpenIddictToken, Guid>();
                })
                //.AddServer(options =>
                //{
                //    //options.AddEventHandler<ApplyTokenResponseContext>(builder =>
                //    //builder.UseSingletonHandler<MyApplyTokenResponseHandler>());

                //    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Address, Scopes.Phone,
                //        Scopes.Roles, Scopes.OfflineAccess, Scopes.OpenId);

                //    if (builder.Environment.IsDevelopment())
                //    {
                //        options.UseAspNetCore(configure =>
                //        {
                //            configure.DisableTransportSecurityRequirement();
                //        });

                //        // Register the signing and encryption credentials.
                //        options.AddDevelopmentEncryptionCertificate()
                //               .AddDevelopmentSigningCertificate();
                //    }
                //    else
                //    {
                //        byte[] rawData = File.ReadAllBytes(Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "app_data", "walure-bo.pfx"));
                //        var x509Certificate = new X509Certificate2(rawData,
                //            authSettings.Password,
                //            X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

                //        options.AddEncryptionCertificate(x509Certificate).AddSigningCertificate(x509Certificate);
                //    }

                //    options.SetTokenEndpointUris("/api/auth/token")
                //    .SetUserinfoEndpointUris("/api/auth/userinfo")
                //    .SetRevocationEndpointUris("/api/auth/revoke")
                //    .AllowRefreshTokenFlow()
                //    .AcceptAnonymousClients()
                //    .AllowPasswordFlow()
                //    .SetAccessTokenLifetime(tokenExpiry)
                //    .SetIdentityTokenLifetime(tokenExpiry)
                //    .SetRefreshTokenLifetime(tokenExpiry);


                //    options.UseAspNetCore()
                //         .EnableAuthorizationEndpointPassthrough()
                //         .EnableLogoutEndpointPassthrough()
                //         .EnableStatusCodePagesIntegration()
                //         .EnableTokenEndpointPassthrough();

                //    options.DisableAccessTokenEncryption();

                //})
                // if you want to secure some controllers/actions within the same project with JWT

                // Register the OpenIddict validation components.
                .AddValidation(options =>
                {
                    // Import the configuration from the local OpenIddict server instance.
                    options.UseLocalServer();

                    // Register the ASP.NET Core host.
                    options.UseAspNetCore();
                })
                ;
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = tokenExpiry;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            });

        }
    }
}
