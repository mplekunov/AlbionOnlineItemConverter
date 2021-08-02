using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace JsonConverter.Json.OutputModels
{
    public class Item
    {
        public string uniqueName { get; set; } //Item Name
        public int tier { get; set; } //Item Tier from 1 to 8
        public int power { get; set; } //Item Power
        public double weight { get; set; } //Item Weight
        public string  mainCategory { get; set; } //Main Item Category under which Item is located in the in-game Market
        public string subCategory { get; set; } //Sub Item Category (if existed) under which Item is located in the in-game Market
        public int quantity { get; set; }//Item Quantity
        public int enchantmentLevel { get; set; }//Item Enchantment level

        public List<Recepie> recepies = new List<Recepie>(); //List of all crafting recepies that can be used for crafting the item
        public List<Item> enchantments = new List<Item>(); //List of all Enchantments that can be applied on the item 

        public virtual void showItemInfo(string fileLocation)
        {
            using (System.IO.StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine("\nUnique Name: " + this.uniqueName);
                sw.WriteLine("Tier: " + this.tier);
                sw.WriteLine("Weight: " + this.weight);
                sw.WriteLine("Cateory: " + this.mainCategory);
                sw.WriteLine("Sub Category: " + this.subCategory);

                if (this.recepies.Count > 0) {
                    sw.WriteLine("Crafting Requirements\n");

                    foreach (var recepie in this.recepies)
                    {
                        sw.WriteLine("Recepie: ");

                        foreach (var item in recepie.items)
                        {
                            sw.WriteLine("Quantity: " + item.quantity);

                            sw.WriteLine("Unique Name: " + item.uniqueName);
                        }

                        sw.WriteLine('\n');
                    }
                }

                if (this.enchantments.Count > 0)
                {
                    sw.WriteLine("Echantments: ");

                    foreach (var enchantment in this.enchantments)
                    {
                        sw.WriteLine("Enchantment Level: " + enchantment.enchantmentLevel);
                        sw.WriteLine("Item Power: " + enchantment.power);

                        if (enchantment.recepies.Count > 0)
                        {
                            sw.WriteLine("Crafting Requirements\n");

                            foreach (var recepie in enchantment.recepies)
                            {
                                sw.WriteLine("Recepie:");

                                foreach (var item in recepie.items)
                                {
                                    sw.WriteLine("Quantity: " + item.quantity);

                                    sw.WriteLine("Unique Name: " + item.uniqueName);
                                }

                                sw.WriteLine('\n');

                            }
                        }
                    }
                }
            }
        }

    }
}
