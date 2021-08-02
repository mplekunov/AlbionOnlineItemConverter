using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JsonConverter.Json.InputModels
{
    public class Enchantments
    {
        public class Enchantment
        {
            [JsonProperty("@enchantmentlevel")]
            public string enchantmentLevel { get; set; }

            [JsonProperty("@itempower")]
            public string itemPower { get; set; }

            [JsonProperty("@dummyitempower")]
            private string _itemPower { set { itemPower = value; } }

            [JsonProperty("craftingrequirements")]
            [JsonConverter(typeof(Json.Converter.SingleOrArrayConverter<Recepie>))]
            public List<Recepie> itemRecepies = new List<Recepie>();
        }

        [JsonProperty("enchantment")]
        [JsonConverter(typeof(Json.Converter.SingleOrArrayConverter<Enchantment>))]
        public List<Enchantment> enchantments = new List<Enchantment>();
    }
}
