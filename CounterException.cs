using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    public class CounterException : Exception
    {
        public CounterException(int counter)
        {
            this.Counter = counter;
        }

        public readonly int Counter;
    }
}
