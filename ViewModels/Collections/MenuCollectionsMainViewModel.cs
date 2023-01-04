using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.ViewModels
{
    public class MenuCollectionsMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private TabCollectionsViewModel _tabcollectionsViewModel;
        private TabCollectionsMediaViewModel _tabcollectionsMediaViewModel;

        private int _selectedTabIndex;


        public MenuCollectionsMainViewModel()
        {
            createTabViewModels();

            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = _tabcollectionsViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            SelectedTabIndex = 0;

        }
       

        public ViewModelBase CurrentCollectionsViewModel => _navigationStore.CurrentViewModel;

        internal void OpenCollectionItemTab(LangDataAccessLibrary.Models.Collections collections)
        {
            _navigationStore.CurrentViewModel = new TabCollectionsItemViewModel(collections);
        }

        internal void OpenCollectionMediaItemTab(CollectionMediaModel collection)
        {
            _navigationStore.CurrentViewModel = new TabCollectionsMediaItemViewModel(collection);
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value;
                changeViewModel();
            }
        }

        public TabCollectionsViewModel TabcollectionsViewModel { get => _tabcollectionsViewModel; set => _tabcollectionsViewModel = value; }

        private void changeViewModel()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    _navigationStore.CurrentViewModel = _tabcollectionsViewModel;
                    break;
                case 1:
                    
                    _navigationStore.CurrentViewModel = _tabcollectionsMediaViewModel;
                    break;
            }
        }

        private void createTabViewModels()
        {
            _tabcollectionsViewModel = new TabCollectionsViewModel(this);
            _tabcollectionsMediaViewModel = new TabCollectionsMediaViewModel(this);
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentCollectionsViewModel));
        }

        public override void updateTheFields()
        {
            _tabcollectionsViewModel.updateTheFields();
            _tabcollectionsMediaViewModel.updateTheFields();
        }



    }
}
