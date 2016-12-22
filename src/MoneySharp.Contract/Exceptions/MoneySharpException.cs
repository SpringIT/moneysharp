using System;

namespace MoneySharp.Contract.Exceptions
{
    public class MoneySharpException : Exception
    {
        public MoneySharpException(string message) : base(message) 
        {
            
        }
    }
}