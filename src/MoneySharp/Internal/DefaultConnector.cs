using System.Collections.Generic;
using MoneySharp.Internal.Helper;
using RestSharp;

namespace MoneySharp.Internal
{
    public class DefaultConnector<TGetObject, TPostObject> : IDefaultConnector<TGetObject, TPostObject>
        where TGetObject : class, new()
        where TPostObject : class, new()
    {
        private readonly IClientInitializer _initializer;
        protected readonly string UrlAppend;
        protected readonly IRequestHelper RequestHelper;
        protected IRestClient Client => _initializer.Get();

        public DefaultConnector(string urlAppend, IClientInitializer initializer, IRequestHelper requestHelper)
        {
            _initializer = initializer;
            UrlAppend = urlAppend;
            RequestHelper = requestHelper;
        }

        public IList<TGetObject> GetList()
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}", Method.GET);
            var response = Client.Execute<List<TGetObject>>(request);
            RequestHelper.CheckResult(response);
            return response.Data;
        }

        public TGetObject GetById(long id)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}/{id}", Method.GET);
            var result = Client.ExecuteAsGet<TGetObject>(request, "GET");
            RequestHelper.CheckResult(result);
            return result.Data;
        }

        public TGetObject Create(TPostObject data)
        {
            var request = RequestHelper.BuildRequest(UrlAppend, Method.POST, data);
            var result = Client.ExecuteAsPost<TGetObject>(request, "POST");
            RequestHelper.CheckResult(result);
            return result.Data;
        }

        public TGetObject Update(long id, TPostObject data)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}/{id}", Method.PATCH, data);
            var result = Client.ExecuteAsPost<TGetObject>(request, "PATCH");
            RequestHelper.CheckResult(result);
            return result.Data;
        }

        public void Delete(long id)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}/{id}", Method.DELETE);
            var result = Client.Execute(request);
            RequestHelper.CheckResult(result);
        }
    }
}
