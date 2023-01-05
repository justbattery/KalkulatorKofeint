using System;
using System.Collections.Generic;
using System.Text;

namespace KalkulatorKofeiny
{
    internal class Toolbox
    {
        public static int EnterInt(string Text, int Min, int Max)
        {
            while (true)
            {
                Console.WriteLine(Text);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= Min && value <= Max) return value;
                else Console.WriteLine($"Nieprawidłowa wartość, wartość musi być pomiędzy {Min}, a {Max}");
            }
        }
    }
}
