﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    class LicencePlateException : Exception
    {
        public LicencePlateException(string lPlate)
        {
            this.LPlate = lPlate;
        }

        public readonly string LPlate;
    }
}
