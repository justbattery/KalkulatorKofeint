using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;
using static KalkulatorKofeiny.Program;

namespace KalkulatorKofeiny
{
    internal class Program
    {
        // STRUKTURY // MOŻE INNY PLIK BO SYF SIĘ ROBI
        public struct Users // DANE UŻYTKOWNIKA
        {
            public string Name;
            public string Surname;
            public int WeightKg;
        }
        public static Users User;

        public static List<Drink> drinks = new List<Drink>();
        public static int DrinkId = 1;
        public struct Drink // DANE NAPOJU
        {
            public int Number;
            public string Name;
            public int CaffeineMg;
            public int CapacityMl;

            public void GetData(string[] fields)
            {
                Number = DrinkId++;
                foreach (var field in fields)
                {
                    var dividedField = field.Split('=');
                    string key = dividedField[0];
                    string value = dividedField[1];

                    if (key == "Name") Name = value;
                    else if (key == "CaffeineMg") int.TryParse(value, out CaffeineMg);
                    else if (key == "CapacityMl") int.TryParse(value, out CapacityMl);
                }
            }
            public string CreateLine()
            {
                return $"Name={Name};CaffeineMg={CaffeineMg};CapacityMl={CapacityMl}";
            }
            public override string ToString()
            {
                return $"{Number} {Name} {CaffeineMg}mg {CapacityMl}ml";
            }
        }
        public static int safeDailyDose;
        public static List<DailyConsumption> dailyConsumption = new List<DailyConsumption>(); // ZOPTYMALIZUJ ZMIENNE GLOBALNE

        // WAŻNE WARTOŚCI
        public static int TodaysCaffeineConsumption;
        public static int HowMuchLeft;
        public static List<TodaysDrinks> drinksConsumedToday = new List<TodaysDrinks>();

        public struct TodaysDrinks
        {
            public string Name;
            public int Count;

            public override string ToString()
            {
                return $"{Name} {Count}";
            }
            public string CreateLine()
            {
                return $"DayName={Name},Count={Count}";
            }
        }
        public struct DailyConsumption
        {
            public DateTime Date;
            public int HowMuchLeft;
            public int HowMuchConsumed;
            public TodaysDrinks DrinksConsumed;
            //public List<TodaysDrinks> DrinksConsumed; // MOŻE TO POWINNA BYĆ STRUKTURA JAK W K35?

            public string CreateLine()
            {
                return $"Date={Date};HowMuchLeft={HowMuchLeft};HowMuchConsumed={HowMuchConsumed};DrinksConsumed={DrinksConsumed}"; // NIE DZIAŁA 
            }
            public override string ToString()
            {
                return $"{Date} // {HowMuchLeft} // {HowMuchConsumed} // {DrinksConsumed.Name}";
            }
        }

        public static void SaveDailyStatsToList() // DOKOŃCZ
        {
            // NIE SKOŃCZONE, WZÓR W MENULIST
            
            dailyConsumption.Add(new DailyConsumption
            {
                Date = DateTime.Now,
                HowMuchLeft = HowMuchLeft,
                HowMuchConsumed = TodaysCaffeineConsumption,
                DrinksConsumed = drinksConsumedToday
            });
            foreach(var d in dailyConsumption)
            {
                Console.WriteLine(d);
                Console.WriteLine("////////////////////////////////////////////////");
                Console.WriteLine(d.CreateLine());
            }
        }

        // FRONTEND
        public static void Menu()
        {
            foreach (var test in drinksConsumedToday)
            {
                Console.WriteLine($"{test.Name} {test.Count}");
            }
            Console.WriteLine("\nCo chcesz zrobić?\n" +
                "1 - Dodaj dzisiejsze spożycie\n" +
                "2 - Wyświetl statystyki\n" +
                "3 - Wyświetl informacje o kofeinie\n" +
                "4 - Przejdź do profilu użytkownika\n" +
                "5 - Modyfikuj listę napojów\n" +
                "0 - Wyjście");
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        MenuToday.TodaysConsumption(); // PRAWIE
                        break;
                    case ConsoleKey.D2:
                        Stats(); // NIC
                        break;
                    case ConsoleKey.D3:
                        Info(); // DONE
                        break;
                    case ConsoleKey.D4:
                        MenuProfile.UserProfile(); // PRAWIE
                        break;
                    case ConsoleKey.D5:
                        MenuList.ModifyList();
                        break;
                    case ConsoleKey.D0:
                        Console.WriteLine("\nDo widzenia");
                        Backend.UserToFile();
                        Backend.DrinkToFile();
                        SaveDailyStatsToList();
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static void Stats()
        {
            // Średnie dzienne spożycie, maksimum, minimum, spożycie w ujęciu dziennym, tygodniowym, miesięcznym, konkretne dni
        }
        static void Info() // ZROBIONE
        {
            if (File.Exists(@"Resources\Informator.txt")) // PLIK NIE OTWARTY W INNYM PROCESIE
            {
                Console.WriteLine("\nInformacje: ");
                string Informator = File.ReadAllText(@"Resources\Informator.txt");
                Console.WriteLine(Informator);
                Console.WriteLine("Naciśnij dowolny przycisk aby powrócić...");
                Console.ReadKey();
                Menu();
            }
            else
            {
                Console.WriteLine("\nNic nie wiadomo o kofeinie przykro mi");
                Console.WriteLine("Naciśnij dowolny przycisk aby powrócić...");
                Console.ReadKey();
                Menu();
            }
        }
        
        static void Main(string[] args)
        {
            if (File.Exists(@"Resources\UserData.txt"))
            {
                Backend.UserFromFile();
            }
            else
            {
                Console.WriteLine("Witaj, wygląda na to, że to Twoje pierwsze uruchomienie! Podaj dane aby program mógł dzialać poprawnie");
                MenuProfile.UserProfile();
            }
            Backend.DrinkFromFile();
            Backend.CalcSafeDailyDose();

            Menu();
        }
    }
}