using System;
using System.Threading;

namespace Bankomaten
{
    class Program
    {
        static Random rand = new Random();
        static double balance = Convert.ToDouble(rand.Next(37, 2274));
        static bool userError = default(bool);
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
                Console.Clear();
                Console.WriteLine("Välj ett av följande alternativ\n-------------------------------");
                Console.WriteLine("1. Se ditt saldo");
                Console.WriteLine("2. Ta ut pengar");
                Console.WriteLine("3. Sätta in pengar");
                Console.WriteLine("4. Samla ränta \n");
                Console.WriteLine("a. Avsluta \n-------------------------------");
                
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ColorRed(errorMsg);
                    errorMsg = default(string);
                }
                Console.Write("Ange ett val: ");
                try
                {
                    menuChoice = Convert.ToChar(Console.ReadLine());
                }
                catch
                {
                    menuChoice = ',';                    
                }

                switch (menuChoice) {
                    case '1':
                        ColorGreen("Ditt saldo är: " + balance);
                        Thread.Sleep(2000);
                        break;
                    case '2':
                        Console.Write("Ange uttagssumman: ");
                        double userInput = Convert.ToDouble(Console.ReadLine());
                        double newBalance = WithDraw(userInput);
                        if (!userError)
                        {
                            errorMsg = "Du tog ut " + userInput + "kr och har nu kvar " + newBalance + "kr på kontot";
                            ColorGreen(errorMsg);
                        }
                        else {
                            errorMsg = "Du kan inte ta ut mer än vad du har på kontot. Saldot är oförändrat.";
                            ColorRed(errorMsg);
                            userError = false;
                        }
                        errorMsg = default(string);
                        Console.WriteLine("Tryck valfri tangent för att fortsätta");
                        Console.ReadKey();
                        break;
                    case '3':
                        Console.WriteLine("Val 3");
                        Console.ReadKey();
                        break;
                    case '4':
                        Console.WriteLine("Val 4");
                        Console.ReadKey();
                        break;
                    case 'a':
                        quit = true;
                        break;
                    case 'A':
                        quit = true;
                        break;
                    default:
                        errorMsg = "Felaktig inmatning";
                        break;
                }

                
            } while (!quit);
            Console.WriteLine("Bankomaten har avslutats");
            Console.ReadKey();
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

        public static double WithDraw(double money)
        {
            if (balance - money >= 0)
            {
                balance -= money;
                return balance;
            }
            else
            {
                userError = true;
                return balance;
            }
        }


    }
}
