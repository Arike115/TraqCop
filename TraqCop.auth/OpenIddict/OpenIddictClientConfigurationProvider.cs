using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraqCop.auth.OpenIddict
{

    /// <summary>
    /// Provides configurations for OpenId clients
    /// </summary>
    public interface IOpenIddictClientConfigurationProvider
    {
        /// <summary>
        /// Returns configuration for passed clientId or null if client is not found. 
        /// </summary>
        OpenIddictClientConfiguration? GetConfiguration(string clientId);

        /// <summary>
        /// Returns configuration for passed clientId (and `true` as returnor null if client is not found. 
        /// </summary>
        bool TryGetConfiguration(string clientId, out OpenIddictClientConfiguration configuration);

        /// <summary>
        /// Returns configuration for all clients
        /// </summary>
        IList<OpenIddictClientConfiguration> GetAllConfigurations();
    }

    /// <summary>
    /// Provides configuration for openiddict client (
    /// </summary>
    public class OpenIddictClientConfigurationProvider : IOpenIddictClientConfigurationProvider
    {
        private readonly Dictionary<string, OpenIddictClientConfiguration> _clients;

        /// <summary>
        /// Constructs OpenIddictClientConfigurationProvider 
        /// </summary>
        public OpenIddictClientConfigurationProvider(IOptions<OpenIddictConfiguration> configuration)
        {
            _clients = configuration.Value.Clients.Values.ToDictionary(x => x.ClientId);
        }

        /// <inheritdoc />
        public OpenIddictClientConfiguration GetConfiguration(string clientId)
        {
            return _clients[clientId];
        }

        /// <inheritdoc />
        public bool TryGetConfiguration(
            string clientId,
            out OpenIddictClientConfiguration configuration
        )
        {
            return _clients.TryGetValue(clientId, out configuration);
        }

        /// <inheritdoc />
        public IList<OpenIddictClientConfiguration> GetAllConfigurations()
        {
            return _clients.Values.ToList();
        }
    }
}
