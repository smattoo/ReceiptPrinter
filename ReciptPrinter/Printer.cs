using System;

namespace ReciptPrinter
{
    class Printer : IPrinter 
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}