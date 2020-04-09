using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Tuples
{
    // **************************************************
    //
    // Title: CIT 110 - Demo - Tuples
    // Description: Demonstration using and passing tuples
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 3/16/2020
    // Last Modified: 
    //
    // **************************************************
    class Program
    {
        public enum Gender
        {
            MALE,
            FEMALE,
            OTHER
        }

        static void Main(string[] args)
        {
            //Gender genderType;
            //int age;
            //string fullName;
            (string fullName, int age, Gender genderType) information;

            //fullName = GetFullName();
            //age = getAge();
            //genderType = GetGender();
            information = GetInformation();

            //DisplayInfo(fullName, age, genderType);
            DisplayInfoTuples(information);
        }

        private static (string fullName, int age, Gender genderType) GetInformation()
        {
            (string fullName, int age, Gender genderType) information;

            DisplayScreenHeader("Information");

            Console.Write("Full Name:");
            information.fullName = Console.ReadLine();
            Console.Write("Age:");
            information.age = int.Parse(Console.ReadLine());
            Console.Write("Gender:");
            Enum.TryParse(Console.ReadLine(), out information.genderType);

            DisplayContinuePrompt();

            return information;
        }

        private static string GetFullName()
        {
            string fullName;

            DisplayScreenHeader("Full Name");

            Console.Write("Full Name:");
            fullName = Console.ReadLine();

            DisplayContinuePrompt();

            return fullName;
        }

        private static int getAge()
        {
            int age; 

            DisplayScreenHeader("Age");

            Console.Write("Age:");
            age = int.Parse(Console.ReadLine());

            DisplayContinuePrompt();

            return age;
        }

        private static Gender GetGender()
        {
            Gender genderType;

            DisplayScreenHeader("Gender");

            Console.Write("Gender:");
            Enum.TryParse(Console.ReadLine(), out genderType);

            DisplayContinuePrompt();

            return genderType;
        }

        private static void DisplayInfo(string fullName, int age, Gender genderType)
        {
            DisplayScreenHeader("Information");

            Console.WriteLine($"Full name: {fullName}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Gender: {genderType}");

            DisplayContinuePrompt();
        }

        private static void DisplayInfoTuples((string fullName, int age, Gender genderType) information)
        {
            DisplayScreenHeader("Information");

            Console.WriteLine($"Full name: {information.fullName}");
            Console.WriteLine($"Age: {information.age}");
            Console.WriteLine($"Gender: {information.genderType}");

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
