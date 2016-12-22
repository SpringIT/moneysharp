namespace MoneySharp.Internal.Model.Wrapper
{
    public class SalesInvoiceWrapper
    {
        public SalesInvoiceWrapper()
        {
            
        }

        public SalesInvoiceWrapper(SalesInvoicePost salesInvoice)
        {
            this.sales_invoice = salesInvoice;
        }

        public SalesInvoicePost sales_invoice { get; set; }
    }
}
