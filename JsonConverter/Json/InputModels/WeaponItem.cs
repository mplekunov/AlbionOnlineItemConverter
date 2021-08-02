using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JsonConverter.Json.InputModels
{
    public class WeaponItem : Item
    {
        [JsonProperty("@maxqualitylevel")]
        public string maxQualityLevel { get; set; }

        [JsonProperty("@itempower")]
        public string itemPower { get; set; }

        public override void showItemInfo(string fileLocation)
        {
            base.showItemInfo(fileLocation);

            using (StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine("Max Quality Level: " + this.maxQualityLevel);
                sw.WriteLine("Item Power: " + this.itemPower);
                sw.WriteLine('\n');
            }
        }
    }
}