using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JsonConverter.Json.InputModels
{
    public class Item
    {
        [JsonProperty("@uniquename")]
        public string uniqueName { get; set; }

        [JsonProperty("@tier")]
        public string tier { get; set; }

        [JsonProperty("@weight")]
        public string weight { get; set; }

        [JsonProperty("@shopcategory")]
        public string mainCategory { get; set; }

        [JsonProperty("@shopsubcategory1")]
        public string subCategory { get; set; }

        [JsonProperty("enchantments")]
        public Enchantments itemEnchantments = new Enchantments(); //changed

        [JsonProperty("craftingrequirements")]
        [JsonConverter(typeof(Json.Converter.SingleOrArrayConverter<Recepie>))]
        public List<Recepie> itemRecepies = new List<Recepie>(); //changed

        public virtual void showItemInfo(string fileLocation)
        {
            using (StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine("Unique Name: " + this.uniqueName);
                sw.WriteLine("Tier: " + this.tier);
                sw.WriteLine("Weight: " + this.weight);
                sw.WriteLine("Cateory: " + this.mainCategory);
                sw.WriteLine("Sub Category: " + this.subCategory);

                if (this.itemRecepies.Count != 0)
                {
                    sw.WriteLine("Crafting Requirements: ");

                    foreach (var craftReq in this.itemRecepies)
                    {
                        if (craftReq.craftResources.Count != 0)
                        {
                            foreach (var craftResource in craftReq.craftResources)
                            {
                                sw.WriteLine("Resource Name: " + craftResource.uniqueName);
                                sw.WriteLine("Amount: " + craftResource.amount);
                            }
                        }
                    }
                }

                if (this.itemEnchantments.enchantments.Count != 0)
                {
                    sw.WriteLine("Echantments: ");

                    foreach (var enchantment in this.itemEnchantments.enchantments)
                    {
                        sw.WriteLine("Enchantment Level: " + enchantment.enchantmentLevel);
                        sw.WriteLine("Item Power: " + enchantment.itemPower);

                        foreach (var craftResource in enchantment.itemRecepies)
                        {
                            if (craftResource.craftResources.Count != 0)
                            {
                                foreach (var ase in craftResource.craftResources)
                                {
                                    sw.WriteLine("Resource Name: " + ase.uniqueName);
                                    sw.WriteLine("Amount: " + ase.amount);
                                }
                            }
                        }
                    }
                }
                //sw.WriteLine('\n');
            }
        }
    }
}
