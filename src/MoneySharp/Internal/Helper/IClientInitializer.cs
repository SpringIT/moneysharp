using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public interface IClientInitializer
    {
        IRestClient Get();
    }
}