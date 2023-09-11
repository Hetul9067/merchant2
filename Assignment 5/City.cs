using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    internal class City
    {
        private string Name  { get; set; }

        //aggregate relation
        private List<Item> inventories { get; set; }

        private List<string> inventoriesReferenceLi { get; set; }


        
         //constructor
        public City ( string name, List<Item>items)
        {
            Name = name;
            inventories = items;         
            inventoriesReferenceLi = new List<string> ();
            inventoriesReferenceLi.Add("Lsd");
            inventoriesReferenceLi.Add("Weed");
            inventoriesReferenceLi.Add("Heroin");
            inventoriesReferenceLi.Add("Cocaine");
            inventoriesReferenceLi.Add("Ketamine");

        }

        //for selling wares
        public void sellsItem()
        {
            int ans = 0;
            float quantity = 0;
            bool quantityChecker = false;


            Console.WriteLine("#############################################################");
            Console.WriteLine("#### Item\t\tSelling Price\t\tQuantity");
            
            
            for (int i = 0; i < inventories.Count(); i++)
            {
                Console.WriteLine("####" + (i + 1) + ". " + inventories[i].getName()+"\t\t\t"+inventories[i].getSellingPrice()+"\t\t\t" + inventories[i].getQuantity());
            }

            bool checker1 = true;
            do
            {
                try
                {
                    checker1 = false;
                    Console.WriteLine("#############################################################");
                    Console.WriteLine("Please enter the item number for selling : ");
                    string input = Console.ReadLine();


                    if (!Int32.TryParse(input, out ans)) throw new Exception();
                    if (ans < 1 || ans > inventories.Count()) throw new CustomException("error1");
                }
                catch (CustomException ce)
                {
                    checker1 = true;

                    Console.WriteLine("#########################");
                    CustomException.processError(CustomException.msg, inventories.Count());
                    Console.WriteLine("#########################");

                }
                catch (Exception e)
                {

                    checker1 = true;
                    Console.WriteLine("#########################");
                    Console.WriteLine(HardCodedValue.ERROR2);
                    Console.WriteLine("#########################");
                }
                } while (checker1) ;



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

                        if (!float.TryParse(input, out quantity)) throw new Exception();

                        }

                        catch (Exception e)
                        {

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


                    Console.WriteLine("################################");
                    Console.WriteLine("####       Item sold        ####");
                    Console.WriteLine("################################");
                    



                    //decreasing city's item quantity after selling it successfully 
                    inventories[ans - 1].setQuantity(inventories[ans - 1].getQuantity() - quantity);
                    Console.WriteLine("going to city menu");
                    
                    cityMenu();
        }



        public void buyTheirWares()
        {
            int ans = 0;
            float quantity = 0;
            bool quantityChecker = false;

            Console.WriteLine("#############################################################");
            Console.WriteLine("------------------------------------------------");
            
            for (int i = 0; i < inventoriesReferenceLi.Count(); i++)
            {
                Console.WriteLine("##### " + (i + 1)+ ". " + inventoriesReferenceLi[i] + ".");
             
            }

            Console.WriteLine ( "------------------------------------------------");
            Console.WriteLine ( "#############################################################" );



            bool checker1 = true;
            do
            {
                try
                {
                    checker1 = false;
                    Console.WriteLine ( "#############################################################" );
                    Console.WriteLine ( "Enter 0 to exit from buying menu : ");

                    Console.WriteLine("Please enter the item number to buy item for " + Name +" : " );
                    string input = Console.ReadLine();

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
                catch (Exception e)
                {

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

                bool checker = true;
                do
                {
                    try
                    {
                        checker = false;
                        Console.WriteLine("Please enter the quantities of the " + inventoriesReferenceLi[ans - 1] + " : ");
                       string input = Console.ReadLine();

                        if (!float.TryParse(input, out quantity)) throw new Exception();
                        
                    }
                    
                    catch (Exception e)
                    {

                        checker = true;
                        Console.WriteLine("#########################");
                        Console.WriteLine(HardCodedValue.ERROR3);
                        Console.WriteLine("#########################");
                    }
                } while (checker) ;



                    Console.WriteLine("#################################");
                    Console.WriteLine("Item has been bought successfully!");
                    Console.WriteLine("#################################");

                    //increasing city's item quantity after buying it successfully
                    int itemNumber = -1;
                    for (int i = 0; i < inventories.Count(); i++)
                    {
                        if (inventories[i].getName() == inventoriesReferenceLi[ans - 1])
                        {
                            itemNumber = i;
                            break;
                        }
                    }
                    if (itemNumber == -1)
                    {
                        Random ran = new Random();

                        int x = ran.Next(10, 41);
                        float bPrice = x;

                        Item it = new Item(inventoriesReferenceLi[ans - 1], bPrice, 0);
                        inventories.Add(it);
                        itemNumber = inventories.Count() - 1;

                    }

                    inventories[itemNumber].setQuantity(inventories[itemNumber].getQuantity() + quantity);
                    Console.WriteLine ("it's finished");

                    cityMenu();
        }




        public void cityAnsChecker(int a)
        {
            switch (a)
            {
                case 1:
                    sellsItem();
                    break;
                case 2:
                    buyTheirWares();
                    break;

            }
        }

        //for showing the functionality of the city menu
        public void cityMenu()
        {
            Console.WriteLine("###############################################################");
            Console.WriteLine("welcome to " + Name + " : ");

                 
            Console.WriteLine("#####| Item\t\t|"+ "Buying Price|\t\t|"+ "Selling Price|\t\t|"+ "Quantity|");
            for (int i = 0; i < inventories.Count; i++)
            {
                Console.WriteLine("#####| " + inventories[i].getName() + "\t\t\t|" + inventories[i].getBuyingPrice()
                    +"|\t\t\t|"+ inventories[i].getSellingPrice() +"\t\t\t|" + inventories[i].getQuantity() + "|");


            }
            Console.WriteLine( "###############################################################");

            int ans = 0;
            Console.WriteLine("1. Sell city's wares");
            Console.WriteLine("2. Buy city's wares");
            Console.WriteLine("3. Going back to Main Page");

            


            bool checker = true;
            do
            {
                try
                {
                    checker = false;
                    Console.WriteLine("Please enter the require option: ");
                    string input = Console.ReadLine();
                    if (!Int32.TryParse(input, out ans)) throw new Exception();
                    if (ans < 1 || ans > 3) throw new CustomException("error1");
                }
                catch (CustomException ce)
                {
                    checker = true;

                    Console.WriteLine("#########################");
                    CustomException.processError(CustomException.msg, 3);
                    Console.WriteLine("#########################");

                }
                catch (Exception e)
                {

                    checker = true;
                    Console.WriteLine("#########################");
                    Console.WriteLine(HardCodedValue.ERROR2);
                    Console.WriteLine("#########################");
                }
            } while (checker) ;


             if (ans == 3) return;
                cityAnsChecker(ans);



        }


        public string getCityName()
        {
            return Name;
        }

        public void setCityName(string name)
        {
            Name = name;
        }


        public List<Item> getInventories()
        {
            return inventories;
        }

        public void setInventroies(List<Item> inv)
        {

            inventories = inv;
        }

    }
}
