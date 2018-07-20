namespace MoneySharp.Internal.Model.Wrapper
{
    public class PaymentWrapper
    {
        public PaymentWrapper()
        {
            
        }

        public PaymentWrapper(Payment payment)
        {
            this.payment = payment;
        }

        public Payment payment { get; set; }
    }
}
