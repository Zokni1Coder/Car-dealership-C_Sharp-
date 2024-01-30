using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    public class PriceException : Exception
    {
        public PriceException(int Price)
        {
            this.Price = Price;
        }

        public readonly int Price;
    }
}
