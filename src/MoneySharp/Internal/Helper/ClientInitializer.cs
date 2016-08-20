using System;
using MoneySharp.Contract.Model;
using MoneySharp.Contract.Settings;
using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public class ClientInitializer : IClientInitializer
    {
        private static ISettingsProvider _settingsProvider;

        private static readonly Lazy<ISettings> Settings =
            new Lazy<ISettings>(() => _settingsProvider.GetSettings());

        private readonly Lazy<IRestClient> _client = new Lazy<IRestClient>(LoadClient);

        public ClientInitializer(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public IRestClient Get()
        {
            return _client.Value;
        }

        private static IRestClient LoadClient()
        {
            var settings = Settings.Value;
            var url = $"{settings.Url}/api/{settings.Version}/{settings.AdministrationId}/";
            var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", $"Bearer {settings.Token}");
            return client;
        }
    }
}
