using MoneySharp.Internal.Model;

namespace MoneySharp.Internal
{
    public interface ISalesInvoiceConnector<TGetObject, in TPostObject> : IDefaultConnector<TGetObject, TPostObject>
        where TGetObject : class, new() where TPostObject : class, new()
    {
        void Send(long id, SendInvoice sendInvoice);
        void DeletePayment(long id, long paymentId);
        void CreatePayment(long id, Payment payment);
        SalesInvoiceGet CreditInvoice(long id);
    }
}