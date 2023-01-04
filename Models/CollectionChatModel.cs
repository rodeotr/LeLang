using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.ServerDBModels;
using LangDataAccessLibrary.Services;
using SubProgWPF.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SubProgWPF.Models
{
    public class CollectionChatModel
    {


        private ServerChat _serverChat;
        private readonly string _collectionId;

        public CollectionChatModel(string collectionId)
        {
            _collectionId = collectionId;
        }
        public async Task<int> SendMessage(string message)
        {
            User user = SettingServices.getCurrentUser();
            ServerChatMessage serverChatMessage = new ServerChatMessage() {
                ServerChatId = _serverChat.Id,
                Message = message,
                UserId = user.PublicId,
                UserName = user.Name
            };
            HttpResponseMessage response = await ServerUtils.SendMessage(serverChatMessage);
            return Int32.Parse(response.Content.ToString());
        }

        public async Task<ServerChat> RefreshChat()
        {
            return await ServerUtils.getServerCollectionChat(_collectionId);
        }

    }

   



}
