using System;

namespace MoneySharp.Contract.Model
{
    public class Mandate
    {
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string AccountName { get; set; }
        public DateTime? SignedMandate { get; set; }
        public string SequenceType { get; set; }
        public string Mandatecode { get; set; }
    }
}