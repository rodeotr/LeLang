using LangDataAccessLibrary;
using LangDataAccessLibrary.ServerDBModels;
using LangDataAccessLibrary.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubProgWPF.Utils
{
    public class ServerUtils
    {
        public static async Task<Collections> getCollectionFromServerAsync(string id)
        {
            string json = await GetCollectionHttpGetRequestAsync(id);
            return JsonConvert.DeserializeObject<LangDataAccessLibrary.ServerDBModels.Collections>(json);

        }
        public static async Task<ServerChat> getServerCollectionChat(string collectionPublicId) {
            string json = await getServerCollectionChatHttpGetRequestAsync(collectionPublicId);
            return JsonConvert.DeserializeObject<LangDataAccessLibrary.ServerDBModels.ServerChat>(json);
        }

        private static async Task<string> getServerCollectionChatHttpGetRequestAsync(string collectionPublicId)
        {
            string html = string.Empty;
            //string url = @"http://18.184.60.51/pN2wNVf3DBFxA5VFtwBH/" + collectionPublicId;
            string url = @"http://localhost:51524/pN2wNVf3DBFxA5VFtwBH/" + collectionPublicId;

            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync(url);
            }

            return result;
        }

        internal async static Task<HttpResponseMessage> SendMessage(ServerChatMessage serverChatMessage)
        {
            
            string chatJson = Newtonsoft.Json.JsonConvert.SerializeObject(serverChatMessage);
            
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.PostAsync(
                    //"http://18.184.60.51/GGVFYhpidNY6HEPg46cm/",
                    "http://localhost:51524/GGVFYhpidNY6HEPg4sss/",
                     new StringContent(chatJson, Encoding.UTF8, "application/json"));
            }
            return response;
        }

        

        private static async Task<string> GetCollectionHttpGetRequestAsync(string id)
        {
            string html = string.Empty;
            string url = @"http://18.184.60.51/pN2wNVf3DBFxA5VFtwBH/" + id;

            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync(url);
            }

            return result;
        }

        public static async Task<string> AllCollectionsHttpGetRequestAsync()
        {
            string html = string.Empty;
            string url = @"http://18.184.60.51/WR3dEWtCi17j0yqYflAl/";

            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync(url);
            }

            return result;
        }
        public static async void publishCollectionToServer(string name)
        {
            if (CollectionServices.getCollectionByName(name).MediaType == MediaTypes.TYPE.Youtube)
            {
                string collectionJson = CollectionServices.getServerCollection(name);
                await PublishCollectionHttpPostRequestAsync(collectionJson);
            }
        }
        private static async Task<HttpResponseMessage> PublishCollectionHttpPostRequestAsync(string json)
        {
            //string myJson = "{'Username': 'myusername','Password':'pass'}";

            //string myJson = "{'Username': " + "'" +  CreateString(1000000) + "'" + "}";
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.PostAsync(
                    //"http://18.184.60.51/GGVFYhpidNY6HEPg46cm/",
                    "http://localhost:51524/GGVFYhpidNY6HEPg46cm/",

                     new StringContent(json, Encoding.UTF8, "application/json"));
            }
            return response;
        }



    }
}
