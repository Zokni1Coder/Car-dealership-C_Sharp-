using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace GepkocsisZH1_Lambda_LINQ_
{
    class Program
    {
        //Color for Warning and Error messages
        static void Warning(string description)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(description);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void Error(string description)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(description);
            Console.ForegroundColor = ConsoleColor.Gray;
        }



        private static void CreatingObjects(string [] datas, string fileName, Dealer Dler)
        {
            Vehicle temp;
            try
            {
                if (datas[0] == "G")
                {
                    switch (datas.Length)
                    {
                        case 5:
                            temp = new Vehicle(datas[1], int.Parse(datas[2]), int.Parse(datas[3]), (Conditions)Enum.Parse(typeof(Conditions), datas[4]));
                            break;
                        case 3:
                            temp = new Vehicle(datas[1], int.Parse(datas[2]), int.Parse(datas[3]));
                            break;
                        default:
                            throw new DataSourceException(fileName);
                            //throw new Exception("Something is wrong with the Source data.");
                    }
                }
                else
                {
                    switch (datas.Length)
                    {
                        case 8:
                            temp = new Car(datas[1], int.Parse(datas[2]), int.Parse(datas[3]), (Conditions)Enum.Parse(typeof(Conditions), datas[4]), int.Parse(datas[5]), bool.Parse(datas[6]), (AirCondition)Enum.Parse(typeof(AirCondition), datas[7]));
                            break;
                        case 6:
                            temp = new Car(datas[1], int.Parse(datas[2]), int.Parse(datas[3]), int.Parse(datas[4]), bool.Parse(datas[5]));
                            break;
                        default:
                            throw new DataSourceException(fileName);
                            //throw new Exception("Something is wrong with the Source data.");
                    }
                }
                Dler.AddVehicle(temp);
            }
            catch (DuplicateObjectException ex)
            {
                Warning($"The {ex.Obj.LincencePlate} is already exists in the database.Specifications:\n{ex.Obj.ToString()}\n");
            }
            catch (LicencePlateException ex)
            {
                Error($"Problem with {ex.LPlate} License Plate.");
            }
            catch (YearException ex)
            {
                int min = 1950;
                int max = DateTime.Now.Year;
                if (ex.Year < min)
                {
                    datas[2] = min.ToString();
                    Warning($"Construction year cant be earlier than {min}. Its changed from {ex.Year} to {datas[2]} by car with \"{datas[1]}\" Licence Plait.\n");
                    CreatingObjects(datas, fileName, Dler);
                }
                else
                {
                    datas[2] = max.ToString();
                    Warning($"Construction year cant be later than {max}. Its changed from {ex.Year} to {datas[2]} by car with \"{datas[1]}\" Licence Plait.\n");
                    CreatingObjects(datas, fileName, Dler);
                }
            }
            catch (PriceException ex)
            {
                int min = 300000;
                int max = 12000000;

                if (ex.Price < min)
                {
                    datas[3] = min.ToString();
                    Warning($"Price year cant be less than {min}. Its changed from {ex.Price} to {datas[3]} by car with \"{datas[1]}\" Licence Plait.\n");
                    CreatingObjects(datas, fileName, Dler);
                }
                else
                {
                    datas[3] = max.ToString();
                    Warning($"Construction year cant be more than {max}. Its changed from {ex.Price} to {datas[3]} by car with \"{datas[1]}\" Licence Plait.\n");
                    CreatingObjects(datas, fileName, Dler);
                }
            }
            catch (SeatNumberException ex)
            {
                string line = "";
                foreach (int numbers in ex.Seats)
                {
                    line += $",{numbers}";
                }
                line = line.Remove(0, 1);
                Error($"Number of seats can be only {line}! Problem with Vehicle \"{datas[1]}\" wich have {ex.SeatNumber}!\n");
            }
            catch (Exception)
            {

            }
            
        }


        private static void DataLoading(string fileName, Dealer Dler)
        {
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] datas = line.Split(';');

                    CreatingObjects(datas, fileName, Dler);
                }
            }
            catch (FileNotFoundException)
            {
                Error("Source file not found!");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
        }

        static void OutWriting(Dealer Dler)
        {
            Console.WriteLine("\nAll Vehicles:\n");
            foreach (var item in Dler.Container)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\nCars:\n");
            foreach (var item in Dler.Cars)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("");
        }

        private static void Search(Dealer Dler)
        {
            Console.Write("\nLicense Plate to search: ");
            string temp = Console.ReadLine().ToUpper();
            if (!Regex.IsMatch(temp, @"^[A-Z]{3}-(?!000)[0-9]{3}$"))
            {
                throw new LicencePlateException(temp);
            }

            string output = "\nResult of searching: ";

            Vehicle founded = Dler[temp];
            Car isACar = (founded is Car ? founded as Car : null);


            output += (founded == null ? "Not found!" : "");
            output += (isACar != null ? $"{isACar}" : $"{founded}");
            
            Console.WriteLine(output);
        }


        private static void Cheapest(Dealer Dler)
        {
            Car temp = Dler.cheapest(); 
            Console.WriteLine($"\nThe cheapest car with \"{Dler.Correcting(Conditions.WellKept.ToString())}\" condition is: {temp.LincencePlate} with price {temp.ExtraPrice+temp.OriginalPrice}$!");
        }


        static void Main(string[] args)
        {
            try
            {
                Dealer Dler = new Dealer();
                DataLoading("Cars.txt", Dler);
                OutWriting(Dler);

                try
                {
                    if (Dler.Cars.Count == 0)
                    {
                        throw new ListIsEmpty();
                    }
                    Dler.Filtering();
                }               
                catch (ListIsEmpty)
                {
                    Warning("Currently we dont have cars!");
                }
                catch (Exception)
                {
                    Error("Something went wrong!");
                }
                try
                {
                    Search(Dler);
                }
                catch (LicencePlateException)
                {
                    Console.WriteLine("You gave a wrong type of License Plate! Do you want to try again? (Type: Yes or No)");
                    string answer = Console.ReadLine().ToUpper();
                    if (answer == "YES")
                    {
                        Search(Dler);
                    }
                    else
                    {
                        Console.WriteLine("Goodbye! :)");
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                try
                {
                    Cheapest(Dler);
                }
                catch (ListIsEmpty)
                {
                    Error("\nThe list of Car is Empty!\n");
                }
            }
            finally
            {
                Console.WriteLine("Goodbye!");
                Console.ReadKey();
            }
        }
    }
}
