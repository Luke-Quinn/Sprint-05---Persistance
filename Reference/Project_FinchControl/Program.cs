using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // *****************************************************
    //
    // Title: Finch Control - Mission 3 : Sprint 5
    // Description: User Programming
    // Application Type: Console
    // Author: Quinn, Luke
    // Dated Created: 3/16/2020
    // Last Modified: 3/22/2020
    //
    // ****************************************************


    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DONE
    }   

    class Program
    {
        /// <summary>
        /// 
        /// first method run when the app starts up
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();

            DisplayLoginRegister();

            DisplayMenuScreen();

            DisplayClosingScreen();

        }

        /// <summary>
        /// SET THE THEME 
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary> 
        ///  MAIN MENU SCREEN     
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;
            Finch finchRobot = new Finch(); //instatiate an object

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DisplayDataRecorderMenuScreen(finchRobot);
                        break;

                    case "d":
                        DisplayMainAlarmMenuScreen(finchRobot);
                        break;
                        
                    case "e":
                        DisplayUserProgrammingMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        static void DisplayUserProgrammingMenuScreen(Finch finchRobot)
        {
            string menuChoice;
            bool quitMenu = false;

            //
            //tuple to store all three command parameters
            //
            (int motorSpeed, int ledBrightness, double waitInSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitInSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // Get the user menu choice
                //

                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Quit");
                Console.Write("\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        UserProgramingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        UserProgrammingDisplayFinchCommands(commands);
                        break;

                    case "d":
                        UserProgrammingDisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);

        }      


        #region USER PROGRAMMING

        /// <summary>
        /// ****************************************************
        ///     User Programming : GET Command Parameters      *     ----- FIgure out what to do with this hot garbage, Luke !
        /// ****************************************************
        /// </summary>
        /// <returns> tuple of command parameters </returns>
        static (int motorSpeed, int ledBrightness, double waitInSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            string userInput = "";
            DisplayScreenHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitInSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitInSeconds = 0;

            Console.Write("\tEnter Motor Speed[1 - 255]: ");
            userInput = Console.ReadLine();

            if(!int.TryParse(userInput, out commandParameters.motorSpeed))
            {
                Console.WriteLine("\t\t************************");
                Console.WriteLine("\t\t*Please Enter An Integer*");
                Console.WriteLine("\t\t*************************");
            }

            else
            {
                Console.Write("\tEnter LED Brightness [1 - 255]: ");
                userInput = Console.ReadLine();
                if (!int.TryParse(userInput, out commandParameters.ledBrightness))
                {
                    Console.WriteLine("\t\t************************");
                    Console.WriteLine("\t\t*Please Enter An Integer*");
                    Console.WriteLine("\t\t*************************");
                }

                else
                {
                    Console.Write("\tEnter Wait Time in Seconds: ");
                    userInput = Console.ReadLine();
                    if (!double.TryParse(userInput, out commandParameters.waitInSeconds))
                    {
                        Console.WriteLine("\t\t************************");
                        Console.WriteLine("\t\t*Please Enter An Integer*");
                        Console.WriteLine("\t\t*************************");
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine($"\tMotor Speed: {commandParameters.motorSpeed}");
                        Console.WriteLine($"\tLED Brightness: {commandParameters.ledBrightness}");
                        Console.WriteLine($"\tWait Duration: {commandParameters.waitInSeconds}");

                        DisplayMenuPrompt("User Programming");
                    }

                } 
                
            }    
            
            return commandParameters;
        }


        /// <summary>
        /// ********************************************
        ///      User Programming : GET COMMANDS       *
        /// ********************************************
        /// </summary>
        /// <param name="commands"></param>
        static void UserProgramingDisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Finch Robot Commands");

            //
            // List Commands
            //

            int commandCount = 1;
            Console.WriteLine("\tList of Available Commands");
            Console.WriteLine();
            Console.Write("\t");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"-{commandName.ToLower()} -\n ");
                if (commandCount == 0) Console.Write("-\n\t-");
                commandCount++;

            }
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.Write("\tEnter Command: ");
                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }

                else
                {
                    Console.WriteLine("\t\t***********************************");
                    Console.WriteLine("\t\tEnter a command from the above list");
                    Console.WriteLine("\t\t***********************************");
                }

            }

            DisplayMenuPrompt("User Programming");

        }

        /// <summary>
        /// ********************************************
        ///   User Programming : DISPLAY COMMANDS      *
        /// ********************************************
        /// </summary>
        /// <returns> tuple of command parameters </returns>
        static void UserProgrammingDisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }

            DisplayMenuPrompt("User Programming");
        }

        /// <summary>
        /// ********************************************
        ///   User Programming : EXECUTE COMMANDS      *
        /// ********************************************
        /// </summary>
        /// <returns> tuple of command parameters </returns>
        static void UserProgrammingDisplayExecuteFinchCommands(Finch finchRobot, 
            List<Command> commands, 
            (int motorSpeed, int ledBrightness, double waitInSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitInSeconds * 1000);            

            string commandFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayScreenHeader("Executing Finch Comands");

            Console.WriteLine("\tThe Finch is primed to execute the commands...");
            DisplayContinuePrompt();

            foreach ( Command command in commands)
            {

                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        commandFeedback = Command.WAIT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        finchRobot.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNRIGHT.ToString();
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandFeedback = Command.LEDON.ToString();
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandFeedback = $"Temperature: {finchRobot.getTemperature().ToString("n2")}";
                        break;

                    case Command.DONE:
                        commandFeedback = Command.DONE.ToString();
                        break;

                    default:

                        break;
                }

                Console.WriteLine($"\t{commandFeedback}");
            }

            DisplayMenuPrompt("User Programming");
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        ///  TALENT SHOW MENU SCREEN
        /// </summary>
        /// <param name="myFinch"></param>
        static void DisplayTalentShowMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Moovin' and Groovin' ");
                Console.WriteLine("\tc) Mixing it Up");
                Console.WriteLine("\td) Song Bird");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(myFinch);
                        break;

                    case "b":
                        DisplayDance(myFinch);
                        break;

                    case "c":
                        DisplayMixingItUp(myFinch);
                        break;

                    case "d":
                        DisplaySongBird(myFinch);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch myFinch)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("\tLights and Sounds");

            Console.WriteLine("\tTHE FINCH ROBOT WILL NOW YELL AND GLOW");
            DisplayContinuePrompt();

            myFinch.setLED(204, 0, 102);
            myFinch.wait(1000);
            myFinch.setLED(0, 204, 102);
            myFinch.wait(1000);
            myFinch.setLED(204, 102, 0);
            myFinch.wait(1000);


            for (int lightSoundLevel = 0; lightSoundLevel < 70; lightSoundLevel++)
            {
                myFinch.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                myFinch.noteOn(lightSoundLevel * 200);
            }

            for (int lightSoundLevel = 0; lightSoundLevel < 40; lightSoundLevel++)
            {
                myFinch.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                myFinch.noteOn(lightSoundLevel * 200);
            }

            for (int lightSoundLevel = 0; lightSoundLevel < 40; lightSoundLevel++)
            {
                myFinch.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                myFinch.noteOn(lightSoundLevel * 100);
            }

            myFinch.noteOff();
            string userResponse;

            Console.Write("\tWhich color would you like to illumiate the Finch with? (Red, Green, or Blue): ");
            userResponse = Console.ReadLine();

            if (userResponse == "red")
            {
                myFinch.setLED(255, 0, 0);
                myFinch.wait(3000);
                myFinch.setLED(0, 0, 0);
            }
            if (userResponse == "blue")
            {
                myFinch.setLED(0, 0, 255);
                myFinch.wait(3000);
                myFinch.setLED(0, 0, 0);
            }
            if (userResponse == "green")
            {
                myFinch.setLED(0, 255, 0);
                myFinch.wait(3000);
                myFinch.setLED(0, 0, 0);
            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Moovin and Groovin                *
        /// *****************************************************************
        /// </summary>
        /// <param name="myFinch">finch robot object</param>
        static void DisplayDance(Finch myFinch)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("\tMoovin' and Groovin'");
            Console.WriteLine("\tTHE FINCH ROBOT WILL NOW EXECUTE SOME SWEET MOVES ");
            DisplayContinuePrompt();

            string userInput;

            Console.WriteLine("\tThe Square Dance");

            for (int count = 0; count < 4; count++)
            {
                myFinch.setMotors(200, 200);
                myFinch.wait(500);
                myFinch.setMotors(0, 255);
                myFinch.wait(500);
                myFinch.setMotors(0, 0);
            }

            for (int count = 0; count < 4; count++)
            {
                myFinch.setMotors(200, 200);
                myFinch.wait(500);
                myFinch.setMotors(255, 0);
                myFinch.wait(500);
                myFinch.setMotors(0, 0);
            }

            Console.Clear();
            Console.CursorVisible = true;
            DisplayScreenHeader("\tMoovin' and Groovin'");
            Console.Write("\t Would you like to do some donuts with the Finch?: ");
            userInput = Console.ReadLine();

            int userDonuts;

            Console.WriteLine();
            Console.WriteLine("\tYou said, {0}!\n\tGood Choice!", MyReturnStringMethod(userInput));
            Console.WriteLine();
            Console.Write("\tPress any key to continue:");
            Console.ReadKey();

            if (userInput == "yes")
            {
                Console.WriteLine();
                Console.Write("\tHow many would you like to do? 1-5: ");
                userDonuts = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\tYou Have Chosen {0} donuts....", MyReturnNumericMethod(userDonuts));

                if (userDonuts <= 5 && userDonuts >= 1)
                {
                    Console.WriteLine("\tPress a key to whip some donuts:");
                    Console.ReadKey();

                    for (int donuts = 0; donuts < userDonuts; donuts++)
                    {
                        myFinch.setMotors(-255, 255);
                        myFinch.wait(1500);
                        myFinch.setMotors(0, 0);
                    }

                    Console.Clear();
                }

                if (userDonuts > 5 || userDonuts < 1)
                {
                    Console.WriteLine("\tSorry that is not an accepted number");
                }
            }

            else
            {
                Console.WriteLine("\tThanks for Dancing with the Finch Robot!");
                Console.WriteLine();
            }

            DisplayMenuPrompt("\tTalent Show Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Mixing It Up                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="myFinch">finch robot object</param>
        static void DisplayMixingItUp(Finch myFinch)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Mixing It Up");
            Console.WriteLine("\tTHE FINCH ROBOT WILL NOW MIX IT UP ON YA' ");
            DisplayContinuePrompt();
            myFinch.setLED(255, 0, 0);
            myFinch.noteOn(261);
            myFinch.wait(1000);
            myFinch.noteOff();
            myFinch.setLED(0, 255, 0);
            myFinch.setMotors(50, 200);

            for (int frequency = 0; frequency < 20000; frequency = frequency + 100)
            {
                myFinch.noteOn(frequency);
                myFinch.wait(2);
                myFinch.noteOff();
            }

            myFinch.wait(3000);
            myFinch.setMotors(0, 0);
            myFinch.setMotors(-200, -200);
            myFinch.wait(1000);
            myFinch.setMotors(0, 0);
            myFinch.setLED(0, 0, 0);
            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Song Bird                         *
        /// *****************************************************************
        /// </summary>
        /// <param name="myFinch">finch robot object</param>
        static void DisplaySongBird(Finch myFinch)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Song Bird");
            Console.WriteLine("\tTHE FINCH ROBOT WILL SING A SPECIAL SONG FOR YOU");
            Console.WriteLine();
            Console.WriteLine("\tImperial March - By VADER");
            DisplayContinuePrompt();
            ImperialMarch(myFinch);
            DisplayMenuPrompt("Talent Show Menu");
        }
        #endregion

        #region DATA RECORDER

        /// <summary>
        /// *****************************************************************
        /// *                     Data Recorder Menu                        *
        /// *****************************************************************
        /// </summary>
        /// <param name="myFinch">finch robot object</param>

        static void DisplayDataRecorderMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitDataRecorderMenu = false;
            string menuChoice;
            int numberOfDataPoints = 0;
            double frequencyOfDataPoints = 0;            
            double[] temperatures = null;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points ");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //

                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        frequencyOfDataPoints = DataRecorderDisplayGetFrequencyOfDataPoints();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, frequencyOfDataPoints, myFinch);
                        break;

                    case "d":
                        DataRecorderDisplayData(temperatures);
                        break;

                    case "q":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);
        }

        /// <summary>
        /// Show Temperature Data
        /// </summary>
        /// <param name="temperatures"></param>
        static void DataRecorderDisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Show Temperature Data");

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display Table For temperatures
        /// </summary>
        /// <param name="temperatures"></param>
        static void DataRecorderDisplayTable(double[] temperatures)
        {
            //
            // Display the table headers
            //

            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temp in  °F".PadLeft(15)
                );
            Console.WriteLine(
                "___________".PadLeft(15) +
                "___________".PadLeft(15)
                );

            //
            // Display Table Data
            //

            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    temperatures[index].ToString("n2").PadLeft(15)
                    );
            }

        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double frequencyOfDataPoints, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints]; // Instatiating a new array
            DisplayScreenHeader("\tGet Temperatures");

            Console.WriteLine($"\tNumber of Temperatures: {numberOfDataPoints}");
            Console.WriteLine($"\tTemperature Frequency: {frequencyOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine("\tThe Finch is now ready to begin recording the temperature data");
            DisplayContinuePrompt();


            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature() * 9 / 5 + 32;

                Console.WriteLine($"\tReading {index + 1}:      {temperatures[index].ToString("n2")} °F".PadLeft(10));

                int waitInSeconds = (int)(frequencyOfDataPoints * 1000);
                finchRobot.wait(waitInSeconds);
            }

            DisplayContinuePrompt();

            return temperatures;
        }

        /// <summary>
        /// Get the number of data points 
        /// </summary>
        /// <returns> Number of data points </returns>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;

            DisplayScreenHeader("\tNumber Of Data Points");

            Console.Write("\tHow many times would you like us to record the temp? (1-50): ");

            int.TryParse(Console.ReadLine(), out numberOfDataPoints);

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        /// <summary>
        /// Get the freq of the data points
        /// </summary>
        /// <returns> freq of data points </returns>
        static double DataRecorderDisplayGetFrequencyOfDataPoints()
        {
            double frequencyOfDataPoints;

            DisplayScreenHeader("\tFrequency of Temperatures");

            Console.Write("\tHow often, in seconds, would you like to record the temperature?(1-30): ");

            double.TryParse(Console.ReadLine(), out frequencyOfDataPoints);

            DisplayContinuePrompt();

            return frequencyOfDataPoints;

        }


        #endregion

        #region ALARM SYSTEM

        #region MAIN ALARM
        /// <summary>
        /// MAIN ALARM MENU SCREEN
        /// </summary>
        /// <param name="finchRobot"></param>
        static void DisplayMainAlarmMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;
            string whatToMonitor = "";
            DisplayScreenHeader("Alarm Menu");

            Console.Write("\tWill we be setting an alarm for temperature, light, or both?: ");
            whatToMonitor = Console.ReadLine();

            if(whatToMonitor == "light")
            {
                DisplayLightAlarmMenuScreen(finchRobot);
            }

            if(whatToMonitor == "temperature")
            {
                DisplayTempAlarmMenuScreen(finchRobot);
            }

            if(whatToMonitor == "both")
            {
                DisplayComboAlarmMenuScreen(finchRobot);                                          
            }

        }
        #endregion

        #region LIGHT ALARM
        /// <summary>
        /// LIGHT ALARM MENU SCREEN
        /// </summary>
        /// <param name="finchRobot"></param>

        static void DisplayLightAlarmMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Light Alarm Menu");
                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Max / Min Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //

                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplaySetMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmDisplaySetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

       /// <summary>
       ///  User sets sensors for Light Alarm
       /// </summary>
       /// <returns>sensors</returns>

        static string LightAlarmDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor;

            DisplayScreenHeader("\tSensors to Monitor");

            Console.Write("\tEnter which sensors to monitor [left, right, both]: ");
            sensorsToMonitor = Console.ReadLine();

            Console.WriteLine($"\tSensors chosen to monitor = {sensorsToMonitor}");

            DisplayMenuPrompt("\tLight Alarm");

            return sensorsToMonitor;           
        }

        /// <summary>
        /// Sets the Range type MIN || MAX
        /// </summary>
        /// <returns>range type</returns>
        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType = "";

            DisplayScreenHeader("\tSet Range Type");

            Console.Write("\tRange Type [minimum, maximum]: ");
            rangeType = Console.ReadLine(); 
           

            DisplayMenuPrompt("\tLight Alarm");
            return rangeType;
        }

        /// <summary>
        /// User sets limits for sensors
        /// </summary>
        // <param name="finchRobot"></param>
        /// <returns>MIN/MAX threshold values</returns>
        static int LightAlarmDisplaySetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            int minMaxThresholdValue = 0;

            DisplayScreenHeader("\tMinimum / Maximum Threshold Value");

            Console.WriteLine($"\tLeft Light Ambient Light Sensor: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tRight Light Ambient Light Sensor: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the {rangeType} light sensor value: ");
            int.TryParse(Console.ReadLine(), out minMaxThresholdValue);
            //echo value

            DisplayMenuPrompt("\tLight Alarm");

            return minMaxThresholdValue;
        }

        /// <summary>
        /// SET TIME TO MONITOR
        /// </summary>
        /// <returns>Time to monitor</returns>
        static int LightAlarmDisplaySetTimeToMonitor()
        {
            int timeToMonitor = 0;

            DisplayScreenHeader("\tTime to Monitor");
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the desired time to monitor: ");
            int.TryParse(Console.ReadLine(), out timeToMonitor);
            //echo value

            DisplayMenuPrompt("\tLight Alarm");

            return timeToMonitor;
        }

        /// <summary>
        /// SETS ALARM
        /// </summary>        
        /// <param name="setAlarm"></param>
        static void LightAlarmSetAlarm(Finch finchRobot, 
            string sensorsToMonitor,
            string rangeType, 
            int minMaxThresholdValue,
            int timeToMonitor)
        {

            int secondsElapsed = 0;
            int currentLightSensorValue = 0;
            bool thresholdExceeded = false;

            DisplayScreenHeader("\tSet Alarm");

            Console.WriteLine($"\tSensors to monitor : {sensorsToMonitor}");
            Console.WriteLine($"\tRange Type: {rangeType}");
            Console.WriteLine($"\tMin/Max Threshold Value:  {minMaxThresholdValue}");
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("\t\tPress any key to start monitoring.");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < timeToMonitor) && !thresholdExceeded)
            {

                switch(sensorsToMonitor)
                {
                    case "left":
                        currentLightSensorValue = finchRobot.getLeftLightSensor();
                        break;
    
                    case "right":
                        currentLightSensorValue = finchRobot.getRightLightSensor();
                        break;

                    case "both":
                        currentLightSensorValue = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                        break;           

                }

                switch(rangeType)
                {
                    case "minimum":
                        if(currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if(currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true ;
                        }
                        break;
                }

                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Console.Clear();
                
                Console.WriteLine("\tThreshold Exceeded!!");
                Console.WriteLine();
                Console.WriteLine($"\t The {rangeType} threshold has been exceeded by the current light sensor value.\n\t Current value is {currentLightSensorValue} ");

                AlarmMissionThree(finchRobot);
            }

            else
            {
                Console.WriteLine($"The {rangeType} threshold value of {minMaxThresholdValue} was not exceeded.");
            }

            DisplayMenuPrompt("Light Alarm");
        }
        #endregion

        #region TEMPERATURE ALARM
        /// <summary>
        /// TEMPERATURE ALARM MENU
        /// </summary>
        /// <param name="myFinch">finch robot object</param>
        /// 
        static void DisplayTempAlarmMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            string tempRangeType = "";
            int tempMinMaxValue = 0;
            int tempTimeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Temp Alarm Menu");

                //
                // get user menu choice
                //  
                Console.WriteLine("\ta) Set Range Type");
                Console.WriteLine("\tb) Set Max / Min Threshold Value");
                Console.WriteLine("\tc) Set Time to Monitor");
                Console.WriteLine("\td) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //

                switch (menuChoice)
                {
                    case "a":
                        tempRangeType = TempAlarmDisplaySetRangeType();
                        break;

                    case "b":
                        tempMinMaxValue = TempAlarmDisplaySetMinMaxThresholdValue(tempRangeType, finchRobot);
                        break;

                    case "c":
                        tempTimeToMonitor = TempAlarmDisplaySetTimeToMonitor();
                        break;

                    case "d":
                        TempAlarmSetAlarm(finchRobot, tempRangeType, tempMinMaxValue, tempTimeToMonitor);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }

        /// <summary>
        /// TEMPERATURE SET RANGE TYPE
        /// </summary>
        /// <returns>MIN/MAX RANGE TYPE</returns>
        static string TempAlarmDisplaySetRangeType()
        {
            string tempRangeType = "";

            DisplayScreenHeader("\tSet Range Type");

            Console.Write("\tRange Type [minimum, maximum]: ");
            tempRangeType = Console.ReadLine();

            DisplayMenuPrompt("\tTemp Alarm");
            return tempRangeType;
        }

        /// <summary>
        /// TEMPERATURE MIN/MAX THRESHOLDS
        /// </summary>        
        /// <returns> tempMinMaxValue </returns>
        static int TempAlarmDisplaySetMinMaxThresholdValue(string tempRangeType, Finch finchRobot)
        {
            int tempMinMaxValue = 0;

            DisplayScreenHeader("\tMinimum / Maximum Threshold Value");

            Console.WriteLine($"\tCurrent Temperature: {(finchRobot.getTemperature() * 9 / 5)+32} °F ");            
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the {tempRangeType} until alarm sounds: ");
            int.TryParse(Console.ReadLine(), out tempMinMaxValue);
            //echo value
            Console.WriteLine($"\tYou Entered...{tempMinMaxValue}");


            DisplayMenuPrompt("\tTemp Alarm");

            return tempMinMaxValue;
        }

        /// <summary>
        /// TEMPERATURE SET TIME TO MONITOR
        /// </summary>
        /// <returns>tempTimeToMonitor</returns>
        static int TempAlarmDisplaySetTimeToMonitor()
        {
            int tempTimeToMonitor = 0;

            DisplayScreenHeader("\tTime to Monitor");
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the desired time to monitor: ");
            int.TryParse(Console.ReadLine(), out tempTimeToMonitor);
            //echo value

            DisplayMenuPrompt("\tTemp Alarm");

            return tempTimeToMonitor;
        }

        /// <summary>
        /// TEMPERATURE SET ALARM
        /// </summary>
        /// 
        static void TempAlarmSetAlarm(Finch finchRobot,
            string tempRangeType, 
            int tempMinMaxValue,
            int tempTimeToMonitor)
        {

            int secondsElapsed = 0;
            int currentTempValue = 0;
            bool thresholdExceeded = false;

            DisplayScreenHeader("\tSet Temp Alarm");
            
            Console.WriteLine($"\tRange Type: {tempRangeType}");
            Console.WriteLine($"\tMin/Max Threshold Value:  {tempMinMaxValue}");
            Console.WriteLine($"\tTime to Monitor: {tempTimeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("\t\tPress any key to start monitoring.");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < tempTimeToMonitor) && !thresholdExceeded)
            {             

                switch (tempRangeType)
                {
                    case "minimum":
                        if (currentTempValue < tempMinMaxValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentTempValue > tempMinMaxValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }

                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Console.Clear();
                
                Console.WriteLine("\tThreshold Exceeded!!");
                Console.WriteLine();
                Console.WriteLine($"\t The {tempRangeType} threshold has been exceeded. Current temperature is {(finchRobot.getTemperature()*9/5) + 32} °F ");

                AlarmMissionThree(finchRobot);
            }

            else
            {
                Console.WriteLine($"The {tempRangeType} threshold value of {tempMinMaxValue} was not exceeded.");
            }

            DisplayMenuPrompt("Temp Alarm");
        }
        #endregion

        #region COMBO ALARM

        /// <summary>
        /// COMBO ALARM DISPLAY MENU
        /// </summary>
        /// <param name="finchRobot"></param>
        static void DisplayComboAlarmMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;

            string comboMenuChoice = "";
            string comboSensorsToMonitor = "";
            string comboRangeType = "";

            int comboMinMaxLightValue = 0;
            int comboMinMaxValueTemp = 0;
            int comboTime = 0;

            do
            {
                DisplayScreenHeader("Combo Alarm Menu");

                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Max / Min Light Value");
                Console.WriteLine("\td) Set Max / Min Temp Value");
                Console.WriteLine("\te) Set Time to Monitor");
                Console.WriteLine("\tf) Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                comboMenuChoice = Console.ReadLine().ToLower();

                switch (comboMenuChoice)
                {
                    case "a":
                        comboSensorsToMonitor = ComboAlarmDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        comboRangeType = ComboAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        comboMinMaxLightValue = ComboAlarmDisplaySetMinMaxLightValue(comboRangeType, finchRobot);
                        break;

                    case "d":
                        comboMinMaxValueTemp = ComboAlarmDisplaySetMinMaxTemp(comboRangeType, finchRobot);
                        break;

                    case "e":
                        comboTime = ComboAlarmDisplaySetTimeToMonitor();
                        break;

                    case "f":
                        ComboAlarmSetAlarm(finchRobot, comboSensorsToMonitor, comboRangeType, comboMinMaxLightValue, comboMinMaxValueTemp, comboTime);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;

                }

            } while (!quitMenu);

        }

        /// <summary>
        /// COMBO SET RANGE
        /// </summary>
        /// <returns>comboRangeType</returns>
        static string ComboAlarmDisplaySetRangeType()
        {
            string comboRangeType;

            DisplayScreenHeader("\tSet Range Type");

            //validate
            Console.Write("\tRange Type [minimum, maximum]: ");
            comboRangeType = Console.ReadLine();
            //echo back
            Console.WriteLine($"\tYou Entered...{comboRangeType}");

            DisplayMenuPrompt("Combo Alarm");
            return comboRangeType;
        }

        /// <summary>
        /// COMBO SET SENSORS
        /// </summary>
        /// <returns> comboSensorsToMonitor </returns>
        static string ComboAlarmDisplaySetSensorsToMonitor()
        {
            string comboSensorsToMonitor;

            DisplayScreenHeader("\tSensors to Monitor");

            Console.Write("\tEnter which sensors to monitor [left, right, both]: ");
            comboSensorsToMonitor = Console.ReadLine();

            Console.WriteLine($"\tSensors chosen to monitor = {comboSensorsToMonitor}");

            DisplayMenuPrompt("\tCombo Alarm");

            return comboSensorsToMonitor;
        }

        /// <summary>
        /// COMBO SET MIN/MAX LIGHT VALUE
        /// </summary>        
        /// <param name="finchRobot"></param>
        /// <returns> comboMinMaxLightValue </returns>
        static int ComboAlarmDisplaySetMinMaxLightValue(string comboRangeType, Finch finchRobot)
        {
            int comboMinMaxLightValue;            

            DisplayScreenHeader("\tMinimum / Maximum Threshold Value");

            Console.WriteLine($"\tLeft Light Ambient Light Sensor: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine($"\tRight Light Ambient Light Sensor: {finchRobot.getLeftLightSensor()}");
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the {comboRangeType} light sensor value: ");
            int.TryParse(Console.ReadLine(), out comboMinMaxLightValue);
            //echo value
            Console.WriteLine($"\tYou Entered....{comboMinMaxLightValue}");
            Console.WriteLine();

            DisplayMenuPrompt("\tCombo Alarm");

            return comboMinMaxLightValue;
        }

        /// <summary>
        /// COMBO SET MIN/MAX TEMP VALUE
        /// </summary>
        /// <param name="finchRobot"></param>
        /// <returns> comboMinMaxValueTemp </returns>
        static int ComboAlarmDisplaySetMinMaxTemp(string comboRangeType, Finch finchRobot)
        {
            int comboMinMaxValueTemp;

            DisplayScreenHeader("\tMinimum / Maximum Threshold Value");            
            Console.WriteLine();

            Console.WriteLine($"\tCurrent Temperature: {(finchRobot.getTemperature() * 9 / 5) + 32} °F ");
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the {comboRangeType} temperature that will sound the alarm: ");
            int.TryParse(Console.ReadLine(), out comboMinMaxValueTemp);
            //echo value 
            Console.WriteLine($"\tYou Entered...{comboMinMaxValueTemp} °F");

            DisplayMenuPrompt("\tCombo Alarm");

            return comboMinMaxValueTemp;
        }

        /// <summary>
        /// COMBO SET TIME TO MONITOR
        /// </summary>
        /// <returns> comboTimeToMonitor </returns>
        static int ComboAlarmDisplaySetTimeToMonitor()
        {
            int comboTimeToMonitor = 0;

            DisplayScreenHeader("\tTime to Monitor");
            Console.WriteLine();
            //Validate value
            Console.Write($"\tEnter the desired time to monitor: ");
            int.TryParse(Console.ReadLine(), out comboTimeToMonitor);
            //echo value

            DisplayMenuPrompt("\tCombo Alarm");

            return comboTimeToMonitor;
        }

        /// <summary>
        /// COMBO SET ALARM
        /// </summary>
        /// <param name="finchRobot"></param>
      
        static void ComboAlarmSetAlarm(Finch finchRobot,
            string comboSensorsToMonitor,
            string comboRangeType,
            int comboMinMaxLightValue,
            int comboMinMaxValueTemp,
            int comboTime)
        {

            int secondsElapsed = 0;
            int currentTempValue = Convert.ToInt32(finchRobot.getTemperature());
            int comboLightSensor = 0;
            bool thresholdExceeded = false;

            DisplayScreenHeader("\tSet Combo Alarm");

            
            Console.WriteLine($"\tMin/Max Threshold Light:  {comboMinMaxLightValue}");
            Console.WriteLine($"\tMin/Max Threshold Temp:  {comboMinMaxValueTemp}");
            Console.WriteLine($"\tTime to Monitor: {comboTime}");
            Console.WriteLine();

            Console.WriteLine("\t\tPress any key to start monitoring.");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < comboTime) && !thresholdExceeded)
            {
                switch (comboSensorsToMonitor)
                {
                    case "left":
                        comboLightSensor = finchRobot.getLeftLightSensor();
                        break;

                    case "right":
                        comboLightSensor = finchRobot.getRightLightSensor();
                        break;

                    case "both":
                        comboLightSensor = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                        break;

                }

                switch (comboRangeType)
                {
                    case "minimum":

                        if (currentTempValue < comboMinMaxValueTemp && comboLightSensor < comboMinMaxLightValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentTempValue > comboMinMaxValueTemp && comboLightSensor > comboMinMaxLightValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }

                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Console.Clear();

                Console.WriteLine("\tThreshold Exceeded!!");
                Console.WriteLine();
                Console.WriteLine($"\t The {comboRangeType} threshold has been exceeded. Current temperature is {(finchRobot.getTemperature() * 9 / 5) + 32} °F ");
                Console.WriteLine($"\t The {comboRangeType} threshold has been exceeded by the current light sensor value.\n\t Current value is {comboLightSensor}");

                AlarmMissionThree(finchRobot);
            }

            else
            {
                Console.WriteLine($"The {comboRangeType} threshold temp of {comboMinMaxValueTemp} was not exceeded.");
                Console.WriteLine($"The {comboRangeType} threshold value of {comboMinMaxLightValue} was not exceeded.");
            }

            DisplayMenuPrompt("Combo Alarm");
        }


        #endregion

        #endregion


        #region Persistence 

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
        /// *****************************************************************
        /// *                          Login Screen                         *
        /// *****************************************************************
        /// </summary>
        /// 
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
        /// read login info from data file
        /// Note: no error or validation checking
        /// </summary>
        /// <returns>tuple of user name and password</returns>

        static (string userName, string password) ReadLoginInfoData()
        {
            string dataPath = @"Data/Logins.txt";

            string loginInfoText;
            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            loginInfoText = File.ReadAllText(dataPath);

            //
            // use the Split method to separate the user name and password into an array
            //
            loginInfoArray = loginInfoText.Split(',');
            loginInfoTuple.userName = loginInfoArray[0];
            loginInfoTuple.password = loginInfoArray[1];

            return loginInfoTuple;
        }

        /// <summary>
        /// check user login
        /// </summary>
        /// <param name="userName">user name entered</param>
        /// <param name="password">password entered</param>
        /// <returns>true if valid user</returns>
        static bool IsValidLoginInfo(string userName, string password)
        {
            (string userName, string password) userInfo;
            bool validUser;

            userInfo = ReadLoginInfoData();

            validUser = (userInfo.userName == userName) && (userInfo.password == password);

            return validUser;
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

            File.WriteAllText(dataPath, loginInfoText);
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnected.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

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
        /// *****************************************************************
        /// *                     Continue Prompt                           *
        /// *****************************************************************
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// *************************************************************
        /// *                     Menu Prompt                           *
        /// *************************************************************
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// ***************************************************************
        /// *                     Screen Header                           *
        /// ***************************************************************
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }


        #endregion

        #region SPACE FOR OTHER METHODS

        ///
        
     
        /// <summary>
        /// ************************************************************
        /// *                     Alarm for M3                         *
        /// ************************************************************
        /// </summary>
        static void AlarmMissionThree(Finch finchRobot)
        {
            for (int lightSoundLevel = 0; lightSoundLevel < 70; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 200);
            }

            for (int lightSoundLevel = 0; lightSoundLevel < 40; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 200);
            }

            for (int lightSoundLevel = 0; lightSoundLevel < 40; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 200);
            }

            finchRobot.noteOff();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Imperial March                            *
        /// *****************************************************************
        /// </summary>
        static void ImperialMarch(Finch myFinch)
        {
            for (int count = 0; count < 3; count++)
            {
                myFinch.noteOn(880);
                myFinch.wait(500);
                myFinch.noteOff();
            }

            // f-c
            myFinch.noteOn(698);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1046);
            myFinch.wait(500);
            myFinch.noteOff();
            //

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(698);
            myFinch.wait(250);
            myFinch.noteOff();

            myFinch.noteOn(1046);
            myFinch.wait(250);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            //2nd line

            for (int count = 0; count < 3; count++)
            {
                myFinch.noteOn(1318);
                myFinch.wait(500);
                myFinch.noteOff();

            }

            myFinch.noteOn(1396);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1046);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(698);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1046);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            //

            myFinch.noteOn(1760);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1760);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1760);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1567);
            myFinch.wait(500);
            myFinch.noteOff();

            //line3

            myFinch.noteOn(1567);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1396);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1567);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(987);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1318);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1174);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1174); //D
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1046);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(987); //b
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(1046); //c
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(698);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(698);
            myFinch.wait(500);
            myFinch.noteOff();

            myFinch.noteOn(880);
            myFinch.wait(500);
            myFinch.noteOff();
        }


        /// <summary>
        /// BS Num RETURN METHOD
        /// </summary>
        /// <param name="userDonuts"></param>
        /// <returns></returns>
        private static int MyReturnNumericMethod(int userDonuts)
        {
            int result = userDonuts;
            return result;
        }

        /// <summary>
        /// BS STRING RETURN METHOD
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns></returns>
        private static string MyReturnStringMethod(string userInput)
        {
            string result = userInput;
            return result;
        }


        #endregion


        
    }
}
