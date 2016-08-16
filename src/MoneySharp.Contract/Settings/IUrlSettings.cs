namespace MoneySharp.Contract.Model
{
    public interface IUrlSettings
    {
        string Url { get; set; }
        string Version { get; set; }
        string AdministrationId { get; set; }
    }
}