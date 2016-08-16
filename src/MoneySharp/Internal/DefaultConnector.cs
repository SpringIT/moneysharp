using System.Collections.Generic;
using MoneySharp.Internal.Helper;
using RestSharp;

namespace MoneySharp.Internal
{
    public class DefaultConnector<TGetObject, TPostObject> : IDefaultConnector<TGetObject, TPostObject> 
        where TGetObject : class, new() 
        where TPostObject : class, new()
    {
        private readonly string _urlAppend;
        private readonly IClientInitializer _initializer;
        private readonly IRequestHelper _requestHelper;

        private IRestClient Client => _initializer.Get();

        public DefaultConnector(string urlAppend, IClientInitializer initializer, IRequestHelper requestHelper)
        {
            _urlAppend = urlAppend;
            _initializer = initializer;
            _requestHelper = requestHelper;
        }

        public IList<TGetObject> GetList() 
        {
            var request = _requestHelper.BuildRequest($"{_urlAppend}", Method.GET);
            var response = Client.Execute<List<TGetObject>>(request);
            _requestHelper.CheckResult(response);
            return response.Data;
        }

        public TGetObject GetById(long id)
        {
            var request = _requestHelper.BuildRequest($"{_urlAppend}/{id}", Method.GET);
            var result = Client.ExecuteAsGet<TGetObject>(request, "GET");
            _requestHelper.CheckResult(result);
            return result.Data;
        }

        public TGetObject Create(TPostObject data)
        {
            var request = _requestHelper.BuildRequest(_urlAppend, Method.POST, data);
            var result = Client.ExecuteAsPost<TGetObject>(request, "POST");
            _requestHelper.CheckResult(result);
            return result.Data;
        }

        public TGetObject Update(long id, TPostObject data)
        {
            var request = _requestHelper.BuildRequest($"{_urlAppend}/{id}", Method.PATCH, data);
            var result = Client.ExecuteAsPost<TGetObject>(request, "PATCH");
            _requestHelper.CheckResult(result);
            return result.Data;
        }

        public void Delete(long id)
        {
            var request = _requestHelper.BuildRequest($"{_urlAppend}/{id}", Method.DELETE);
            var result = Client.Execute(request);
            _requestHelper.CheckResult(result);
        }
    }
}
