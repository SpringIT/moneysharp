using System;
using NUnit.Common;
using NUnitLite;

namespace MoneySharp.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int result;
            result = new AutoRun().Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
            if (result > 0)
            {
                Environment.Exit(1);
            }
        }
    }
}
