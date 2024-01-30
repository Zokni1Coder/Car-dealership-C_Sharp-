using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    public class DataSourceException : Exception
    {
        public DataSourceException(string source) : base("Something is wrong with the Source data.")
        {
            this.Source = source;
        }

        public readonly string Source;
    }
}
