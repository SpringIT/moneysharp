namespace MoneySharp.Internal.Model
{
    public class SalesInvoiceDetail
    {
        public string description { get; set; }
        public decimal price { get; set; }
        public string amount { get; set; }
        public string tax_rate_id { get; set; }
        public long ledger_account_id { get; set; }
        public string period { get; set; }
        public int row_order { get; set; }
        public long product_id { get; set; }
    }
}
