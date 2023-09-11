using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    //this class is for display city and smuggler menu
    internal class Display
    {
        private List<City> cities= new List<City>();
        private string SmugglerName = "";
        private bool loseStatus = false;
        private bool loginChecker = false;

        public Smuggler s1= new Smuggler();
        
        // This function displays the menu for the smuggler Smuggler.
        bool smugglerMenu()
        {

            // Check if the Smuggler has been initialized yet. If not, create a new Smuggler and set their inventory.
            int ans = 0;
            if (s1.getName() == null)
            {
                Item iu1 = new Item("Lsd", 10, 200);
                Item iu2 = new Item("Weed", 15, 30);
                Item iu3 = new Item("Heroin", 30, 50);
                Console.WriteLine("Enter the name of the Smuggler : ");
                SmugglerName = Console.ReadLine();

                s1 = new Smuggler(SmugglerName, cities);

                List<Item> lui = new List<Item>();
                lui.Add(iu1 );
                lui.Add(iu2 );
                lui.Add(iu3 );
                s1.setInventories(lui);
            }

            // Clear the console and display the menu options.
            Console.Clear();
            Console.WriteLine("#######################");
            Console.WriteLine("Smuggler menu :");
            Console.WriteLine("1. To access Smuggler Menu.");
            Console.WriteLine("2. To go to Main Page.");
            Console.WriteLine("#######################");

            // Prompt the Smuggler to enter a choice, and validate their input.
            bool checker = true;
            do
            {
                try
                {
                    checker = false;
                    Console.WriteLine("Please enter the value :");
                    string input  = Console.ReadLine();

                    if (!Int32.TryParse(input, out ans)) throw new Exception();    // Check if the input was valid (not an integer).
                    if (ans < 1 || ans > 2) throw new CustomException("error1");   // Check if the input was within the valid range (1-2).
                }
                // If the input is not between 1 to 2, display an error message and try again.
                catch (CustomException ce)
                {
                    checker = true;
                   
                    Console.WriteLine("#########################");
                    CustomException.processError(CustomException.msg, 2);
                    Console.WriteLine("#########################");

                }

                catch (Exception e) {

                    // If the input was not valid, display an error message and try again.
                    checker = true;
                    Console.WriteLine("#########################");
                    Console.WriteLine(HardCodedValue.ERROR2);
                    Console.WriteLine("#########################");

                }
                } while (checker) ;

                // Based on the Smuggler's choice, either display the Smuggler menu or return to the main page.
                switch (ans)
                {
                    case 1:
                        s1.SmugglerMenu();
                        break;
                    case 2:
                        return false;
                }

                // Return whether the Smuggler has lost the game or not.
                return s1.getGameLose();
            }


        void citiesMenu() {
                int ans = 0;
                int i = 0;
                Console.WriteLine("Cities data : ");

                // Loop through the cities List to print out each city's name
                for (i=0; i < cities.Count(); i++)
                {
                    Console.WriteLine((i + 1) + ". " + cities[i].getCityName() + ".");
                }

                // Print out an option to go back to the main page
                Console.WriteLine((i + 1) + ". To go to main page.");

                bool checker = true;
                do
                {
                    try
                    {
                        checker = false;

                        // Ask the Smuggler to input a number corresponding to the city they want to access
                        Console.WriteLine("Please enter the number to access specific city menu : ");
                        string input = Console.ReadLine();

                        // Check if the input is valid
                        if (!Int32.TryParse(input,out ans)) throw new Exception();
                        if (ans < 1 || ans > i + 1) throw new CustomException("error1");
                    }
                    catch (CustomException ce)
                    {
                        checker = true;

                        Console.WriteLine("#########################");
                        CustomException.processError(CustomException.msg, i+1);
                        Console.WriteLine("#########################");

                    }
                    catch (Exception e) {

                        // If the input is not valid, catch the exception and set the checker to true
                        checker = true;
                        Console.WriteLine("#########################");
                        Console.WriteLine(HardCodedValue.ERROR2);
                        Console.WriteLine("#########################");
                    }
                    } while (checker) ;

                    // If the Smuggler chose to go back to the main page, return from the function
                    if (ans == i + 1)
                    {
                        return;
                    }

                    // Otherwise, access the city's menu using the cityMenu function
                    cities[ans - 1].cityMenu();
                }


            public void menu() {
                    int a = 0;
                   // system("cls"); // clears the console screen

                    // Display "GAME OVER" message if the game is lost
                    if (loseStatus)
                    {
                        Console.WriteLine("#####################");
                        Console.WriteLine("##### GAME OVER #####");
                        Console.WriteLine("#####################");
                        loseStatus = false;
                    }

                    // Display "Welcome to Merchant Game" message if Smuggler is not logged in
                    if (loginChecker)
                    {
                        Console.WriteLine("###########################");
                        Console.WriteLine("###########################");
                        Console.WriteLine("Welcome to Merchant Game : ");
                        Console.WriteLine("###########################");
                    }
                    else
                        Console.WriteLine("Welcome to Main Page: ");

                    // Display menu options
                    Console.WriteLine("1. To access cities : ");
                    Console.WriteLine("2. To access Smuggler account : ");
                    Console.WriteLine("3. To exit from the Game : ");

                    bool checker = true;
                    do
                    {
                        try
                        {
                            checker = false;
                            Console.WriteLine("please enter number to play game : ");
                            string input = Console.ReadLine();

                            // Check if input is a valid integer
                            if (!Int32.TryParse(input, out a)) throw new Exception();

                            // Check if input is a valid option
                            if (a < 1 || a > 3) throw new CustomException("error1");
                        }
                        catch (CustomException ce)
                        {
                            checker = true;

                            Console.WriteLine("#########################");
                            CustomException.processError(CustomException.msg, 3);
                            Console.WriteLine("#########################");

                        }
                        catch (Exception e) {
                            checker = true;
                            Console.WriteLine("#########################");
                            Console.WriteLine(HardCodedValue.ERROR2);
                            Console.WriteLine("#########################");
                        }
                        } while (checker) ;

                        // Execute the corresponding menu option
                        switch (a)
                        {
                            case 1:
                                citiesMenu();
                                break;
                            case 2:
                                loseStatus = smugglerMenu();

                                // Reset the Smuggler's name if the game is lost
                                if (loseStatus)
                                {
                                    s1.setName("");
                                }
                                break;
                            case 3:
                                Console.WriteLine("####################");
                                Console.WriteLine("The end of the Game!");
                                Console.WriteLine("####################");
                                return;
                        }

                        loginChecker = true;
                        menu();
                    }

                // Definition of a getter function that returns the List of Cities
                public List < City> getCities() {
                        return cities; // Return the "cities" List
                    }

                   
                    // Definition of a function that sets the List of cities to a new List of Cities
                    void setCities(List < City > c) {
                        cities = c;
                    }

                    // Definition of a getter function that returns the Smugglername
                    string getSmugglerName() {
                        return SmugglerName;
                    }

                    // Definition of a function that sets the Smugglername
                    void setSmugglerName(string uName) {
                        SmugglerName = uName;
                    }

                    // Definition of a getter function that returns the login status
                    bool getLoginChecker() {
                        return loginChecker;
                    }

                    // Definition of a function that sets the login status
                    void setLoginChecker(bool lChecker) {
                        loginChecker = lChecker;
                    }
                }
}
