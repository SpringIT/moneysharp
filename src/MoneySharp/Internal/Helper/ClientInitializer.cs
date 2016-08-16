using MoneySharp.Contract.Model;
using MoneySharp.Contract.Settings;
using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public class ClientInitializer : IClientInitializer
    {
        private readonly ISettingsProvider _settingsProvider;

        public ClientInitializer(ISettingsProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        private IAuthenticationSettings _authenticationSettings;
        //Lazy loading 
        private IAuthenticationSettings AuthenticationSettings => _authenticationSettings ??
                                                          (_authenticationSettings = _settingsProvider.GetAuthenticationSettings());

        private IUrlSettings _urlSettings;
        //Lazy loading 
        private IUrlSettings UrlSettings => _urlSettings ??
                                                          (_urlSettings = _settingsProvider.GetUrlSettings());

        //Lazy loading 
        private IRestClient _client;

        public IRestClient Get()
        {
            if (_client == null)
            {
                _client = new RestClient($"{UrlSettings.Url}/api/{UrlSettings.Version}/{UrlSettings.AdministrationId}");
                _client.AddDefaultHeader("Authorization", $"Bearer {AuthenticationSettings.Token}");
            }
       
            return _client;
        }
    }
}
