using MoneySharp.Internal.Helper;
using MoneySharp.Internal.Model;
using RestSharp;

namespace MoneySharp.Internal
{
    public class SalesInvoiceConnector<TGetObject, TPostObject> : DefaultConnector<TGetObject, TPostObject>, ISalesInvoiceConnector<TGetObject, TPostObject>
        where TGetObject : class, new()
        where TPostObject : class, new()
    {

        public SalesInvoiceConnector(string urlAppend, IClientInitializer initializer, IRequestHelper requestHelper) : base(urlAppend, initializer, requestHelper)
        {
        }

        public void Send(long id, SendInvoice sendInvoice)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}/{id}/send_invoice", Method.PATCH, sendInvoice);
            var response = Client.Execute(request);
            RequestHelper.CheckResult(response);
        }
    }
}