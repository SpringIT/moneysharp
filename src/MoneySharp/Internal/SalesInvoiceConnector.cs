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

        public void CreatePayment(long id, Payment payment)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}/{id}/payments", Method.POST, payment);
            var response = Client.Execute(request);
            RequestHelper.CheckResult(response);
        }  
        
        public void DeletePayment(long id, long paymentId)
        {
            var request = RequestHelper.BuildRequest($"{UrlAppend}/{id}/payments/{paymentId}", Method.DELETE);
            var response = Client.Execute(request);
            RequestHelper.CheckResult(response);
        }
    }
}