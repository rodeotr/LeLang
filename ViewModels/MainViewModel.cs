using SubProgWPF.Stores;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using LangDataAccessLibrary.Services;

namespace SubProgWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public Vlc.DotNet.Core.VlcMediaPlayer MediaPlayer;

        private readonly NavigationStore _navigationStore;
        private  ViewModelBase _tabDashViewModel;
        private  ViewModelBase _tabMediaViewModel;
        private ViewModelBase _tabLearnViewModel;
        private ViewModelBase _tabTestViewModel;
        private ViewModelBase _storageViewModel;
        private ViewModelBase _listNewWordsGridViewModel;
        bool IsMediaPlayerPlaying;



        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public ViewModelBase LeftPanelViewModel { get; }
        public ViewModelBase TabLearnViewModel { get; }




        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        
        public MainViewModel(NavigationStore navigationStore, Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer, bool isPlaying)
        {
            //_mediaPlayer.Stop();
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            IsMediaPlayerPlaying = isPlaying;
            MediaPlayer = _mediaPlayer;
            createTabViewModels(_mediaPlayer);

            LeftPanelViewModel = new LeftPanelViewModel(new LeftPanelModel(
                "GlobeModel",
                new List<LeftMenuItemModel>()), this);
            TabLearnViewModel = new TabLearnViewModel();

            //_tabDashBoardVisibility = Visibility.Visible;
            //_tabTestVisibility = Visibility.Hidden;
            //_tabAddVisibility = Visibility.Hidden;
        }

        private void createTabViewModels(Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer)
        {
            _tabDashViewModel = CurrentViewModel;
            _tabLearnViewModel = new TabLearnViewModel();
            _tabTestViewModel = new TabTestViewModel(_mediaPlayer);
            _storageViewModel = new TabStorageViewModel(new StorageMemberModel(WordServices.getAllWords()));
            _tabMediaViewModel = new TabMediaViewModel(_navigationStore);
        }

        public void switchTab(string param)
        {
            //TabDashBoardVisibility = Visibility.Hidden;
            //TabAddVisibility = Visibility.Hidden;
            //TabTestVisibility = Visibility.Hidden;
            switch (param)
            {
                case "learnButton":
                    _navigationStore.CurrentViewModel = _tabLearnViewModel;
                    break;
                case "dashButton":
                    _navigationStore.CurrentViewModel = _tabDashViewModel;
                    break;
                case "testButton":
                    _navigationStore.CurrentViewModel = _tabTestViewModel;
                    break;
                case "mediaButton":
                    _navigationStore.CurrentViewModel = _tabMediaViewModel;
                    break;
                case "ListNewWords":
                    _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
                    break;
                case "storageButton":
                    _navigationStore.CurrentViewModel = _storageViewModel;
                    break;
            }
        }
        public void launchDataGrid(DataGridNewWordModel dataGridNewWordModel)
        {

            _listNewWordsGridViewModel = new DataGridNewWordsViewModel(dataGridNewWordModel);
            _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
        }
    }
}
