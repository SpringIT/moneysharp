namespace MoneySharp.Contract.Settings
{
    public interface ISettings
    {
        string Token { get; set; }
        string Url { get; set; }
        string Version { get; set; }
        string AdministrationId { get; set; }
    }
}
