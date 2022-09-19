using Gallerio.Web.Client.Options;
using Microsoft.Extensions.Options;

namespace Gallerio.Web.Client.Services.BackendProvider
{
    public class BackendProvider : IBackendProvider
    {
        private Uri _baseUri;

        public BackendProvider(IOptions<BackendOptions> backendOptions)
        {
            _baseUri = new Uri(backendOptions.Value.BaseUri);
        }

        public Uri GetUri(string relativeUri)
        {
            return new Uri(_baseUri, relativeUri);
        }
    }
}
