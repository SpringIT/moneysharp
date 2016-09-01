namespace MoneySharp.Contract
{
    public interface IMoneyBirdClient
    {
        /// <summary>
        /// Contacts of moneybird
        /// </summary>
        IContactService Contacts { get; }
        /// <summary>
        /// Sales invoices of moneybird
        /// </summary>
        ISalesInvoiceService SalesInvoices { get; }
    }
}