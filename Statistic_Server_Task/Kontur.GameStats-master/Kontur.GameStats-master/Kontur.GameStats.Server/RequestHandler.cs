using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kontur.GameStats.Server
{
    class RequestHandler
    {
        private Dictionary<string, GameServer> _dataStorage;
        private Dictionary<string, Dictionary<string, Action<HttpListenerContext, string[]>>> MethodsStorage;
        public RequestHandler()
        {
            SaveModule.UploadDataFromSave(ref _dataStorage, "saveStorage//save.json");
            MethodsStorage = new Dictionary<string, Dictionary<string, Action<HttpListenerContext, string[]>>>
            {
                ["GET"] = new Dictionary<string, Action<HttpListenerContext, string[]>>(),
                ["PUT"] = new Dictionary<string, Action<HttpListenerContext, string[]>>()
            };
            MethodsStorage["GET"]["servers"] = ProcessGetServers;
            MethodsStorage["PUT"]["servers"] = ProcessPutServers;
            MethodsStorage["GET"]["players"] = ProcessGetPlayers;
            MethodsStorage["PUT"]["players"] = ProcessPutPlayers;
            //SaveModule.UploadDataFromSave(ref DataStorage, "saveStorage//save.json");
        }

        public void ExecuteRequestProcessing(HttpListenerContext listenerContext, string[] incomingRequest)
        {
            MethodsStorage
                   [listenerContext.Request.HttpMethod]
                   [incomingRequest[0]]
                   (listenerContext, incomingRequest);
        }

        private void ProcessGetServers(HttpListenerContext listenerContext, string[] incomingRequest)
        {
            var allInfo = new List<Newtonsoft.Json.Linq.JObject>();
            if (incomingRequest.Contains("matches"))
            {
                if (_dataStorage[incomingRequest[1]].Matches.ContainsKey(incomingRequest[3]))
                    allInfo.Add(_dataStorage[incomingRequest[1]].Matches[incomingRequest[3]]);
                else
                    listenerContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            if (incomingRequest[1] == "info")
                allInfo = _dataStorage
                            .Values
                            .Select(v => v.Info)
                             .ToList();
            else
                if (_dataStorage.ContainsKey(incomingRequest[1]))
                allInfo.Add(_dataStorage[incomingRequest[1]].Info);
            else
                listenerContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            if (allInfo.Count > 0)
            {
                var allInfoInJson = allInfo.Count == 1 ?
                        JsonConvert.SerializeObject(allInfo[0], Formatting.Indented)
                        : JsonConvert.SerializeObject(allInfo, Formatting.Indented);
                using (var writer = new StreamWriter(listenerContext.Response.OutputStream))
                    writer.WriteLine(allInfoInJson);
            }
            else
                listenerContext.Response.Close();
        }

        private void ProcessPutServers(HttpListenerContext listenerContext, string[] incomingRequest)
        {
            var requestBodyInJson = Newtonsoft.Json.Linq.JObject.Parse(new StreamReader(
                    listenerContext.Request.InputStream,
                    listenerContext.Request.ContentEncoding).ReadToEnd());
            if (!_dataStorage.ContainsKey(incomingRequest[1]))
                _dataStorage[incomingRequest[1]] = new GameServer();

            if (incomingRequest[2] == "info")
                _dataStorage[incomingRequest[1]].Info = requestBodyInJson;
            else
            {
                if (!_dataStorage[incomingRequest[1]].Matches.ContainsKey(incomingRequest[3]))
                    _dataStorage[incomingRequest[1]].Matches = new Dictionary<string, Newtonsoft.Json.Linq.JObject>();
                _dataStorage[incomingRequest[1]].Matches[incomingRequest[3]] = requestBodyInJson;
            }
            listenerContext.Response.Close();
            SaveModule.DownloadDataToSave(_dataStorage, "saveStorage//save.json");
        }

        private void ProcessGetPlayers(HttpListenerContext listenerContext, string[] incomingRequest)
        {
            using (var writer = new StreamWriter(listenerContext.Response.OutputStream))
            {
                //writer.WriteLine("Hello, " + listenerContext.Request.RawUrl.Trim('/') + "! 2");
                //writer.WriteLine(json);
                var allData = File.ReadAllText("C://Users//Alexandr Kulbachka//Desktop//jsonStarage//2.json");
                writer.WriteLine(allData);
            }
        }

        private void ProcessPutPlayers(HttpListenerContext listenerContext, string[] incomingRequest)
        {

            string json = String.Empty;
            string json2 = String.Empty;

            if (listenerContext.Request.HasEntityBody)
            {
                System.IO.Stream body = listenerContext.Request.InputStream;
                System.Text.Encoding encoding = listenerContext.Request.ContentEncoding;
                System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
                json = reader.ReadToEnd();
                // Message message1 = JsonConvert.DeserializeObject<Message>(json);
                //json2 = JsonConvert.SerializeObject(message1);
            }

            using (var writer = new StreamWriter(listenerContext.Response.OutputStream))
            {
                //writer.WriteLine("Hello, " + listenerContext.Request.RawUrl.Trim('/') + "! 2");
                writer.WriteLine(json2);
                var str = listenerContext.Request.QueryString;
                //File.AppendAllText("C://Users//Alexandr Kulbachka//Desktop//jsonStarage//3.json", );
            }
        }


    }
}
