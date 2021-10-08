/*
                --Description of program and its purpose--
            Author: Dan Gustafsson, SUT21.
            "Individuellt projekt - Enkel internetbank/bankomat"
            Final project of "Utveckling med C# och .NET"
*/
using System;

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
        }
    }
}
