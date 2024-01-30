using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GepkocsisZH1_Lambda_LINQ_
{
    class Dealer
    {
        private List<Vehicle> container = new List<Vehicle>();

        public List<Vehicle> Container
        {
            get
            {
                List<Vehicle> temp = new List<Vehicle>();
                foreach (Vehicle item in container)
                {
                    Vehicle clone = new Vehicle(item.LincencePlate, item.ConstructionYear, item.OriginalPrice, item.Condition);
                    temp.Add(clone);
                }
                return temp;
            }
        }

        //Fontos, hogy ne a klónon iteráljak végig, mert úgy nem lesz felismerhető tovább hogy az objektum az autó vagy jármű és nem fogom
        //tudni kiválogatni!
        public List<Car> Cars
        {
            get
            {
                List<Car> cars = new List<Car>();
                foreach (Vehicle item in container)
                {
                    Car temp;
                    if (item is Car)
                    {
                        temp = item as Car;
                        cars.Add(temp);
                    }
                }
                return cars;
            }
        }

        //This will give back the cheapest car in the List
        //Will throw Error, if the Dealer doesnt have any car at the moment.
        public Car cheapest()
        {
            if (Cars.Count == 0)
            {
                throw new ListIsEmpty();
            }

            Car cheapest= null;

            foreach (Car item in Cars)
            {
                if (item.Condition == Conditions.WellKept && cheapest == null)
                {
                    cheapest = item;
                }
                if (item.Condition == Conditions.WellKept && ((cheapest.OriginalPrice + cheapest.ExtraPrice) >= (item.OriginalPrice + item.ExtraPrice)))
                {
                    cheapest = item;
                }
            }
            return cheapest;
        }

        //Indexer: search with Licence Plate
        public Vehicle this[string lPLate]
        {
            get
            {
                foreach (Vehicle item in container)
                {
                    if (item.LincencePlate == lPLate)
                    {
                        return item;
                    }
                }
                return null;
                
                //throw new Exception("We dont have Vehicle with this Licence Plate!");
            }
        }

        //Methods

        //Adding Vehicles to the Container and will give back error if the Vehicle is already exists in the Container 
        public void AddVehicle(Vehicle temp)
        {
            //List<Vehicle>tempList = Container;
            //if (tempList.Contains(temp))
            //{
            //    throw new Exception("the Vehicle is already exists in the Container");
            //}
            //tempList.Add(temp);
            //container = tempList;

            if (container.Contains(temp))
            {
                throw new DuplicateObjectException(temp);
                //throw new Exception("the Vehicle is already exists in the Container");
            }

            container.Add(temp);
        }

        //Collecting the properties for the Filtering and writing out.
        public void Filtering()
        {
            var ConditionList = Enum.GetValues(typeof(Conditions)).Cast<Conditions>().ToList();
            string description = "Select the condition of the car: ";
            foreach (var item in ConditionList)
            {
                description += $"{(int)item}.) {Correcting(item.ToString())} ";
            }
            Console.WriteLine(description);

            Console.Write("Your choice: ");
            int choosedNumber = int.Parse(Console.ReadLine());
            Conditions tempCond = ConditionList.Find(x => (int)x == choosedNumber);

            Console.WriteLine($"Your chosen Condition is {tempCond}.");
            Console.Write("The maximum price: ");
            int maxPrice = int.Parse(Console.ReadLine());

            List<Car> Result = FilteredCars(tempCond, maxPrice);

            if (Result.Count == 0)
            {
                Console.WriteLine("We dont have any car at the moment with this criteria.");
            }

            foreach (var item in Result)
            {
                Console.WriteLine(item);
            }
        }

        //Will give back a List with the cars, which is under the selected Price and have a selected condition.
        private List<Car> FilteredCars(Conditions SConditon, int SPrice)
        {
            return Cars.Where(c => c.Condition == SConditon && (c.OriginalPrice + c.ExtraPrice) <= SPrice).ToList();
        }

        //Correcting the Enum names.
        public string Correcting(string name)
        {
            string CorrectName = "";

            for (int i = 0; i < name.Length; i++)
            {
                if (char.IsUpper(name[i]) && i > 0)
                {
                    CorrectName += " ";
                }
                CorrectName += $"{name[i]}";
            }
            return CorrectName;
        }

    }
}
