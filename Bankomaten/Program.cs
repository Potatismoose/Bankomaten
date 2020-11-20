using System;

namespace Bankomaten
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loggedIn = default(bool);
            string errorMsg = default(string);

            do
            {
                Console.Clear();
                Console.WriteLine("Välkommen till bankomaten med obegränsat med pengar.");
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMsg);
                    Console.ResetColor();
                }
                Console.Write("Ange din PIN-kod: ");
                int userPinInput = Convert.ToInt32(Console.ReadLine());
                loggedIn = Login(userPinInput);
                if (!loggedIn)
                    errorMsg = "Felaktig pinkod";
            } while (!loggedIn);

            Console.WriteLine("You got logged in");
            Console.ReadKey();
        }

        public static bool Login(int pinCode)
        {
            if (pinCode.ToString().Length == 4 || pinCode.ToString().Length == 6)
                return true;
            else
                return false;

        }
    }
}
