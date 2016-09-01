using System;
using MoneySharp.Contract.Model;
using MoneySharp.Contract.Settings;
using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public class ClientInitializer : IClientInitializer
    {
        private readonly ISettings _settings;

        private readonly Lazy<IRestClient> _client;

        public ClientInitializer(ISettings settings)
        {
            _settings = settings;
            _client = new Lazy<IRestClient>(LoadClient);
        }

        public IRestClient Get()
        {
            return _client.Value;
        }

        private IRestClient LoadClient()
        {
            var url = $"{_settings.Url}/api/{_settings.Version}/{_settings.AdministrationId}/";
            var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", $"Bearer {_settings.Token}");
            return client;
        }
    }
}
