using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_ArraysToLists
{
    // **************************************************
    //
    // Title: Demo - Arrays to Lists
    // Description: Demonstration of adding and displaying integer
    //              values using arrays and lists
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 3/16/2020
    // Last Modified: 
    //
    // **************************************************
    class Program
    {
        static void Main(string[] args)
        {
            //int[] numbers = new int[3];
            List<int> listNumbers = new List<int>();

            //GetNumbers(numbers);
            GetListNumbers(listNumbers);

            //DisplayNumbers(numbers);
            DisplayListNumbers(listNumbers);
        }

        private static void DisplayNumbers(int[] numbers)
        {
            DisplayScreenHeader("Display Numbers");

            for (int index = 0; index < numbers.Length; index++)
            {
                Console.WriteLine($"Number {index + 1}: {numbers[index]}");
            }

            Console.WriteLine();
            Console.WriteLine($"Average: {numbers.Average()}");
            Console.WriteLine($"Maximum: {numbers.Max()}");
            Console.WriteLine($"Minimum: {numbers.Min()}");
            Console.WriteLine($"Sum: {numbers.Sum()}");

            DisplayContinuePrompt();
        }

        private static void DisplayListNumbers(List<int> listNumbers)
        {
            DisplayScreenHeader("Display List Numbers");

            //
            // this method will sort the list in assenting order
            //
            listNumbers.Sort();

            foreach (int listNumber in listNumbers)
            {
                Console.WriteLine($"\t{listNumber}");
            }

            Console.WriteLine();
            Console.WriteLine($"Average: {listNumbers.Average()}");
            Console.WriteLine($"Maximum: {listNumbers.Max()}");
            Console.WriteLine($"Minimum: {listNumbers.Min()}");
            Console.WriteLine($"Sum: {listNumbers.Sum()}");

            DisplayContinuePrompt();
        }

        private static void GetNumbers(int[] numbers)
        {
            DisplayScreenHeader("Get Numbers");

            for (int index = 0; index < numbers.Length; index++)
            {
                Console.Write($"Number {index + 1}:");
                numbers[index] = int.Parse(Console.ReadLine());
            }

            DisplayContinuePrompt();
        }   

        private static void GetListNumbers(List<int> listNumbers)
        {
            string userResponse = "";
            int numberCount = 1;

            DisplayScreenHeader("Get List Numbers");

            do
            {
                Console.Write($"Number {numberCount}:");
                userResponse = Console.ReadLine();

                if (userResponse != "done")
                {
                    listNumbers.Add(int.Parse(userResponse));
                }

                numberCount++;

            } while (userResponse != "done");

            DisplayContinuePrompt();
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
