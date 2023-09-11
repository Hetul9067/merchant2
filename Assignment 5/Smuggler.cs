using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_5;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;


namespace Assignment_5
{
    internal class Smuggler
    {
        private string name;
        private float SmugglerMoney = 100000;
        private City currentCity = null;
        private City nextCity;
        private City tempCity;
        private int caughtPercentageCounter = 0;
        private int fine = 100;

        private List<City> citiesLi= new List<City>();
        private bool gameLose = false;
        private int randomValue = 0;
        private bool caught = false;


        //aggregate relation 
        private List<Item> inventories = new List<Item>();

        //constructor 
        public Smuggler() { }

        //constructor
        public Smuggler(string n, List<City> cities) {
	        name = n;
	        
            citiesLi = cities;
	        randomCityAssigner();
            randomNextCityAssigner();

          
        }

        void randomCityAssigner() {
            
            Random ran = new Random();
            int x = ran.Next()%3;// Generate a random number between 0 and 2 (inclusive)

            // Loop until a city is found that is not the current city
            while (currentCity == null || citiesLi[x] != currentCity)
            {
                // Assign a random city to the currentCity
                currentCity = citiesLi[ran.Next() % 3];
            }
            Console.WriteLine("current city name: " + currentCity.getCityName()) ;

        }

        //it will assign random city object to next city object
        void randomNextCityAssigner() {
            
            
            Random ran = new Random();
            int x = ran.Next() % 3;

            do
            {

                nextCity = citiesLi[ran.Next() % 3];

            } while (nextCity == currentCity || nextCity == tempCity);

        }

        //this feature is for showing the inventories on the console.
        public void inventoriesListDisplay() {
            Console.WriteLine("################################################################################");

            Console.WriteLine("##### Item\t\t" + "Buying Price\t\t" + "Selling Price\t\t" + "Quantity");
            for (int i = 0; i < inventories.Count(); i++)
            {
                Console.WriteLine("##### " + (i + 1) + ". " + inventories[i].getName() + "\t\t\t|" + inventories[i].getBuyingPrice()
                    + "|\t\t\t|" + inventories[i].getSellingPrice() + "|\t\t|" + inventories[i].getQuantity() + "|");


            }

            Console.WriteLine("################################################################################");

        }

        //for selling wares
        void sellTheirWares() {
            int ans = 0;
            float quantity = 0;
            bool quantityChecker = false;
            float earnedMoney = 0;


            Console.WriteLine("#############################################################");
            Console.WriteLine("##### Item\t\t" + "Selling Price\t\tQuantity");
            for (int i = 0; i < inventories.Count(); i++)
            {
                Console.WriteLine("##### " +( i + 1) + ". " + inventories[i].getName() + "\t\t\t|" + inventories[i].getSellingPrice() + "|\t\t\t|" + inventories[i].getQuantity() + "|");
            }
            Console.WriteLine("#############################################################");


            bool checker1 = true;
            do
            {
                try
                {
                    checker1 = false;
                    Console.WriteLine("#############################################################");
                    Console.WriteLine("Enter 0 to exit from Selling menu : ");
                    Console.WriteLine("Please enter the item number for selling : ");
                    string input= Console.ReadLine();


                    if (!Int32.TryParse(input, out ans)) throw new Exception();
                    if (ans < 0 || ans > inventories.Count()) throw new CustomException("error2");
                }
                catch (CustomException ce)
                {
                    checker1 = true;

                    Console.WriteLine("#########################");
                    CustomException.processError(CustomException.msg, inventories.Count());
                    Console.WriteLine("#########################");

                }
                catch (Exception e) {

                    checker1 = true;
                    Console.WriteLine("#########################");
                    Console.WriteLine(HardCodedValue.ERROR2);
                    Console.WriteLine("#########################");
                }
                } while (checker1) ;

                if (ans == 0)
                {
                    Console.WriteLine("Thank you for visiting selling menu!");
                    return;
                }


            do
                {



                    bool checker = true;
                    do
                    {
                        try
                        {
                            checker = false;
                            Console.WriteLine("Please enter the quantities of the " + inventories[ans - 1].getName() + " : ");
                            string input = Console.ReadLine();

                            if(!float.TryParse(input, out quantity)) throw new Exception();
                            
                        }

                        catch (Exception e) {

                                checker = true;
                                Console.WriteLine("#########################");
                                Console.WriteLine(HardCodedValue.ERROR3);
                                Console.WriteLine("#########################");
                            }
                            } while (checker) ;

                            quantityChecker = false;
                            if (quantity > inventories[ans - 1].getQuantity())
                            {
                                Console.WriteLine("########################################");
                                Console.WriteLine("Sorry, we don't have that much quantity ");
                                quantityChecker = true;
                            }
                    } while (quantityChecker);

                    earnedMoney = quantity * inventories[ans - 1].getSellingPrice();
                    Console.WriteLine("################################");
                    Console.WriteLine("Earned amount : " + earnedMoney + "");
                    Console.WriteLine("################################");

                    //increasing Smuggler's money after selling the item
                    SmugglerMoney += earnedMoney;

                    //decreasing Smuggler's item quantity after selling it successfully 
                    inventories[ans - 1].setQuantity(inventories[ans - 1].getQuantity() - quantity);
                    Console.WriteLine("going to Smuggler menu");
                    SmugglerMenu();
                }


