using System;

namespace MoneySharp.Internal.Model
{
    public class RecurringSalesInvoiceGet : SalesInvoiceGet
    {
        public DateTime start_date { get; set; }
        public DateTime? last_date { get; set; }
        public string frequency_type { get; set; }
        public int? frequency { get; set; }
    }
}