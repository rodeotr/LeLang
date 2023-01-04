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
    public class TabCollectionsMediaViewModel : ViewModelBase
    {


        private readonly ICommand _tabCollectionsMediaCommand;
        private readonly MenuCollectionsMainViewModel _mainViewModel;
        private ObservableCollection<CollectionMediaModel> _collectionList;
        private List<ColorCouple> _colorCouples;
        List<FTVEpisode> episodes;
        List<List<Word>> allEpisodeWords;

        internal void OpenCollectionItemTab(CollectionMediaModel item2)
        {
            _mainViewModel.OpenCollectionMediaItemTab(item2);
        }

        public ICommand TabCollectionsMediaCommand => _tabCollectionsMediaCommand;

        public ObservableCollection<CollectionMediaModel> CollectionList { get => _collectionList; set { _collectionList = value; OnPropertyChanged(nameof(CollectionList)); } }

        public TabCollectionsMediaViewModel(MenuCollectionsMainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _tabCollectionsMediaCommand = new TabCollectionsMediaCommand(this);
            episodes = MediaServices.getTVEpisodes();
            allEpisodeWords = new List<List<Word>>();
            List<Word> allWords = WordServices.getAllWords();
            foreach (FTVEpisode e in episodes)
            {
                List<Word> eWords = WordServices.getAllWordsOfTVSeries(e.Id, allWords);
                allEpisodeWords.Add(eWords);
            }
            setColorCouples();
            populateCollectionList();
        }

        private void populateCollectionList()
        {
            _collectionList = new ObservableCollection<CollectionMediaModel>();
            Random r = new Random();
            for (int i = 0; i < episodes.Count; i++)
            {
                FTVEpisode episode = episodes[i];
                List<Word> words = allEpisodeWords[i];
                _collectionList.Add(
                    new CollectionMediaModel()
                    {
                        Name = episode.Season.Series.Name + " " + "S" + episode.Season.SeasonIndex + "E" + episode.EpisodeIndex,
                        Words = words,
                        Color = _colorCouples[r.Next(_colorCouples.Count)],
                        CollectionTypeIconKind = "DesktopMac",
                        TotalElements = words.Count.ToString(),
                        TotalExamples = ""
                    }
                );
            }
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

        public override void updateTheFields()
        {
            
        }

        


    }

    public class CollectionMediaModel
    {
        public string Name { get; set; }
        public ColorCouple Color { get; set; }
        public List<Word> Words { get; set; }
        public string TotalElements { get; set; }
        public string TotalExamples { get; set; }
        public string CollectionTypeIconKind { get; set; }
    }


}
