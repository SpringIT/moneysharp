namespace MoneySharp.Internal.Model
{
    public class Payment
    {
        public string payment_date { get; set; }
        public decimal price { get; set; }
        public decimal price_base { get; set; }
        public long? financial_account_id { get; set; }
        public long? financial_mutation_id { get; set; }
    }
}