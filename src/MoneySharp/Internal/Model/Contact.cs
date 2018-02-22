using System;

namespace MoneySharp.Internal.Model
{
    public class Contact
    {
        public long? id { get; set; }
        public string attention { get; set; }
        public string chamber_of_commerce { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string zipcode { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string company_name { get; set; }
        public DateTime created_at { get; set; }
        public string customer_id { get; set; }
        public string send_invoices_to_email { get; set; }
        public string send_estimates_to_email { get; set; }
        public string phone { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool sepa_active { get; set; }
        public string sepa_bic { get; set; }
        public string sepa_iban { get; set; }
        public string sepa_iban_account_name { get; set; }
        public DateTime? sepa_mandate_date { get; set; }
        public string sepa_mandate_id { get; set; }
        public string sepa_sequence_type { get; set; }
        public string tax_number { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
