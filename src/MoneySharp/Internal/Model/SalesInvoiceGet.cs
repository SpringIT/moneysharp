using System.Collections.Generic;

namespace MoneySharp.Internal.Model
{
    public class SalesInvoiceGet : SalesInvoice
    {
        public string url { get; set; }
        public List<SalesInvoiceDetail> details { get; set; }
        public List<CustomField> custom_fields { get; set; }
    }
}