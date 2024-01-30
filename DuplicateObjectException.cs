using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    class DuplicateObjectException : Exception
    {
        public DuplicateObjectException(Vehicle obj) 
        {
            this.Obj = obj;
        }

        public readonly Vehicle Obj;
    }
}
