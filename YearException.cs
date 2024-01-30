using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    class YearException : Exception
    {
        public YearException(int year)
        {
            this.Year = year;
        }

        public readonly int Year;
    }
}
