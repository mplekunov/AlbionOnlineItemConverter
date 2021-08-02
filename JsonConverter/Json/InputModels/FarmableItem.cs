using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace JsonConverter.Json.InputModels
{
    public class FarmableItem : Item
    {
        [JsonProperty("@kind")]
        public string kind { get; set; }

        private class HarvestJson
        {
            [JsonProperty("@growtime")]
            public string growTime { get; set; }

            [JsonProperty("@lootlist")]
            public string uniqueName { get; set; }

            [JsonProperty("@fame")]
            public string fame { get; set; }

            public class HarvestedItemJson
            {
                [JsonProperty("@chance")]
                public string successRate { get; set; }

                [JsonProperty("@amount")]
                public string amount { get; set; }
            }

            public class GrownItemJson : HarvestedItemJson { }

            [JsonProperty("seed")]
            public HarvestedItemJson harvestedItem = new HarvestedItemJson(); //changed

            [JsonProperty("offspring")]
            public GrownItemJson grownItem = new GrownItemJson(); //changed
        }

        private class GrowJson : HarvestJson { }

        [JsonProperty("harvest")]
        private HarvestJson harvest = new HarvestJson(); //changed

        [JsonProperty("grownitem")]
        private GrowJson grow = new GrowJson(); //changed

        public string growTime => harvest.uniqueName == null ? grow.growTime : harvest.growTime;
        public string loot => harvest.uniqueName == null ? grow.uniqueName : harvest.uniqueName;
        public string fame => harvest.uniqueName == null ? grow.fame : harvest.fame;
        public string successRate => harvest.uniqueName == null ? grow.grownItem.successRate : harvest.harvestedItem.successRate;
        public string amount => harvest.uniqueName == null ? grow.grownItem.amount : harvest.harvestedItem.amount;

        public override void showItemInfo(string fileLocation)
        {
            base.showItemInfo(fileLocation);

            using (StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine("Kind: " + this.kind);
                sw.WriteLine("Grow Time: " + this.growTime);
                sw.WriteLine("Loot: " + this.loot);
                sw.WriteLine("Fame: " + this.fame);
                sw.WriteLine("Success Rate: " + this.successRate);
                sw.WriteLine("Amount: " + this.amount);
                sw.WriteLine('\n');
            }
        }
    }
}
