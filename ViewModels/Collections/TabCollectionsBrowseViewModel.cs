using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.ServerDBModels;
using LangDataAccessLibrary.Services;
using Newtonsoft.Json;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Collections;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.Utils;
using SubProgWPF.Windows.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SubProgWPF.ViewModels.Collections
{
    public class TabCollectionsBrowseViewModel : ViewModelBase
    {


        private readonly ICommand _tabCollectionsCommand;
        private ObservableCollection<ServerCollectionItem> _collectionList;
        private List<ColorCouple> _colorCouples;

        public ICommand TabCollectionsCommand => _tabCollectionsCommand;

        public ObservableCollection<ServerCollectionItem> CollectionList { get => _collectionList; set { _collectionList = value; OnPropertyChanged(nameof(CollectionList)); } }

        public TabCollectionsBrowseViewModel()
        {
            _tabCollectionsCommand = new TabCollectionsBrowseCommand(this);
            setCollectionList();

        }

        

        private async void setCollectionList()
        {
            try
            {
                string json = await ServerUtils.AllCollectionsHttpGetRequestAsync();

                ServerCollectionOverview collectionsOverview = JsonConvert.DeserializeObject<ServerCollectionOverview>(json);
                _collectionList = new ObservableCollection<ServerCollectionItem>(collectionsOverview.Collections);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private async Task<HttpResponseMessage> AllCollectionsHttpPostRequestAsync()
        {
            //string myJson = "{'Username': 'myusername','Password':'pass'}";

            //string myJson = "{'Username': " + "'" +  CreateString(1000000) + "'" + "}";
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                response = await client.PostAsync(
                    "http://18.184.60.51/WR3dEWtCi17j0yqYflAl/",
                     new StringContent("", Encoding.UTF8, "application/json"));
            }
            return response;
        }

        

       
        public override void updateTheFields()
        {
            
        }

        


    }

   
}
