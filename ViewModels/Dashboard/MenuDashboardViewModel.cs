using LangDataAccessLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Dashboard;
using SubProgWPF.Interfaces.Dashboard;
using SubProgWPF.Models;
using SubProgWPF.ViewModels.Learn;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Dashboard
{
    public class MenuDashboardViewModel : ViewModelBase
    {
        DashModel _dashModel;
        private readonly ICommand _tabDashCommand;
        public ICommand TabDashCommand => _tabDashCommand;

        private DashboardCardItem _episodeCard;
        private DashboardCardItem _movieCard;
        private DashboardCardItem _bookCard;
        private DashboardCardItem _youtubeCard;
        public DashboardCardItem EpisodeCard { get { return _episodeCard; } }
        public DashboardCardItem MovieCard { get { return _movieCard; } }
        public DashboardCardItem BookCard { get { return _bookCard; } }
        public DashboardCardItem YoutubeCard { get { return _youtubeCard; }}

        public ObservableCollection<DashUnfinishedMediaModel> UnfinishedMedia { get => _dashModel.UnfinishedMediaList; }


        public MenuDashboardViewModel()
        {
            _dashModel = new DashModel();
            
            _episodeCard = _dashModel.createTVEpisodeCard(MediaServices.getTVEpisodes().Count, MediaServices.getTVWords().Count);
            _movieCard = _dashModel.createMoviesCard(MediaServices.getAllMovies().Count, MediaServices.getMovieWords().Count);
            _bookCard = _dashModel.createBooksCard(MediaServices.getAllBooks().Count, MediaServices.getBookWords().Count);
            _youtubeCard = _dashModel.createYoutubeCard(MediaServices.getAllYoutubeVideos().Count, MediaServices.getYoutubeWords().Count);

            _tabDashCommand = new TabDashboardCommand(this);
        }

       
        public override void updateTheFields()
        {
            _dashModel.createUnfinishedMediaList();
            OnPropertyChanged(nameof(UnfinishedMedia));
        }

        public void switchToLearn(DashUnfinishedMediaModel media)
        {
            IHost _hostApp = (IHost)App.Current.Properties["AppHost"];
            IHost _hostMain = (IHost)App.Current.Properties["MainViewModelHost"];

            MainViewModel vM = _hostApp.Services.GetRequiredService<MainViewModel>();
            MenuLearnViewModel vMLearn = _hostMain.Services.GetRequiredService<MenuLearnViewModel>();

            vM.switchTab("Learn");
            vMLearn.SelectedTabIndex = 1;
            //vMLearn.TabContinueMediaViewModel.MediaType = media.Type.ToString();
        }


    }
}
