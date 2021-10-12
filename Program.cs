/*
                --Description of program and its purpose--
            Author: Dan Gustafsson, SUT21.
            "Individuellt projekt - Enkel internetbank/bankomat"
            Final project of "Utveckling med C# och .NET"
*/
using System;
using System.Threading;

namespace Projekt
{
    class Program
    {
        static void Main(string[] args) //MAIN PART: UserDB, AccountDB, Login, Menu
        {
            //userDB (string)
            string[,] userDB = new string[5, 4]

            {//     USERID      LOGIN        PIN         NAME
                {   "0",        "EP40",     "4040",     "Elias Pettersson" },
                {   "1",        "NH21",     "2121",     "Nils Höglander" },
                {   "2",        "BH53",     "5353",     "Bo Horvat" },
                {   "3",        "QH43",     "4343",     "Quinn Hughes" },
                {   "4",        "TM57",     "5757",     "Tyler Myers" },
            };

            //accountDB (double)
            double?[,] accountDB = new double?[5, 6]
            {//     USERID      Lönekonto   Sparkonto   Aktiekonto  Bilkonto    Semesterkonto             
                {   0,          4040.4,     null,       null,       null,       null},
                {   1,          2121.21,    21.21,      null,       null,       null},
                {   2,          5353.53,    53.53,      53000,      null,       null},
                {   3,          4343.43,    43.43,      43000,      4.3,        null},
                {   4,          5757.57,    57.57,      57000,      5.7,        0.57},

            };

            string[] accountList = { "Lönekonto", "Sparkonto", "Aktiekonto", "Bilkonto", "Semesterkonto" };
            //VAR LIST
            int ID = 99; //Placeholder value
            int loginAtempt = 3;
            bool successLogin = false;


            while (true)
            {
                //LOGIN SCREEN
                Console.WriteLine("Välkommen till Canucks-bank");
                while (loginAtempt > 0)
                {
                    Console.WriteLine("Vänligen mata in dina inloggningsuppgifter.");
                    Console.WriteLine("Användar-ID: ");
                    string user = Console.ReadLine();
                    Console.WriteLine("PIN: ");
                    string pinString = Console.ReadLine();
                    successLogin = false;
                    for (int i = 0; i < 5; i++)
                    {
                        if (user == userDB[i, 1] && pinString == userDB[i, 2])
                        {
                            ID = i; //Add the logged in userID value

                            i = 5;
                            loginAtempt = 0;
                            successLogin = true;
                        }
                    }
                    if (!successLogin)
                    {
                        loginAtempt--;
                        Console.Clear();
                        Console.WriteLine("Fel ID eller PIN. \nVänligen försök igen.\n");
                        if (loginAtempt > 0)
                        {
                            Console.WriteLine("Du har {0} försök kvar innan programmet stängs.\n", loginAtempt);
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("För många misslyckade inloggningsförsök. Programmet stängs.");
                            return;
                        }

                    }

                }//End of login while loop


                Console.Clear();
                Console.WriteLine("Välkommen {0}!\n", userDB[ID, 3]); //Welcome Message
                //MENU SCREEN
                bool logOut = false;
                while (logOut == false)
                {
                    Console.WriteLine("1.Se dina konton och saldo\n2.Överföring mellan konton\n3.Ta ut pengar\n4.Logga ut");
                    string menuOption = Console.ReadLine();
                    switch (menuOption)
                    {
                        case "1":
                            {
                                Console.WriteLine("Konton och saldo");
                                accountDisplay(ID, accountDB, accountList);
                                exitOption();
                                break;
                            }
                        case "2":
                            {
                                Console.WriteLine("Överföring");
                                accountDB=accountTransfer(ID, accountDB, accountList);
                                exitOption();
                                break;
                            }
                        case "3":
                            {
                                Console.WriteLine("Ta ut pengar");
                                exitOption();
                                break;
                            }
                        case "4":
                            {
                                Console.WriteLine("Du loggas nu ut.");
                                for (int i = 0; i <= 5; i++)
                                {
                                    Thread.Sleep(400);
                                    Console.Write(".");

                                }
                                loginAtempt = 3;
                                logOut = true;
                                Console.Clear();
                                break;

                            }
                        default:
                            {
                                Console.Clear();
                                Console.WriteLine("Ogiltigt val.\nSkriv en siffra i menyn.\n\n");
                                continue;
                            }
                    }

                }
            }

        }
        static void accountDisplay(int userID, double?[,] accDB, string[] accList) //Method to diplay user account info
        {
            Console.Clear();
            Console.WriteLine("Konton: ");
            for (int i = 1; i < 6; i++)
            {
                if (accDB[userID, i] != null)
                {
                    int listFix = i - 1;

                    Console.WriteLine("{0}: {1} SEK", accList[listFix], accDB[userID, i]);

                }
            }

        }
        static double?[,] accountTransfer(int userID, double?[,] accDB, string[] accList) //Method to transfer money between accounts
            {
            double?[,] newDB=accDB;
            newDB[userID, 1] = 100;
            return newDB;
            }
        
        static void exitOption() //Method to go back to main menu
        {
            Console.WriteLine("\n\nTryck enter för att gå tillbaka till menyn");
            Console.ReadLine();
            Console.Clear();
        }
    }
}