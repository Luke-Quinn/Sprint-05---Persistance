using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Demo_FileIO.Login
{
    // **************************************************
    //
    // Title: Demo - File IO Login
    // Description: Demonstration of using a data file for user login
    // Application Type: Console
    // Author: Velis, John
    // Dated Created: 3/22/2020
    // Last Modified: 
    //
    // **************************************************

    class Program
    {
        static void Main(string[] args)
        {
            DisplayLoginRegister();

            //
            // call the application menu here
            //
        }

        /// <summary>
        /// *****************************************************************
        /// *                 Login/Register Screen                         *
        /// *****************************************************************
        /// </summary>
        static void DisplayLoginRegister()
        {
            DisplayScreenHeader("Login/Register");

            Console.Write("\tAre you a registered user [ yes | no ]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                DisplayLogin();
            }
            else
            {
                DisplayRegisterUser();
                DisplayLogin();
            }
        }

        /// <summary>
        /// *****************************************************************
        /// *                          Login Screen                         *
        /// *****************************************************************
        /// </summary>
        static void DisplayLogin()
        {
            string userName;
            string password;
            bool validLogin;

            do
            {
                DisplayScreenHeader("Login");

                Console.WriteLine();
                Console.Write("\tEnter your user name:");
                userName = Console.ReadLine();
                Console.Write("\tEnter your password:");
                password = Console.ReadLine();

                validLogin = IsValidLoginInfo(userName, password);

                Console.WriteLine();
                if (validLogin)
                {
                    Console.WriteLine("\tYou are now logged in.");
                }
                else
                {
                    Console.WriteLine("\tIt appears either the user name or password is incorrect.");
                    Console.WriteLine("\tPlease try again.");
                }

                DisplayContinuePrompt();
            } while (!validLogin);
        }

        /// <summary>
        /// check user login
        /// </summary>
        /// <param name="userName">user name entered</param>
        /// <param name="password">password entered</param>
        /// <returns>true if valid user</returns>
        static bool IsValidLoginInfo(string userName, string password)
        {
            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();
            bool validUser = false;

            registeredUserLoginInfo = ReadLoginInfoData();

            //
            // loop through the list of registered user login tuples and check each one against the login info
            //
            foreach ((string userName, string password) userLoginInfo in registeredUserLoginInfo)
            {
                if ((userLoginInfo.userName == userName) && (userLoginInfo.password == password))
                {
                    validUser = true;
                    break;
                }
            }

            return validUser;
        }

        /// <summary>
        /// *****************************************************************
        /// *                       Register Screen                         *
        /// *****************************************************************
        /// write login info to data file
        /// </summary>
        static void DisplayRegisterUser()
        {
            string userName;
            string password;

            DisplayScreenHeader("Register");

            Console.Write("\tEnter your user name:");
            userName = Console.ReadLine();
            Console.Write("\tEnter your password:");
            password = Console.ReadLine();

            WriteLoginInfoData(userName, password);

            Console.WriteLine();
            Console.WriteLine("\tYou entered the following information and it has be saved.");
            Console.WriteLine($"\tUser name: {userName}");
            Console.WriteLine($"\tPassword: {password}");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// read login info from data file
        /// Note: no error or validation checking
        /// </summary>
        /// <returns>list of tuple of user name and password</returns>
        static List<(string userName, string password)> ReadLoginInfoData()
        {
            string dataPath = @"Data/Logins.txt";

            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();

            loginInfoArray = File.ReadAllLines(dataPath);

            //
            // loop through the array
            // split the user name and password into a tuple
            // add the tuple to the list
            //
            foreach (string loginInfoText in loginInfoArray)
            {
                //
                // use the Split method to separate the user name and password into an array
                //
                loginInfoArray = loginInfoText.Split(',');

                loginInfoTuple.userName = loginInfoArray[0];
                loginInfoTuple.password = loginInfoArray[1];

                registeredUserLoginInfo.Add(loginInfoTuple);

            }

            return registeredUserLoginInfo;
        }

        /// <summary>
        /// write login info to data file
        /// Note: no error or validation checking
        /// </summary>
        static void WriteLoginInfoData(string userName, string password)
        {
            string dataPath = @"Data/Logins.txt";
            string loginInfoText;

            loginInfoText = userName + "," + password;

            //
            // use the AppendAllText method to not overwrite the existing logins
            //
            File.AppendAllText(dataPath, loginInfoText);
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
