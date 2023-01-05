using System;
using System.Collections.Generic;
using System.Text;
using static KalkulatorKofeiny.Program;

namespace KalkulatorKofeiny
{
    internal class MenuList
    {
        public static void ModifyList()
        {
            Console.WriteLine("\nId/Nazwa/Pojemność/Zawartość kofeiny");
            foreach (var drink in drinks)
            {
                Console.WriteLine($"{drink.Number} - {drink.Name} - {drink.CapacityMl}ml - {drink.CaffeineMg}mg");
            }
            while (true)
            {
                Console.WriteLine("\nCo chcesz zrobić?" +
                    "\n1 - Dodaj nowy napój" +
                    "\n2 - Usuń napój" +
                    "\n0 - Powrót");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        AddDrink();
                        break;
                    case ConsoleKey.D2:
                        RemoveDrink();
                        break;
                    case ConsoleKey.D0:
                        Menu();
                        break;
                }
            }
        }
        public static void RemoveDrink()
        {
            Console.WriteLine("\nPodaj numer napoju, który chcesz usunąć, jeśli nie chcesz usunąć żadnego wciśnij 0");
            int.TryParse(Console.ReadLine(), out int choice);
            bool found = false;
            while (found == false)
            {
                foreach (var drink in drinks)
                {
                    if (drink.Number == choice)
                    {
                        drinks.Remove(drink);
                        Console.WriteLine("Usunięto");
                        found = true;
                        ModifyList();
                        break;
                    }
                    else if (choice == 0)
                    {
                        ModifyList();
                        break;
                    }
                }
            }
        }
        public static void AddDrink()
        {
            Console.WriteLine("\nPodaj nazwę: ");
            string nazwa = Console.ReadLine();
            int pojemnoscMl = Toolbox.EnterInt("Podaj pojemność w ml", 1, 10000);
            Console.WriteLine("Chcesz podać ilość kofeiny na 100ml czy całkowitą?" +
                "\n1 - Na 100ml" +
                "\n2 - Całkowitą");
            int kofeinaMg = 0;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    kofeinaMg = Toolbox.EnterInt("\nPodaj ilość kofeiny na 100ml: ", 1, 10000);
                    kofeinaMg = kofeinaMg * (pojemnoscMl / 100);
                    break;
                case ConsoleKey.D2:
                    kofeinaMg = Toolbox.EnterInt("\nPodaj ilość kofeiny w mg: ", 1, 10000);
                    break;
            }

            drinks.Add(new Drink { Number = DrinkId++, Name = nazwa, CaffeineMg = kofeinaMg, CapacityMl = pojemnoscMl });
            Backend.DrinkToFile();

            Console.WriteLine("Dodany!");
            ModifyList();
        }
    }
}
