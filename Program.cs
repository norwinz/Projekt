﻿/*
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
                                Console.Clear();
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
            
            Console.WriteLine("Konton: ");
            for (int i = 1; i < 6; i++)
            {
                if (accDB[userID, i] != null)
                {
                    int listFix = i - 1;

                    Console.WriteLine("{0}. {1}: {2} SEK",i, accList[listFix], accDB[userID, i]);

                }
            }

        }
        static double?[,] accountTransfer(int userID, double?[,] accDB, string[] accList) //Method to transfer money between accounts
            {
            
            Console.Clear();
            Console.WriteLine("Överförning mellan konton");
            Console.WriteLine("Välj vilket konto du vill flytta pengar FRÅN");
            int fromAccount = getaccountID(userID, accDB, accList);
            Console.WriteLine("Välj vilket konto du vill flytta pengar TILL");
            int toAccount = getaccountID(userID, accDB, accList);
            int listfixFrom = fromAccount - 1;
            int listfixTo = toAccount - 1;
            Console.WriteLine("Från: {0}: {1} SEK", accList[listfixFrom], accDB[userID,fromAccount]);
            Console.WriteLine("Till: {0}: {1} SEK", accList[listfixTo], accDB[userID,toAccount]);
            bool numberBool=false;
            double amountTransfer = 0;
            while (numberBool == false)
            {
                Console.WriteLine("Hur mycket vill du flytta?");
                string amountTransferString = Console.ReadLine();
                
                try
                {
                    amountTransfer = Convert.ToDouble(amountTransferString);
                    numberBool = true;
                    if(amountTransfer<0)
                    {
                        Console.WriteLine("Ej möjligt att flytta mindre än 0 SEK");
                        numberBool = false;
                    }
                }
                catch
                {                    
                    Console.WriteLine("Du måste skriva ett nummer");
                    numberBool = false;
                }
            }
            if (amountTransfer > accDB[userID, fromAccount])
            {
                Console.WriteLine("Valt belopp överstinger kontots saldo.");
                exitOption();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Du kommer flytta {0} SEK från {1} till {2}.", amountTransfer, accList[listfixFrom], accList[listfixTo]);
               
                bool approveBool = false;
                while (approveBool == false)
                    {
                    Console.WriteLine("Skriv 'Godkänn' för att starta överföringen.\nSkriv 'Avbryt' för att avbryta");
                    string approve = Console.ReadLine().ToUpper();
                    if (approve == "GODKÄNN")
                    {
                        approveBool = true;
                        accDB[userID, fromAccount] = accDB[userID, fromAccount] - amountTransfer;
                        accDB[userID, toAccount] = accDB[userID, toAccount] + amountTransfer;
                        Console.WriteLine("Överföringen är genomförd");
                        Console.WriteLine("Nytt saldo:");
                        Console.WriteLine("{0}: {1} SEK", accList[listfixFrom], accDB[userID, fromAccount]);
                        Console.WriteLine("{0}: {1} SEK", accList[listfixTo], accDB[userID, toAccount]);
                    }
                    else if (approve == "AVBRYT")
                    {
                        exitOption();
                    }
                    else
                    {
                        Console.WriteLine("Skriv 'Godkänn' eller 'Avbryt'.");
                    }
                    }

            }
            return accDB;
        }
        static int getaccountID(int userID, double?[,] accDB, string[] accList)
        {
            bool numberBool = false;
            bool accountBool = true;
            int accountInt = 0;

            while (numberBool == false || accountBool == false)
            {
                accountDisplay(userID, accDB, accList);                
                Console.WriteLine("Välj konto");
                try
                {
                    accountInt = Convert.ToInt32(Console.ReadLine());
                    numberBool = true;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Skriv en siffra");
                    numberBool = false;
                }


                if (numberBool == true)
                {
                    if (accountInt < 6)
                    {


                        if (accDB[userID, accountInt] != null)
                        {
                            accountBool = true;

                            
                        }
                        else
                        {
                            accountBool = false;
                            Console.Clear();
                            Console.WriteLine("Du äger inget konto med ID: {0}.", accountInt);
                        }
                    }
                    else
                    {
                        accountBool = false;
                        Console.Clear();
                        Console.WriteLine("Du äger inget konto med ID: {0}.", accountInt);
                    }
                }
            }
            return accountInt;
        }        

            
            
        
        static void exitOption() //Method to go back to main menu
        {
            Console.WriteLine("\n\nTryck enter för att gå tillbaka till menyn");
            Console.ReadLine();
            Console.Clear();
        }
    }
}