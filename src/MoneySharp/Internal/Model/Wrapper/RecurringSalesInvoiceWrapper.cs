namespace MoneySharp.Internal.Model.Wrapper
{
    public class RecurringSalesInvoiceWrapper
    {
        public RecurringSalesInvoiceWrapper()
        {
            
        }

        public RecurringSalesInvoiceWrapper(RecurringSalesInvoicePost recurringSalesInvoice)
        {
            recurring_sales_invoice = recurringSalesInvoice;
        }

        public RecurringSalesInvoicePost recurring_sales_invoice { get; set; }
    }
}
