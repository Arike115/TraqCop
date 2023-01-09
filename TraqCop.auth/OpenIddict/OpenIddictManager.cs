using OpenIddict.Core;
using TraqCop.auth.Model.OpenIddict;

namespace TraqCop.auth.OpenIddict
{
    public static class OpenIddictManager
    {
        public static async Task SeedOpenIddictClient(this WebApplication builder)
        {
            await using var scope = builder.Services.CreateAsyncScope();
            var serviceProvider = scope.ServiceProvider;

            var manager = serviceProvider.GetRequiredService<OpenIddictApplicationManager<TraqOpenIddictApplication>>();
            var publicUrl = builder.Configuration.GetSection("AuthSettings").GetValue<string>("PublicHost");
            var _clientConfigurationProvider = serviceProvider.GetRequiredService<IOpenIddictClientConfigurationProvider>();

            IList<OpenIddictClientConfiguration> clients = _clientConfigurationProvider.GetAllConfigurations();

            foreach (OpenIddictClientConfiguration client in clients)
            {
                if (!string.IsNullOrEmpty(publicUrl))
                {
                    var baseUri = new Uri(publicUrl);
                    PrependBaseUriToRelativeUris(client.RedirectUris, baseUri);
                    PrependBaseUriToRelativeUris(client.PostLogoutRedirectUris, baseUri);
                }

                var clientObject = await manager.FindByClientIdAsync(client.ClientId!)
                    .ConfigureAwait(false);
                // See OpenIddictConstants.Permissions for available permissions

                if (clientObject is null)
                {
                    await manager.CreateAsync(client).ConfigureAwait(false);
                }
                else
                {
                    if (string.IsNullOrEmpty(client.Type))
                    {
                        if (string.IsNullOrEmpty(client.ClientSecret))
                        {
                            client.Type = "public";
                        }
                        else
                        {
                            client.Type = "confidential";
                        }
                    }

                    await manager.PopulateAsync(clientObject, client).ConfigureAwait(false);
                    await manager.UpdateAsync(clientObject, client.ClientSecret ?? "")
                        .ConfigureAwait(false);
                }
            }
        }

        private static void PrependBaseUriToRelativeUris(HashSet<Uri> uris, Uri baseUri)
        {
            if (uris == null)
                return;

            List<Uri> relativeUris = uris.Where(x => !x.IsAbsoluteUri).ToList();
            foreach (var relativeUri in relativeUris)
            {
                uris.Remove(relativeUri);
                uris.Add(new Uri(baseUri, relativeUri));
            }
        }
    }
}
