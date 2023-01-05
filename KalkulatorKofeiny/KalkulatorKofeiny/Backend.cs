using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KalkulatorKofeiny
{
    internal class Backend
    {
        // BACKEND
        public static void UserToFile()
        {
            {
                using (StreamWriter UserData = new StreamWriter(@"Resources\UserData.txt"))
                {
                    UserData.WriteLine(Program.User.Name);
                    UserData.WriteLine(Program.User.Surname);
                    UserData.WriteLine(Program.User.WeightKg);
                }
            }
        }
        public static void UserFromFile()
        {
            {
                if (File.Exists(@"Resources\UserData.txt"))
                {
                    using (StreamReader UserData = new StreamReader(@"Resources\UserData.txt"))
                    {
                        Program.User.Name = UserData.ReadLine();
                        Program.User.Surname = UserData.ReadLine();
                    }
                }
                else Console.WriteLine("SELF NOTE: Plik UserData.txt nie istnieje");
            }
        }

        public static void DrinkFromFile()
        {
            Program.drinks.Clear();

            if (File.Exists(@"Resources\Lista.csv"))
            {
                using (StreamReader data = new StreamReader(@"Resources\Lista.csv")) // INNY PROCES
                {
                    while (data.EndOfStream == false)
                    {
                        string[] fields = data.ReadLine().Split(";");

                        Program.Drink drink = new Program.Drink();
                        drink.GetData(fields);
                        Program.drinks.Add(drink);
                    }
                }
            }
            else
            {
                Console.WriteLine("BŁĄD: BRAK PLIKU Lista.csv");
            }
        }

        public static void DrinkToFile()
        {
            if (File.Exists(@"Resources\Lista.csv"))
            {
                File.Delete(@"Resources\Lista.csv");
                using (StreamWriter stream = new StreamWriter(@"Resources\Lista.csv"))
                {
                    foreach (var drink in Program.drinks)
                    {
                        stream.WriteLine(drink.CreateLine());
                    }
                }
            }
        }

        public static (int, int) CalcSafeDailyDose()
        {
            if (File.Exists(@"Resources\UserData.txt"))
            {
                using (StreamReader Data = new StreamReader(@"Resources\UserData.txt"))
                {
                    for (int i = 1; i < 3; i++)
                    {
                        Data.ReadLine();
                    }
                    int.TryParse(Data.ReadLine(), out Program.User.WeightKg);
                } // UWAGA ZMIANA
                return (Program.safeDailyDose = 6 * Program.User.WeightKg, Program.HowMuchLeft = Program.safeDailyDose);
            }
            else
            {
                return (0, 0);
            }
        }
    }
}