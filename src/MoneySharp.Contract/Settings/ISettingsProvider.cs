using MoneySharp.Contract.Model;

namespace MoneySharp.Contract.Settings
{
    public interface ISettingsProvider
    {
        IAuthenticationSettings GetAuthenticationSettings();
        IUrlSettings GetUrlSettings();
    }
}
