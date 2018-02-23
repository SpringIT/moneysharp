using System.Collections.Generic;
using MoneySharp.Internal.Helper;
using MoneySharp.Internal.Model;
using RestSharp;

namespace MoneySharp.Internal
{
    public class RecurringSalesInvoiceConnector<TGetObject, TPostObject> : DefaultConnector<TGetObject, TPostObject>, IRecurringSalesInvoiceConnector<TGetObject, TPostObject>
        where TGetObject : class, new()
        where TPostObject : class, new()
    {

        public RecurringSalesInvoiceConnector(string urlAppend, IClientInitializer initializer, IRequestHelper requestHelper) : base(urlAppend, initializer, requestHelper)
        {
        }

        public IList<RecurringSalesInvoiceGet> GetByContactId(long id)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}", Method.GET, null, $"?filter=contact_id:{id}");
            var response = Client.Execute<List<RecurringSalesInvoiceGet>>(request);
            RequestHelper.CheckResult(response);
            return response.Data;
        }
    }
}