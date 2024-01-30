using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GepkocsisZH1_Lambda_LINQ_
{
    class Vehicle
    {
        private string lincencePlate;

        public string LincencePlate
        {
            get { return lincencePlate; }
            private set
            {
                Validating(value);
                lincencePlate = value;
            }
        }

        public void Validating(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Z]{3}-(?!000)[0-9]{3}$"))
            {
                throw new LicencePlateException(value);
                //throw new Exception("Problem with the Licence plate formula!");
            }
        }

        //This value just only once can be declared
        protected int YaerDeclaringCounter = 0;

        private int constructionYear;

        public int ConstructionYear
        {
            get { return constructionYear; }
            private set
            {
                Validating(value, YaerDeclaringCounter, 1);
                YaerDeclaringCounter++;
                constructionYear = value;
            }
        }

        public virtual void Validating(int value, int counter, int wich)
        {
            if (counter >= 1)
            {
                throw new CounterException(counter);
            }
            switch (wich)
            {
                case 1:
                   
                    if (!(value >= 1950 && value <= 2024))
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
                default:
                    break;
            }
        }

        //This value just only once can be declared
        private int PriceDeclaringCounter = 0;

        private int originalPrice;

        public int OriginalPrice
        {
            get { return originalPrice; }
            private set
            {
                Validating(value, PriceDeclaringCounter, 2);
                PriceDeclaringCounter++;
                originalPrice = value;
            }
        }

        private Conditions condition;

        public Conditions Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - ConstructionYear;
            }
        }

        public virtual int ExtraPrice
        {
            get
            {
                if (Age <= 2 && Condition == Conditions.LikeANew)
                {
                    return (int)(originalPrice * 0.02);
                    //return Convert.ToInt32((OriginalPrice*0.02));
                }
                return 0;
            }
        }

        public Vehicle(string lPlate, int cYear, int originalPrice, Conditions condition)
        {
            this.LincencePlate = lPlate;
            this.ConstructionYear = cYear;
            this.OriginalPrice = originalPrice;
            this.Condition = condition;
        }

        public Vehicle(string lPlate, int cYear, int originalPrice) : this(lPlate, cYear, originalPrice, Conditions.WellKept)
        {
        }

        private double amortization = 0;
        public virtual int PurchasePrice()
        {
            switch (Condition)
            {
                case Conditions.LikeANew:
                    amortization = 0.09;
                    break;
                case Conditions.WellKept:
                    amortization = 0.10;
                    break;
                case Conditions.Damaged:
                    amortization = 0.11;
                    break;
                case Conditions.Defective:
                    amortization = 0.12;
                    break;
                default:
                    break;
            }
            return (int)Math.Round(OriginalPrice * Math.Pow(amortization, Age) + ExtraPrice);
        }

        public override string ToString()
        {
            string line = $"Licence plate: {this.LincencePlate}, Construction year: {this.ConstructionYear}, Original price: {this.OriginalPrice}, Condition: {this.Condition}, Age: {this.Age}, Extra price: {this.ExtraPrice}, Purchase price: {this.PurchasePrice()}";
            return line;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vehicle))
            {
                throw new Exception("Just two car can be compared!");
            }
            Vehicle vehicle = obj as Vehicle;
            return vehicle.LincencePlate == this.LincencePlate;
        }
    }
}
