using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonConverter.Json.Search
{
    public static class JsonSearch
    {
        private static JObject GetParsedObject(string responseFromServer) => JObject.Parse(responseFromServer);

        public static List<T> getItemList<T>(string stringToParse, string itemType)
        {
            List<T> itemList = new List<T>();

            //JsonModels.RootJson test = JsonConvert.DeserializeObject<JsonModels.RootJson>(stringToParse);

            List<JToken> search = GetParsedObject(stringToParse)["items"][itemType].Children().ToList();

            foreach (JToken result in search)
                itemList.Add(result.ToObject<T>());

            return itemList;
        }
    }
}
