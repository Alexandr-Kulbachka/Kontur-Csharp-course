using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kontur.GameStats.Server
{
    public class GameServer
    {
        public Newtonsoft.Json.Linq.JObject Info { get; set; }
        public Dictionary<string, Newtonsoft.Json.Linq.JObject> Matches;

        public GameServer() 
        {
            Matches = new Dictionary<string, Newtonsoft.Json.Linq.JObject>();
        }
    }
}
