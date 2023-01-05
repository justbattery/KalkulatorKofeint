using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static KalkulatorKofeiny.Program;
using System.Threading;
using System.Linq;

namespace KalkulatorKofeiny
{
    internal class MenuToday
    {
        public static void TodaysConsumption()
        {
            Console.WriteLine("\nCo wypiłeś?");
            ViewList();
            //Console.WriteLine("\nWybierz z listy co wypiłeś, jeśli chcesz ją modyfikować wciśnij 2" +
            //    "\n2 - Modyfikuj" +
            //    "\n0 - Powrót");
            //switch (Console.ReadKey().Key)
            //{
            //    case ConsoleKey.D1:
            //        ViewList();
            //        break;
            //    case ConsoleKey.D2:
            //        MenuList.ModifyList();
            //        break;
            //    case ConsoleKey.D0:
            //        Menu();
            //        break;
            //}
        }
        public static void ViewList()
        {
            if (File.Exists(@"Resources\Lista.csv"))
            {
                foreach (var drink in drinks)
                {
                    Console.WriteLine($"{drink.Number} - {drink.Name}");
                }

                int choice;
                int index = 0;
                bool found = false;

                while (found == false)
                {
                    int.TryParse(Console.ReadLine(), out choice);
                    index = 0;
                    foreach (var drink in drinks)
                    {
                        if (drink.Number == choice)
                        {
                            // STATYSTYKI //
                            TodaysCaffeineConsumption += drink.CaffeineMg;
                            HowMuchLeft -= drink.CaffeineMg;
                            int count = 1;

                            TodaysDrinks find = drinksConsumedToday.Find(d => d.Name == drink.Name);

                                if (find.Name == drink.Name)
                                {
                                    count = find.Count;
                                    count++;
                                    int findIndex = drinksConsumedToday.IndexOf(find);
                                    drinksConsumedToday.RemoveAt(findIndex);
                                    drinksConsumedToday.Insert(findIndex, new TodaysDrinks { Name = drink.Name, Count = count });
                                }
                                else
                                {
                                    drinksConsumedToday.Add(new TodaysDrinks { Name = drink.Name, Count = count });
                                }
                            // STATYSTYKI //
                            Console.WriteLine($"Możesz dzisiaj wypić jeszcze {HowMuchLeft / drink.CaffeineMg} takich i nic nie powinno się stac, zostało Ci jeszcze {HowMuchLeft}mg kofeiny by zostać w bezpieznym limicie" +
                                $"\nCzy chcesz więcej informacji?" +
                                $"\n1 - Tak" +
                                $"\n2 - Nie"); // POKOLORUJ ŁADNIE

                            //UWAGA
                            drinksConsumedToday.ForEach(drink => Console.WriteLine($"{drink.Name} {drink.Count}"));

                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.D1:
                                    Details();
                                    Console.WriteLine("Naciśnij dowolny przycisk aby powrócić do menu...");
                                    Console.ReadKey();
                                    Menu();
                                    break;
                                case ConsoleKey.D2:
                                    Menu();
                                    break;
                            }
                            found = true;
                            break;
                        }
                        index++;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nBŁĄD: BRAK PLIKU" +
                    "\nNaciśnij dowolny przycisk aby powrócić...");
                Console.ReadKey();
                Program.Menu();
            }
        }
        public static void Details()
        {
            Console.WriteLine($"\nTwoja bezpieczna dawka: {safeDailyDose}mg, zostało Ci jeszcze: {HowMuchLeft}" +
                $"\nOznacza to, że możesz wypić dzisiaj jeszcze...");
            foreach (var drink in drinks)
            {
                Console.WriteLine($"{HowMuchLeft / drink.CaffeineMg} {drink.Name}");
            }
        }
    }
}
