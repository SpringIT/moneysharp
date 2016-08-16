using System;

namespace MoneySharp.Internal.Model
{
    public class RecurringSalesInvoicePost : SalesInvoiceGet
    {
        public DateTime? start_date { get; set; }
        public string frequency_type { get; set; }
        public int? frequency { get; set; }
    }
}