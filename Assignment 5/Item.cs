using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_5
{
    public class Item
    {
        private string Name { get; set; }
        
        private float SellingPrice { get; set;}
        private float BuyingPrice { get; set;}   
        private float Quantity { get; set;}

        private int randomValue = 0;
        private int randomValue1 = 0;

        public Item() { }
       public  Item(string name, float price, float quantity ) 
       {
            Name = name;
            randomlySetBuyingPrice(price);
            randomlySetSellingPrice();
           /* SellingPrice = sellingPrice;
            BuyingPrice = buyingPrice;*/
            Quantity = quantity; 

       }

        public void randomlySetBuyingPrice(float price)
        {
            float maxPrice = price * 1.50f;




            if (randomValue == 1) randomValue = 0;
            else randomValue = 1;
            
            Random rand = new Random((int)DateTime.Now.Ticks);
            float range = maxPrice - price;

            BuyingPrice = (float)Math.Round((float)(rand.NextDouble() * range + price), 2);

        }

        public void randomlySetSellingPrice()
        {
            float maxPrice = this.BuyingPrice * 1.50f;

            
            Random rand = new Random((int)DateTime.Now.Ticks);
            float range = maxPrice - BuyingPrice;
            SellingPrice = (float)Math.Round((float)(rand.NextDouble() * range + BuyingPrice), 2);




        }

        public void setName(string name)
        {
            this.Name = name;
        }

        public string getName()
        {
            return this.Name;
        }

        public float getBuyingPrice()
        {
            return this.BuyingPrice;
        }

        public void setBuyingPrice(float bPrice)
        {
            this.BuyingPrice = bPrice;
        }

        public void setSellingPrice(float sPrice) {
            this.SellingPrice = sPrice;
        }
        public float getSellingPrice()
        {
            return this.SellingPrice;
        }
        
        public float getQuantity()
        {
            return this.Quantity;
        }

        public void setQuantity(float quantity)
        {
            this.Quantity = quantity;
        }

    }
}