                public void buyTheirWares() {
                        int ans = 0;
                        float quantity = 0;
                        bool quantityChecker = false;
                        float investedMoney = 0;
                        int j = 0;

                        Console.WriteLine("#############################################################");
                        for (int i = 0; i < citiesLi.Count(); i++)
                        {
                            Console.WriteLine("------------------------------------------------");
                            Console.WriteLine("city name : " + citiesLi[i].getCityName());
                            Console.WriteLine("##### Item \t\t\t" + " Price\t\t\tQuantity");
                            for (j = 0; j < citiesLi[i].getInventories().Count(); j++)
                            {
                                Console.WriteLine("##### " + (j + 1) + ". " + citiesLi[i].getInventories()[j].getName() + "\t\t\t|" + citiesLi[i].getInventories()[j].getSellingPrice() + "|\t\t\t|" + citiesLi[i].getInventories()[j].getQuantity() + "|");

                            }
                            Console.WriteLine("------------------------------------------------");
                        }

                        bool checker1 = true;
                        do
                        {
                            try
                            {
                                checker1 = false;
                                Console.WriteLine("#############################################################");
                                Console.WriteLine("Enter 0 to exit from buying menu : ");

                                Console.WriteLine("Please enter the item number to buy item in " + currentCity.getCityName() + " : ");
                                string input = Console.ReadLine();

                                if(!Int32.TryParse(input, out ans)) throw new Exception();
                                if (ans < 0 || ans > currentCity.getInventories().Count()) throw new CustomException("error2");
                            }

                            catch (CustomException ce)
                            {
                                checker1 = true;

                                Console.WriteLine("#########################");
                                CustomException.processError(CustomException.msg, currentCity.getInventories().Count());
                                Console.WriteLine("#########################");

                            }
                            catch (Exception e ) {

                                checker1 = true;
                                Console.WriteLine("#########################");
                                Console.WriteLine(HardCodedValue.ERROR2);
                                Console.WriteLine("#########################");
                            }
                            } while (checker1) ;


                            if (ans == 0)
                            {
                                Console.WriteLine("Thank you for visiting buying menu!");
                                return;
                            }

                            do
                            {
                                bool checker = true;
                                do
                                {
                                    try
                                    {
                                        checker = false;
                                        Console.WriteLine("Please enter the quantities of the " + currentCity.getInventories()[ans - 1].getName() + " : ");
                                        string input = Console.ReadLine();

                                        if (!float.TryParse(input, out quantity)) throw new Exception();
                                        
                                    }
                                    
                                    catch (Exception e) {

                                        checker = true;
                                        Console.WriteLine("#########################");
                                        Console.WriteLine(HardCodedValue.ERROR3);
                                        Console.WriteLine("#########################");
                                    }
                                    } while (checker) ;

                                    quantityChecker = false;
                                    if (quantity > currentCity.getInventories()[ans - 1].getQuantity())
                                    {
                                        Console.WriteLine("########################################");
                                        Console.WriteLine("Sorry, we don't have that much quantity ");
                                        quantityChecker = true;
                                    }
                                } while (quantityChecker);



                                investedMoney = quantity *  currentCity.getInventories()[ans - 1].getSellingPrice();

                                if (investedMoney > SmugglerMoney)
                                {

                                    Console.WriteLine("###################################");
                                    Console.WriteLine("Sorry! you don't have enough money.");
                                    Console.WriteLine("###################################");


                                }
                                else
                                {


                                    //increasing Smuggler's item quantity after buying it successfully
                                    int itemNumber = -1;


                                    for (int i = 0; i < inventories.Count(); i++)
                                    {
                                        if (inventories[i].getName() == currentCity.getInventories()[ans - 1].getName())
                                        {
                                            itemNumber = i;
                                            break;
                                        }
                                    }


                                    if (itemNumber == -1)
                                    {
                                            
                                        Item it = new Item(currentCity.getInventories()[ans - 1].getName(), currentCity.getInventories()[ans - 1].getSellingPrice(),0 );
                                        //it.setName();
                                        it.setBuyingPrice(currentCity.getInventories()[ans - 1].getSellingPrice());

                                        //it.randomlySetSellingPrice();
                                        it.setQuantity(0);

                                        inventories.Add(it);

                                        itemNumber = inventories.Count() - 1;

                                    }
                                    Item ite = currentCity.getInventories()[ans - 1];
                                    ite.setQuantity(currentCity.getInventories()[ans - 1].getQuantity() - quantity);

                                    inventories[itemNumber].setQuantity(inventories[itemNumber].getQuantity() + quantity);
                                    inventories[itemNumber].setBuyingPrice(currentCity.getInventories()[ans - 1].getSellingPrice());
                                    inventories[itemNumber].randomlySetSellingPrice();
                                    Console.WriteLine("################################");
                                    Console.WriteLine("Total cost : " + investedMoney);
                                    Console.WriteLine("################################");

                                    //decreasing Smuggler's money after buying the item
                                    SmugglerMoney -= investedMoney;


                                    Console.WriteLine("it's finished");

                                }
                                SmugglerMenu();
                            }

