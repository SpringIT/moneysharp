using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public interface IRequestHelper
    {
        IRestRequest BuildRequest(string uri, Method method, object bodyData = null);
        void CheckResult(IRestResponse response);
    }
}