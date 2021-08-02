using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JsonConverter.Json.InputModels
{
    public class ConsumableItem : Item
    {
        [JsonProperty("@consumespell")]
        public string spellName { get; set; }

        [JsonProperty("@dummyitempower")]
        public string itemPower { get; set; }
    
        [JsonProperty("@nutrition")]
        public string nutrition { get; set; }

        public override void showItemInfo(string fileLocation)
        {
            base.showItemInfo(fileLocation);

            using (StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine("Spell Name: " + this.spellName);
                sw.WriteLine("Item Power: " + this.itemPower);
                sw.WriteLine("Nutrition: " + this.nutrition);
                sw.WriteLine('\n');
            }
        }

    }
}