            //this method is for checking the user's answer to call respective method.
            public void SmugglerAnsChecker(int a) {
                    switch (a)
                    {
                        case 1:
                            sellTheirWares();
                            break;
                        case 2:
                            buyTheirWares();
                            break;
                        case 3:
                            {
                                tempCity = currentCity;

                                currentCity = nextCity;


                                randomNextCityAssigner();

                                fineChecker();

                                if (gameLose)
                                {
                                    return;
                                }
                                SmugglerMenu();
                                break;

                            }

                    }
                }

            //for printing Smuggler menu
            public void SmugglerMenu() {

                if (caught)
                {
                    Console.WriteLine("#############################");
                    Console.WriteLine("You are caught by the Police!");
                    Console.WriteLine("#############################");
                    caught = false;

                }
                Console.WriteLine("###########################");
                Console.WriteLine("###########################");
                Console.WriteLine("Welcome to Smuggler Page : ");
                Console.WriteLine("###########################");
                Console.WriteLine("Smuggler name : " + name + "");
                Console.WriteLine("Smuggler's total money : " + SmugglerMoney);
                Console.WriteLine("Smuggler's current City name : " + currentCity.getCityName());
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("After reaching 100% " + name + " is going to caught by the Police");
                Console.WriteLine(caughtPercentageCounter + "% chances to be caught by the Police");
                Console.WriteLine("-------------------------------------------------------------------");


                inventoriesListDisplay();



                int ans = 0;
                Console.WriteLine("1. Sell their wares");
                Console.WriteLine("2. Buy their wares");
                Console.WriteLine("3. travel to a new city");
                Console.WriteLine("4. Going back to Main Page");

                bool checker = true;
                do
                {
                    try
                    {
                        checker = false;
                        Console.WriteLine("Please enter the require option : ");
                        string input = Console.ReadLine();
                        if (!Int32.TryParse(input, out ans)) throw new Exception();
                        if (ans < 1 || ans > 4) throw new CustomException("error1");
                    }
                    catch (CustomException ce)
                    {
                        checker = true;

                        Console.WriteLine("#########################");
                        CustomException.processError(CustomException.msg, 4);
                        Console.WriteLine("#########################");

                    }
                    catch (Exception e) {

                        checker = true;
                        Console.WriteLine("#########################");
                        Console.WriteLine(HardCodedValue.ERROR2);
                        Console.WriteLine("#########################");
                    }
                    } while (checker) ;

                    if (ans == 4) return;
                    SmugglerAnsChecker(ans);


            }

//for randomly assigning the city to Smuggler



        public string getName() {
                return name;
        }
        public void setName(string n) {
                name = n;
            }

        public List<Item> getInventories() {
                return inventories;
        }

        public void setInventories(List < Item > inv) {
                inventories = inv;
        }


        public float itemQuantitiesChecker() {
                float totalQuantities = 0;
                for (int i = 0; i < inventories.Count(); i++)
                {
                    totalQuantities += inventories[i].getQuantity();
                }
                return totalQuantities;
            }
        public void fineChecker() {

                float totalQuantity = itemQuantitiesChecker();
                if (totalQuantity >= 8000)
                {
                    caughtPercentageCounter += 40;
                    Console.WriteLine(caughtPercentageCounter + "percentage of the caught ");
                }
                else if (totalQuantity >= 5000)
                {
                    caughtPercentageCounter += 30;
                    Console.WriteLine(caughtPercentageCounter + "percentage of the caught ");

                }
                else if (totalQuantity >= 2000)
                {
                    caughtPercentageCounter += 20;
                    Console.WriteLine(caughtPercentageCounter + "percentage of the caught ");

                }
                else if (totalQuantity >= 500)
                {
                    caughtPercentageCounter += 10;
                    Console.WriteLine(caughtPercentageCounter + "percentage of the caught ");

                }

                if (caughtPercentageCounter >= 100)
                {
                    caught = true;
                    caughtPercentageCounter = 0;
                    tempCity = currentCity;
                    currentCity = nextCity;
                    randomNextCityAssigner();

                    //minimum one item require

                    if (inventories.Count() != 0)
                    {
                        inventories.RemoveAt(inventories.Count() -1 );

                    }

                    SmugglerMoney -= fine;
                    fine += 50;
                    if (SmugglerMoney <= 0)
                    {
                        gameLose = true;
                        Console.WriteLine("ohh You lose the game!");

                    }
                    Console.WriteLine("Smuggler Money is : " + SmugglerMoney);

                }       
            }

        public bool getGameLose()
        {
            return gameLose;
        }

        public void setGameLose(bool status)
        {
            gameLose = status;
        }

    }
}
