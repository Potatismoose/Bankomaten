using System;
using System.Collections.Generic;
using System.Threading;

namespace Bankomaten
{
    class Program
    {
        static Random rand = new Random();
        static double balance = Convert.ToDouble(rand.Next(37, 2274));
        static bool userError = default(bool);
        static List<double> balancePerYear = new List<double>();
        static void Main(string[] args)
        {
            bool ContinueOn = default(bool);
            bool quit = default(bool);
            string errorMsg = default(string);
            char menuChoice = default(char);
            double newBalance = default(double);
            double userMoneyInput = default(double);
            

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
                        userMoneyInput = Convert.ToDouble(Console.ReadLine());
                        newBalance = WithDraw(userMoneyInput);
                        if (!userError)
                        {
                            errorMsg = "Du tog ut " + userMoneyInput + "kr och har nu kvar " + newBalance + "kr på kontot";
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
                        Console.Write("Ange insättningssumman: ");
                        userMoneyInput = Convert.ToDouble(Console.ReadLine());
                        newBalance = Deposit(userMoneyInput);
                        if (!userError)
                        {
                            errorMsg = "satte in " + userMoneyInput + "kr och har nu " + newBalance + "kr på kontot";
                            ColorGreen(errorMsg);
                        }
                        else
                        {
                            errorMsg = "Du kan inte sätta in 0 eller ett negativt antal kronor. Saldot är oförändrat.";
                            ColorRed(errorMsg);
                            userError = false;
                        }
                        errorMsg = default(string);
                        Console.WriteLine("Tryck valfri tangent för att fortsätta");
                        Console.ReadKey();
                        break;
                    case '4':
                        Console.Write("Ange antal år du vill hoppa över: ");
                        int years = Convert.ToInt32(Console.ReadLine());
                        Intrest(years);
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


        public static double Deposit(double money)
        {
            if (money > 0)
            {
                balance += money;
                return balance;
            }
            else
            {
                userError = true;
                return balance;
            }
        }

        public static void Intrest(int year)
        {
            for (int i = 0; i < year; i++)
            {
                if (balance <= 1000)
                {
                    balance *= 2;
                    balancePerYear.Add(balance);
                }
                else if(balance >1000 && balance <= 5000)
                {
                    balance *= 3;
                    balancePerYear.Add(balance);
                }
                else if (balance > 5000 && balance <= 10000)
                {
                    balance *= 4;
                    balancePerYear.Add(balance);
                }
                else if (balance > 10000)
                {
                    balance *= 5;
                    balancePerYear.Add(balance);
                }
            }

            Console.WriteLine("\nDitt saldo per år:");
            for (int i = 0; i < balancePerYear.Count; i++)
            {
                Console.WriteLine("År " + (i+1) + ": " + balancePerYear[i]);
            }
            balancePerYear.Clear();

               
        }

    }
}
