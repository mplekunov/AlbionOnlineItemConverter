using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace JsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://raw.githubusercontent.com/broderickhyman/ao-bin-dumps/master/items.json");

            WebResponse response = request.GetResponse();
            Console.WriteLine("Status: ");
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            string stringToParse;

            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);

                stringToParse = reader.ReadToEnd();
            }

            string executablePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string directoryPath = Path.GetDirectoryName(executablePath);

            new Thread(() =>
            {
                List<Json.InputModels.FarmableItem> farmableItems =
                    Json.Search.JsonSearch.getItemList<Json.InputModels.FarmableItem>(stringToParse, "farmableitem");

                File.Delete(Path.Combine(directoryPath, "farmables.txt"));
                farmableItems.ForEach(it => it.showItemInfo(Path.Combine(directoryPath, "farmables.txt")));
                Console.WriteLine("Farmable Items Saved");
            }).Start();

            new Thread(() =>
            {
                List<Json.InputModels.ConsumableItem> consumableItems =
                    Json.Search.JsonSearch.getItemList<Json.InputModels.ConsumableItem>(stringToParse, "consumableitem");

                File.Delete(Path.Combine(directoryPath, "consumables.txt"));
                consumableItems.ForEach(it => it.showItemInfo(Path.Combine(directoryPath, "consumables.txt")));
                Console.WriteLine("Consumable Items Saved");
            }).Start();

            new Thread(() =>
            {
                List<Json.InputModels.FarmableItem> farmableItems =
                    Json.Search.JsonSearch.getItemList<Json.InputModels.FarmableItem>(stringToParse, "consumablefrominventoryitem");

                File.Delete(Path.Combine(directoryPath, "consumablefrominventory.txt"));
                farmableItems.ForEach(it => it.showItemInfo(Path.Combine(directoryPath, "consumablefrominventory.txt")));
                Console.WriteLine("Consumable From Inventory Items Saved");
            }).Start();

            new Thread(() =>
            {
                List<Json.InputModels.FarmableItem> farmableItems =
                    Json.Search.JsonSearch.getItemList<Json.InputModels.FarmableItem>(stringToParse, "equipmentitem");

                File.Delete(Path.Combine(directoryPath, "equipment.txt"));
                farmableItems.ForEach(it => it.showItemInfo(Path.Combine(directoryPath, "equipment.txt")));
                Console.WriteLine("Equipment Items Saved");
            }).Start();

            new Thread(() =>
            {
                List<Json.InputModels.FarmableItem> farmableItems =
                    Json.Search.JsonSearch.getItemList<Json.InputModels.FarmableItem>(stringToParse, "simpleitem");

                File.Delete(Path.Combine(directoryPath, "items.txt"));
                farmableItems.ForEach(it => it.showItemInfo(Path.Combine(directoryPath, "items.txt")));
                Console.WriteLine("Items Items Saved");
            }).Start();

            new Thread(() =>
            {
                List<Json.InputModels.FarmableItem> farmableItems =
                    Json.Search.JsonSearch.getItemList<Json.InputModels.FarmableItem>(stringToParse, "weapon");

                File.Delete(Path.Combine(directoryPath, "weapons.txt"));
                farmableItems.ForEach(it => it.showItemInfo(Path.Combine(directoryPath, "weapons.txt")));
                Console.WriteLine("Weapons Items Saved");
            }).Start();
        }
    }
}
