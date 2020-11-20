using System;
using System.Threading;

namespace Bankomaten
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ContinueOn = default(bool);
            bool quit = default(bool);
            string errorMsg = default(string);
            char menuChoice = default(char);

            do
            {
                Console.Clear();
                Console.WriteLine("Välkommen till bankomaten med obegränsat med pengar.");
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ColorRed(errorMsg);
                    errorMsg = default(string);
                }
                Console.Write("Ange din PIN-kod: ");
                int userPinInput = Convert.ToInt32(Console.ReadLine());
                ContinueOn = Login(userPinInput);
                if (!ContinueOn)
                    errorMsg = "Felaktig pinkod";
            } while (!ContinueOn);
            ContinueOn = default(bool);
            
            Console.Clear();
            ColorGreen("Inloggningen lyckades");
            Thread.Sleep(1000);
            Console.Clear();

            do
            {
                Console.WriteLine("Menyn ska presenteras");

                Console.ReadKey();
            } while (!quit);

        }

        public static bool Login(int pinCode)
        {
            if (pinCode.ToString().Length == 4 || pinCode.ToString().Length == 6)
                return true;
            else
                return false;

        }

        public static void ColorGreen(string text) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void ColorRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        
    }
}
