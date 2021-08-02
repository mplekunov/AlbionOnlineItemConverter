using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using MongoDB;
using MongoDB.Bson;

namespace JsonConverter.Json.Converter
{
    public class JsonInToJsonOut
    {
        public static string SerializeObject(Object obj) {

            JsonSerializer jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;


            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jsonSerializer.Serialize(jw, obj);
                return sb.ToString();
            }
        }

        public List<OutputModels.Item> GetItems(List<InputModels.Item> inputItems)
        {
            List<OutputModels.Item> outputItems = new List<OutputModels.Item>();

            inputItems.ForEach(inItem =>
            {
                OutputModels.Item outputItem = new OutputModels.Item(); //temporary item that holds information about input item

                outputItem.uniqueName = inItem.uniqueName;
                
                if (inItem.tier != null)
                    outputItem.tier = Int32.Parse(inItem.tier);
                else
                    outputItem.tier = 0;

                if (inItem.weight != null)
                    outputItem.weight = Double.Parse(inItem.weight);
                else
                    outputItem.weight = 0;

                outputItem.mainCategory = inItem.mainCategory;
                outputItem.subCategory = inItem.subCategory;

                /*
                    The source json file has information about crafting in objects that depending on the item can either be an instance of a class or a collection of instances of a class
                    There is no way to track which Item will be an instance or a collection of instances at the point of conversion from json to Classes in C#
                    Therefore, the only solution is to check each object on whether it's instance or a collection.
                 */
                if (IsList((object)inItem.itemRecepies)) //checks if an object is a list
                {
                    foreach (var recepie in inItem.itemRecepies)
                        outputItem.recepies.Add(GetCraftRecepie(recepie));
                }
                else
                {
                    InputModels.Recepie craftingModel = (InputModels.Recepie)((object)inItem.itemRecepies);

                    if (craftingModel != null)
                        outputItem.recepies.Add(GetCraftRecepie(craftingModel));
                }

                if (inItem.itemEnchantments != null)
                    outputItem.enchantments.AddRange(GetEnchantments(inItem.itemEnchantments));

                outputItems.Add(outputItem);
            });

            return outputItems;
        }
        
        private List<OutputModels.Item> GetEnchantments(InputModels.Enchantments enchantments)
        {
            List<OutputModels.Item> items = new List<OutputModels.Item>();

            OutputModels.Item item = new OutputModels.Item();

            foreach(var enchantment in enchantments.enchantments)
            {
                item.enchantmentLevel = Int32.Parse(enchantment.enchantmentLevel);
                item.power = Int32.Parse(enchantment.itemPower);

                if (IsList((object)enchantment.itemRecepies)) //checks if an object is a list
                {
                    foreach (var recepie in enchantment.itemRecepies)
                        item.recepies.Add(GetCraftRecepie(recepie));
                }
                else
                {
                    InputModels.Recepie craftingModel = (InputModels.Recepie)((object)enchantment.itemRecepies);

                    if (craftingModel != null)
                        item.recepies.Add(GetCraftRecepie(craftingModel));
                }

                items.Add(item);
            }

            return items;
        }

        private OutputModels.Recepie GetCraftRecepie(InputModels.Recepie craftingModel)
        {
            OutputModels.Recepie recepie = new OutputModels.Recepie();

            if (IsList((object)craftingModel.craftResources))
            {
                for (var i = 0; i < craftingModel.craftResources.Count; i++)
                {
                    OutputModels.Item item = new OutputModels.Item();

                    item.uniqueName = craftingModel.craftResources[i].uniqueName;

                    item.quantity = Int32.Parse(craftingModel.craftResources[i].amount);

                    recepie.items.Add(item);
                }
            }
            else
            {
                object craftResource = (object)craftingModel.craftResources;

                InputModels.Recepie.RecepieItem resource = (InputModels.Recepie.RecepieItem)craftResource;

                OutputModels.Item item = new OutputModels.Item();

                item.uniqueName = resource.uniqueName;
                item.quantity = Int32.Parse(resource.amount);

                recepie.items.Add(item);
            }

            return recepie;
        }

        private bool IsList(Object obj)
        {
            if (obj is IList &&
                obj.GetType().IsGenericType &&
                obj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>)))
                return true;
            else
                return false;
        }
    }
}
