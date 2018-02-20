using System.Collections.Generic;
using MoneySharp.Internal.Helper;
using MoneySharp.Internal.Model;
using RestSharp;

namespace MoneySharp.Internal
{
    public class ContactConnector<TGetObject, TPostObject> : DefaultConnector<TGetObject, TPostObject>, IContactConnector<TGetObject, TPostObject>
        where TGetObject : class, new()
        where TPostObject : class, new()
    {

        public ContactConnector(string urlAppend, IClientInitializer initializer, IRequestHelper requestHelper) : base(urlAppend, initializer, requestHelper)
        {
        }

        public IList<Contact> GetBySearch(string search)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}?query={search}", Method.GET);
            var response = Client.Execute<List<Contact>>(request);
            RequestHelper.CheckResult(response);
            return response.Data;
        }
    }
}