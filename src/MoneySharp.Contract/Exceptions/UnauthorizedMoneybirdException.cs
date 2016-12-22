namespace MoneySharp.Contract.Exceptions
{
    public class UnauthorizedMoneybirdException : MoneySharpException
    {
        public UnauthorizedMoneybirdException(string message) : base(message)
        {
        }
    }
}