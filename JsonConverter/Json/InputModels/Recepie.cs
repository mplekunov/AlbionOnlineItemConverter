using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonConverter.Json.InputModels
{
    public class Recepie
    {   
        public class RecepieItem
        {
            [JsonProperty("@uniquename")]
            public string uniqueName { get; set; }

            [JsonProperty("@count")]
            public string amount { get; set; }
        }

        [JsonProperty("craftresource")]
        [JsonConverter(typeof(Json.Converter.SingleOrArrayConverter<RecepieItem>))]
        public List<RecepieItem> craftResources = new List<RecepieItem>();//
    }
}
