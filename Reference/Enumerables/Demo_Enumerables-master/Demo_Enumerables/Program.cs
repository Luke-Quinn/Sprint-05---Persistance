using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Enumerations
{

    // **************************************************
    //
    // Title: Demo - Enumerables
    // Description: Demonstration of Enumerables
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 3/16/2020
    // Last Modified: 
    //
    // **************************************************
    public enum Month
    {
        None,
        january,
        february,
        march,
        april,
        may,
        june,
        july,
        august,
        september,
        octoboer,
        november,
        december
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Clear();

            List<Month> favoriteMonths = new List<Month>();

            favoriteMonths = GetFavoriteMonths();

            DisplayFavoriteMonths(favoriteMonths);
        }

        private static void DisplayFavoriteMonths(List<Month> favoriteMonths)
        {
            DisplayScreenHeader("Favorite Months");

            foreach (Month month in favoriteMonths)
            {
                Console.WriteLine($"\t{month}");
            }

            DisplayContinuePrompt();
        }

        private static List<Month> GetFavoriteMonths()
        {
            string userResponse = "";
            Month favoriteMonth;
            List<Month> favoriteMonths = new List<Month>();

            DisplayScreenHeader("Months");

            do
            {
                Console.Write("Enter month:");
                userResponse = Console.ReadLine();

                if (userResponse != "done")
                {
                    Enum.TryParse(userResponse.ToLower(), out favoriteMonth);
                    favoriteMonths.Add(favoriteMonth);
                }

            } while (userResponse != "done");

            DisplayContinuePrompt();

            return favoriteMonths;
        }

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
