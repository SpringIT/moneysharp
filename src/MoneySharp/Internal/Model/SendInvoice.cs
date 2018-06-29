using System;

namespace MoneySharp.Internal.Model
{
    public class SendInvoice
    {
        public string delivery_method { get; set; }
        public bool sending_scheduled { get; set; }
        public bool delivery_ubl { get; set; }
        public bool mergeable { get; set; }
        public string email_address { get; set; }
        public string email_message { get; set; }
        public DateTime invoice_date { get; set; }
    }
}