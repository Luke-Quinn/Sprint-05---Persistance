using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Demo_TryCatch_FileIoExceptions
{
    // **************************************************
    //
    // Title: Demo - File IO with Try/Catch Exception Handling
    // Description: Demonstration of using a data file with a try/catch block and exception handling
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
            string fileIoStatusMessage;
            string data;

            data = ReadData(out fileIoStatusMessage);

            Console.WriteLine();
            Console.WriteLine($"File I/O status: {fileIoStatusMessage}");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static string ReadData(out string fileIOStatusMessage)
        {
            string dataPath = @"Data/Data.txt";
            string data = "";

            try
            {
                data = File.ReadAllText(dataPath);
                fileIOStatusMessage = "Complete";
            }
            catch (DirectoryNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate the folder for the data file.";
            }
            catch (FileNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate the data file.";
            }
            catch (Exception)
            {
                fileIOStatusMessage = "Unable to read data file.";
            }

            return data;
        }
    }
}
