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
        private TabCollectionsBrowseViewModel _tabcollectionsBrowseViewModel;

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
                    _navigationStore.CurrentViewModel = _tabcollectionsViewModel;
                    break;
                case 1:
                    
                    _navigationStore.CurrentViewModel = _tabcollectionsBrowseViewModel;
                    break;
            }
        }

        private void createTabViewModels()
        {
            _tabcollectionsViewModel = new TabCollectionsViewModel(this);
            _tabcollectionsBrowseViewModel = new TabCollectionsBrowseViewModel();
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentCollectionsViewModel));
        }

        public override void updateTheFields()
        {
            _tabcollectionsViewModel.updateTheFields();
            _tabcollectionsBrowseViewModel.updateTheFields();
        }



    }
}
