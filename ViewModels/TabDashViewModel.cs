using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class TabDashViewModel : ViewModelBase
    {
        DashModel dashModel;
        private readonly ICommand _tabDashCommand;
        private string _totalTVEpisodes;
        private string _totalTVWords;
        private string _totalMovies;
        private string _totalMovieWords;
        private string _totalVideoPodcasts;
        private string _totalVideoPodcastsWords;

        public ICommand TabDashCommand => _tabDashCommand;

        public string TotalTVEpisodes { get => _totalTVEpisodes; set { _totalTVEpisodes = value; OnPropertyChanged(nameof(TotalTVEpisodes)); } }
        public string TotalTVWords { get => _totalTVWords; set { _totalTVWords = value; OnPropertyChanged(nameof(TotalTVWords)); } }
        public string TotalMovies { get => _totalMovies; set { _totalMovies = value; OnPropertyChanged(nameof(TotalMovies)); } }
        public string TotalMovieWords { get => _totalMovieWords; set { _totalMovieWords = value; OnPropertyChanged(nameof(TotalMovieWords)); } }
        public string TotalVideoPodcasts { get => _totalVideoPodcasts; set { _totalVideoPodcasts = value; OnPropertyChanged(nameof(TotalVideoPodcasts)); } }
        public string TotalVideoPodcastsWords { get => _totalVideoPodcastsWords; set { _totalVideoPodcastsWords = value; OnPropertyChanged(nameof(TotalVideoPodcastsWords)); } }

        public TabDashViewModel(NavigationStore navigationStore)
        {
            dashModel = new DashModel();
            _tabDashCommand = new TabDashCommand(this);


            _totalTVEpisodes = dashModel.TvEpisodes.Count.ToString();
            _totalTVWords = WordServices.getTotalWordsCount().ToString();

            _totalMovies = MediaServices.getTotalMovieCount().ToString();
            _totalMovieWords = _totalTVWords;

            _totalVideoPodcasts = _totalTVEpisodes;
            _totalVideoPodcastsWords = _totalTVWords;
        }


    }
}
