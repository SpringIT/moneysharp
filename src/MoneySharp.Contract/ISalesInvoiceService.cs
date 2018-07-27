using System.Collections.Generic;
using MoneySharp.Contract.Model;

namespace MoneySharp.Contract
{
    public interface ISalesInvoiceService
    {
        /// <summary>
        /// Get all sales invoices from moneybird. Limit is 100 invoices
        /// </summary>
        /// <returns></returns>
        IList<SalesInvoice> Get();

        /// <summary>
        /// Gets single sales invoice
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        /// <returns></returns>
        SalesInvoice GetById(long id);

        /// <summary>
        /// Create new sales invoice in moneybird
        /// </summary>
        /// <param name="salesInvoice">Invoice to create</param>
        /// <returns></returns>
        SalesInvoice Create(SalesInvoice salesInvoice);

        /// <summary>
        /// Updates sales invoice in momenybird
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        /// <param name="salesInvoice">Sales invoice to update</param>
        /// <returns></returns>
        SalesInvoice Update(long id, SalesInvoice salesInvoice);

        /// <summary>
        /// Delete invoice from moneybird
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        void Delete(long id);

        /// <summary>
        /// Sends invoice to customer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="invoice"></param>
        void Send(long id, SendInvoice invoice);

        /// <summary>
        /// Creates a payment on the invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payment"></param>
        void CreatePayment(long id, Payment payment);

        /// <summary>
        /// Delete a payment on the invoice
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentId"></param>
        void DeletePayment(long id, long paymentId);

        /// <summary>
        /// Create credit invoice based on invoice
        /// </summary>
        /// <param name="id"></param>
        SalesInvoice CreditInvoice(long id);
    }
}