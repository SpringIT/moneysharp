using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public interface IRequestHelper
    {
        IRestRequest BuildRequest(string resource, Method method, object bodyData = null, string additional = null);
        void CheckResult(IRestResponse response);
    }
}