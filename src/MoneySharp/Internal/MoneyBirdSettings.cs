using MoneySharp.Contract.Settings;

namespace MoneySharp.Internal
{
    internal class MoneyBirdSettings : ISettings
    {
        public string Token { get; set; }
        public string Url { get; set; }
        public string Version { get; set; }
        public string AdministrationId { get; set; }
    }
}