using System.Collections.Generic;
using MoneySharp.Contract.Model;

namespace MoneySharp.Contract
{
    public interface IRecurringSalesInvoiceService
    {
        /// <summary>
        /// Get all sales invoices from moneybird. Limit is 100 invoices
        /// </summary>
        /// <returns></returns>
        IList<RecurringSalesInvoice> Get();

        /// <summary>
        /// Gets single sales invoice
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        /// <returns></returns>
        RecurringSalesInvoice GetById(long id);

        /// <summary>
        /// Create new sales invoice in moneybird
        /// </summary>
        /// <param name="salesInvoice">Invoice to create</param>
        /// <returns></returns>
        RecurringSalesInvoice Create(RecurringSalesInvoice salesInvoice);

        /// <summary>
        /// Updates sales invoice in momenybird
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        /// <param name="salesInvoice">Sales invoice to update</param>
        /// <returns></returns>
        RecurringSalesInvoice Update(long id, RecurringSalesInvoice salesInvoice);

        /// <summary>
        /// Delete invoice from moneybird
        /// </summary>
        /// <param name="id">Id of sales invoice</param>
        void Delete(long id);
    }
}