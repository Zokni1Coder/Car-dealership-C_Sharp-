using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GepkocsisZH1_Lambda_LINQ_
{
    class Car : Vehicle
    {
        private int[] seats = { 2, 4, 5, 7 };
        private int passengerNumber;
        public int PassengerNumber
        {
            get { return passengerNumber; }
            set
            {
                Validating(value, 0, 3);
                passengerNumber = value;
            }
        }

        public override void Validating(int value, int counter, int wich)
        {
            if (counter >= 1)
            {
                throw new CounterException(counter);
            }
            switch (wich)
            {
                case 1:

                    if (!(value >= 1950 && value <= 2023))
                    {
                        throw new YearException(value);
                    }

                    break;
                case 2:
                    if (!(value >= 300000 && value <= 12000000))
                    {
                        throw new PriceException(value);
                    }
                    break;
                case 3:
                    if (!(seats.Contains(value)))
                    {
                        throw new SeatNumberException(value, seats);
                        //throw new Exception("Seat number can be only 2,4,5 or 7!");
                    }
                    break;
                default:
                    break;
            }
        }

        private bool towHitch;

        public bool TowHitch
        {
            get { return towHitch; }
            set { towHitch = value; }
        }

        private AirCondition ac;

        public AirCondition AC
        {
            get { return ac; }
            set { ac = value; }
        }

        public override int ExtraPrice
        {
            get
            {
                int sum = 0;
                if (towHitch)
                {
                    sum += 60000;
                }
                if (this.PassengerNumber == 7)
                {
                    sum += 100000;
                }
                if (Age <= 2 && Condition == Conditions.LikeANew)
                {
                    sum = (int)(OriginalPrice * 0.02);
                    //return Convert.ToInt32((OriginalPrice*0.02));
                }
                switch (AC)
                {
                    case AirCondition.None:
                        break;
                    case AirCondition.Manual:
                        sum += 40000;
                        break;
                    case AirCondition.Digital:
                        sum += 150000;
                        break;
                    case AirCondition.DigitalWithMoreZone:
                        sum += 350000;
                        break;
                    default:
                        break;
                }
                return sum;
            }
        }
        public Car(string lPlate, int cYear, int oPrice, Conditions condition, int passengers, bool towHitch, AirCondition ac) : base(lPlate, cYear, oPrice, condition)
        {
            this.PassengerNumber = passengers;
            this.TowHitch = towHitch;
            this.AC = ac;
        }
        public Car(string lPlate, int cYear, int oPrice, int passenger, bool towHitch) : this(lPlate, cYear, oPrice, Conditions.WellKept, passenger, towHitch, AirCondition.Digital)
        {
        }

        private double amortization = 0;
        public override int PurchasePrice()
        {
            switch (Condition)
            {
                case Conditions.LikeANew:
                    amortization = 0.08;
                    break;
                case Conditions.WellKept:
                    amortization = 0.09;
                    break;
                case Conditions.Damaged:
                    amortization = 0.12;
                    break;
                case Conditions.Defective:
                    amortization = 0.13;
                    break;
                default:
                    break;
            }
            if (this.PassengerNumber == 7)
            {
                amortization *= 1.2;
            }
            return (int)(OriginalPrice * Math.Pow(amortization, Age) + ExtraPrice);
        }

        public override string ToString()
        {
            string line = $"{base.ToString()}, Passengers: {this.PassengerNumber}, Tow Hitch: {(this.TowHitch ? "Yes" : "No")}, " +
                $"Air Condition: {this.AC}";
            return line;
        }
    }
}
