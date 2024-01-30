using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    public class SeatNumberException : Exception
    {
        public SeatNumberException(int seatNumber, int[] seats)
        {
            this.SeatNumber = seatNumber;
            this.Seats = seats;
        }

        public readonly int SeatNumber;
        public readonly int[] Seats;
    }
}
