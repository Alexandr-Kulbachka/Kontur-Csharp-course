using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.GameStats.Server
{
    public static class SaveModule
    {
        public static void UploadDataFromSave(ref Dictionary<string, GameServer> dataStorage, string wayToSave)
        {
            if (File.Exists(wayToSave))
            {
                using (var streamReader = new StreamReader(wayToSave))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    dataStorage = jsonSerializer.Deserialize<Dictionary<string, GameServer>>(jsonReader);
                }
            }
            else
                dataStorage = new Dictionary<string, GameServer>();
        }

        public static void DownloadDataToSave(Dictionary<string, GameServer> dataStorage, string wayToSave)
        {
            using (var file = File.CreateText(wayToSave))
            {
                var serializer = new JsonSerializer();
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(file, dataStorage);
            }
            //var jsonData = JsonConvert.SerializeObject(dataStorage, Formatting.Indented);
            //File.WriteAllText(wayToSave, jsonData);

        }
    }
}

//string filepath = "../../json1.json";
//string result = string.Empty;
//            using (StreamReader r = new StreamReader(filepath))
//            {
//                var json = r.ReadToEnd();
//var jobj = JObject.Parse(json);       
//                foreach (var item in jobj.Properties()) {
//                    item.Value = item.Value.ToString().Replace("v1", "v2");                   
//                }
//                result = jobj.ToString();
//                Console.WriteLine(result);              
//            }
//            File.WriteAllText(filepath, result);
//        }
