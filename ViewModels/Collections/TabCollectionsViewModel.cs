using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.Windows.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace SubProgWPF.ViewModels.Collections
{
    public class TabCollectionsViewModel : ViewModelBase
    {

        MenuCollectionsMainViewModel _mainViewModel;
        private readonly ICommand _tabCollectionsCommand;
        private ObservableCollection<CollectionModel> _collectionList;
        private List<LangDataAccessLibrary.Models.Collections> _collections;
        private List<ColorCouple> _colorCouples;

        public ICommand TabCollectionsCommand => _tabCollectionsCommand;

        internal void OpenCollectionItemTab(CollectionModel collection)
        {
            _mainViewModel.OpenCollectionItemTab(_collections.FirstOrDefault(a => a.Name.Equals(collection.Name)));
        }

        public ObservableCollection<CollectionModel> CollectionList { get => _collectionList; set { _collectionList = value; OnPropertyChanged(nameof(CollectionList)); } }

        public TabCollectionsViewModel(MenuCollectionsMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _tabCollectionsCommand = new TabCollectionsCommand(this);
            setColorCouples();
            setCollectionList();

        }

        

        private void setCollectionList()
        {
            _collections = CollectionServices.getCollections();
            _collectionList = new ObservableCollection<CollectionModel>();
            Random r = new Random();
            foreach(LangDataAccessLibrary.Models.Collections c in _collections)
            {
                string iconKind = c.MediaType == MediaTypes.TYPE.Youtube ? "Youtube" :
                    c.MediaType == MediaTypes.TYPE.TVSeries ? "DesktopMac" :
                    c.MediaType == MediaTypes.TYPE.Book ? "Book" :
                    c.MediaType == MediaTypes.TYPE.Random ? "BowlMix" : "FilmStrip";

                CollectionModel model = new CollectionModel() {
                    Name = c.Name,
                    CollectionTypeIconKind = iconKind,
                    TotalElements = c.TotalEntities.ToString(),
                    TotalExamples = c.TotalContexts.ToString(),
                    Color = _colorCouples[r.Next(_colorCouples.Count)],
                    CreationTime = c.InitTime.ToString(),
                    CollectionType = c.CollectionType.ToString(),
                    MediaType = c.MediaType.ToString()
                };
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                _collectionList.Add(model);
            }
            OnPropertyChanged(nameof(CollectionList));
        }

        internal void createCollection(string name, string collectionType, string mediumType)
        {
            List<LangDataAccessLibrary.Models.Collections> collections = CollectionServices.getCollections();
            if(collections.FirstOrDefault(a=>a.Name.Equals(name)) != null)
            {
                MessageBox.Show("A collection with this name already exists.");
                return;
            }
            LangDataAccessLibrary.Models.Collections collection = new LangDataAccessLibrary.Models.Collections();
            CollectionTypes.TYPE _colType;
            Enum.TryParse(collectionType, out _colType);
            collection.CollectionType = _colType;
            MediaTypes.TYPE _mediumType;
            Enum.TryParse(mediumType, out _mediumType);
            collection.MediaType = _mediumType;
            collection.Name = name;

            CollectionCreator.CreateCollection(collection);
            setCollectionList();

            MessageBox.Show("The collection has been created.");

        }

        internal void launchCreateCollectionWindow()
        {
            CreateCollectionWindow window = new CreateCollectionWindow(this);
            window.Show();
        }

        public override void updateTheFields()
        {
            setCollectionList();

        }

        internal void editCollection(CollectionModel model)
        {
            EditCollectionWindow editCollectionWindow = new EditCollectionWindow(model);
            editCollectionWindow.Show();
        }

        private void setColorCouples()
        {
            _colorCouples = new List<ColorCouple>();
            _colorCouples.Add(new ColorCouple()
            {
                Color1 = "#740058",
                Color2 = "#55003D"
            });
            _colorCouples.Add(new ColorCouple()
            {
                Color1 = "#008B74",
                Color2 = "#005e52"
            });
            _colorCouples.Add(new ColorCouple()
            {
                Color1 = "#942D24",
                Color2 = "#320F0C"
            });
            _colorCouples.Add(new ColorCouple()
            {
                Color1 = "#191F24",
                Color2 = "#2C363F"
            });
        }

    }

    public class ColorCouple
    {
        public string Color1 { get; set; }
        public string Color2 { get; set; }
    }
    public class CollectionModel
    {
        public string Name { get; set; }
        public ColorCouple Color { get; set; }
        public string TotalElements { get; set; }
        public string TotalExamples { get; set; }
        public string TotalDownload { get; set; }
        public string CollectionTypeIconKind { get; set; }
        public string CreationTime { get; set; }
        public string CollectionType { get; set; }
        public string MediaType { get; set; }
    }
    public class CollectionDBModel
    {
        public string Name { get; set; }

    }
}
