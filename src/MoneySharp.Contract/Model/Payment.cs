using System;

namespace MoneySharp.Contract.Model
{
    public class Payment
    {
        public DateTime PaymentDate { get; set; }
        public decimal Price { get; set; }
        public decimal PriceBase { get; set; }
        public long? FinancialAccountId { get; set; }
        public long? FinancialMutationId { get; set; }
    }
}