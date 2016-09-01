namespace MoneySharp.Contract.Model
{
    public class SalesInvoiceDetail
    {
        public string Description { get; set; }
        public string Amount { get; set; }
        public string TaxRateId { get; set; }
        public decimal Price { get; set; }
        public long LedgerAccountId { get; set; }
        public string Period { get; set; }
        public int RowOrder { get; set; }
    }
}
