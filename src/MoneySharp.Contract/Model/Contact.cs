namespace MoneySharp.Contract.Model
{
    public class Contact
    {
        public long Id { get; set; }
        public string Company { get; set; }
        public string ChamberOfCommerce { get; set; }
        public string TaxNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public Mandate Mandate { get; set; }
    }
}
