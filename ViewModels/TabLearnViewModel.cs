using SubProgWPF.Models;
using SubProgWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.ViewModels
{
    public class TabLearnViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private TabAddMediaViewModel _tabAddMediaViewModel;
        private TabContinueMediaViewModel _tabContinueMediaViewModel;
        private TabAddWordViewModel _tabAddWordViewModel;
        private DataGridNewWordsViewModel _listNewWordsGridViewModel;
        private int _selectedTabIndex;


        public TabLearnViewModel()
        {
            createTabViewModels();
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = _tabAddMediaViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            SelectedTabIndex = 0;

        }
        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentLearnViewModel));
        }

        public ViewModelBase CurrentLearnViewModel => _navigationStore.CurrentViewModel;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value;
                changeViewModel();
            }
        }

        private void changeViewModel()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    _navigationStore.CurrentViewModel = _tabAddMediaViewModel;
                    break;
                case 1:
                    _navigationStore.CurrentViewModel = _tabContinueMediaViewModel;
                    break;
                case 2:
                    _navigationStore.CurrentViewModel = _tabAddWordViewModel;
                    break;

            }
        }

        private void createTabViewModels()
        {
            _tabAddMediaViewModel = new TabAddMediaViewModel(this);
            _tabContinueMediaViewModel = new TabContinueMediaViewModel(this);
            _tabAddWordViewModel = new TabAddWordViewModel();
        }

        public void switchTab(string param)
        {
            switch (param)
            {
                case "ListNewWords":
                    _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
                    break;
            }
        }
        public void launchDataGrid(DataGridNewWordModel dataGridNewWordModel)
        {
            //dataGridNewWordModel.AddMediaModel.SeasonIndex = _tabAddMediaViewModel.SeasonIndex;
            //dataGridNewWordModel.AddMediaModel.EpisodeIndex = _tabAddMediaViewModel.EpisodeIndex;

            _listNewWordsGridViewModel = new DataGridNewWordsViewModel(dataGridNewWordModel);
            _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
        }
    }
}
