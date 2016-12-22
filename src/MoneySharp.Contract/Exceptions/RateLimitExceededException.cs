namespace MoneySharp.Contract.Exceptions
{
    public class RateLimitExceededException : MoneySharpException
    {
        public RateLimitExceededException(string message): base(message)
        {
        }
    }
}