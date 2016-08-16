using System.Collections.Generic;
using System.Net;
using MoneySharp.Contract.Exceptions;
using RestSharp;

namespace MoneySharp.Internal.Helper
{
    public class RequestHelper : IRequestHelper
    {
        // BUG Moneybird. Can't handle .json for updates and deletes
        private const string UpdateExtension = ".xml";
        private const string Extension = ".json";

        public IRestRequest BuildRequest(string uri, Method method, object bodyData = null)
        {
            var totalUrl = GetUrl(uri, method);
            var request = new RestRequest(totalUrl) {Method = method};

            if (bodyData != null && (method == Method.POST || method == Method.PUT || method == Method.PATCH)) request.AddJsonBody(bodyData);

            return request;
        }

        public void CheckResult(IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    return;
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedMoneybirdException("Unauthorized by moneybird. See ISettingsProvider GetAuthenticationSettings");
                case HttpStatusCode.Forbidden:
                    throw new RateLimitExceededException("Moneybird can process 350 calls each hour");
                case HttpStatusCode.NotFound:
                    throw new KeyNotFoundException();
                default:
                    throw new MoneySharpException($"The server returned '{response.StatusDescription}' with the status code {response.StatusCode} ({response.StatusCode:d}). Logging: {response.Content}");
            }
        }

        private string GetUrl(string uri, Method method)
        {
            switch (method)
            {
                case Method.GET:
                    return $"{uri}{Extension}";
                case Method.POST:
                    return $"{uri}{Extension}";
                case Method.PUT:
                    return $"{uri}{UpdateExtension}";
                case Method.PATCH:
                    return $"{uri}{UpdateExtension}";
                case Method.DELETE:
                    return $"{uri}{Extension}";
                default:
                    return $"{uri}{Extension}";
            }
        }
    }
}
