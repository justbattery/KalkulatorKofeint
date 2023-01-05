using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static KalkulatorKofeiny.Program;

namespace KalkulatorKofeiny
{
    internal class MenuProfile
    {
        public static void UserProfile()
        {
            if (File.Exists(@"Resources\UserData.txt") == false)
            {
                Console.WriteLine("\nPodaj swoje imię: ");
                User.Name = Console.ReadLine();
                Console.WriteLine("Podaj swoje nazwisko: ");
                User.Surname = Console.ReadLine();
                User.WeightKg = Toolbox.EnterInt("Podaj swoją wagę w kg: ", 1, 1000);

                Backend.UserToFile();
                Backend.CalcSafeDailyDose();

                Console.WriteLine($"Imię: {User.Name}" +
                $"\nNazwisko: {User.Surname}" +
                    $"\nWaga w kg: {User.WeightKg}" +
                    $"\nBezpieczna dzienna dawka kofeiny: {safeDailyDose}mg"); // WYŚWIETLANIE ILE ZOSTAŁO
                Console.WriteLine("Czy chcesz edytować dane?" +
                    "\n1 - Nie" +
                    "\n2 - Tak");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.WriteLine("\nNaciśnij dowolny przycisk aby powrócić...");
                            Console.ReadKey();
                            Menu();
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            File.Delete(@"Resources\UserData.txt");
                            UserProfile();
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine($"\nImię: {User.Name}");
                Console.WriteLine($"Nazwisko: {User.Surname}");
                Console.WriteLine($"Waga: {User.WeightKg}kg");
                Console.WriteLine($"Twoja dzienna bezpieczna dawka: {safeDailyDose}");

                Console.WriteLine("Czy chcesz edytować dane?" +
                    "\n1 - Nie" +
                    "\n2 - Tak");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.WriteLine("\nNaciśnij dowolny przycisk aby powrócić...");
                            Console.ReadKey();
                            Menu();
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            File.Delete(@"Resources\UserData.txt");
                            UserProfile();
                            break;
                        }
                }
            }
        }
    }
}