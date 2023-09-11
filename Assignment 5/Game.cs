using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_5;
using Microsoft.Win32;

namespace Assignment_5
{
    //main ex
    internal class Game
    {
        static void Main(string[] args)
        {
            //list of cities
            Display d1 = new Display();

            //Montreal's Items
            Item im1 = new Item("Lsd", 10, 20000);
            Item im2 = new Item("Weed", 19, 40000);
            Item im3 = new Item("Heroin", 30, 50000);

            List<Item> l1 = new List<Item>() ;
            l1.Add(im1);
            l1.Add(im2);
            l1.Add(im3);

            //Toronto's item
            Item it1 = new Item("Lsd", 12, 20000);
            Item it2 = new Item("Weed", 17, 30000);
            Item it3 = new Item("Heroin", 28, 50000);
            Item it4 = new Item("Cocaine", 10, 20000);

            List<Item> l2  = new List<Item>();
            l2.Add(it1);
            l2.Add(it2);
            l2.Add(it3);
            l2.Add(it4);
            
            //Vancouver's item
            Item iv1 = new Item("Lsd", 15, 20000);
            Item iv2 = new Item("Weed", 14, 30000);
            Item iv3 = new Item("Heroin", 33, 50000);
            Item iv4 = new Item("Cocaine", 12, 20000);
            Item iv5 = new Item("Ketamine", 20, 20000);

            List<Item> l3 = new List<Item>();
            l3.Add(iv1);
            l3.Add(iv2);
            l3.Add(iv3);
            l3.Add(iv4);
            l3.Add(iv5);

            d1.getCities().Add(new City("Montreal", l1));
            d1.getCities().Add(new City("Toronto", l2));
            d1.getCities().Add(new City("Vancouver", l3));
            
            
            d1.menu();

        }

    }

}


