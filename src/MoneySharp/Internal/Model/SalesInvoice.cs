namespace MoneySharp.Internal.Model
{
    public class SalesInvoice
    {
        public long? id { get; set; }
        public long contact_id { get; set; }
        public long document_style_id { get; set; }
        public long workflow_id { get; set; }
        public bool prices_are_incl_tax { get; set; }
        public string invoice_date { get; set; }
        public string due_date { get; set; }
        public decimal total_price_excl_tax { get; set; }
        public decimal total_price_incl_tax { get; set; }
        public decimal total_tax { get; set; }
        public string invoice_id { get; set; }
        public string state { get; set; }
        public string reference{get;set;}
    }
}
