using System;

namespace MoneySharp.Contract.Model
{
    public class SendInvoice
    {
        public string DeliveryMethod { get; set; }
        public bool SendingScheduled { get; set; }
        public bool DeliveryUbl { get; set; }
        public bool Mergeable { get; set; }
        public string EmailAddress { get; set; }
        public string EmailMessage { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}