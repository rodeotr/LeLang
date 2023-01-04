using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.ViewModels.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.ViewModels.Storage
{
    public class MenuStorageMainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private TabStorageWordsViewModel _tabStorageWordsViewModel;
        private TabStorageExpressionsViewModel _tabStorageExpressionsViewModel;
   
        private int _selectedTabIndex;


        public MenuStorageMainViewModel()
        {
            createTabViewModels();

            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = _tabStorageWordsViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            SelectedTabIndex = 0;

        }
       

        public ViewModelBase CurrentLearnViewModel => _navigationStore.CurrentViewModel;
        
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set { _selectedTabIndex = value;
                changeViewModel();
            }
        }

        public TabStorageWordsViewModel TabStorageWordsViewModel { get => _tabStorageWordsViewModel; set => _tabStorageWordsViewModel = value; }
        public TabStorageExpressionsViewModel TabStorageExpressionsViewModel { get => _tabStorageExpressionsViewModel; set => _tabStorageExpressionsViewModel = value; }

        private void changeViewModel()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    _navigationStore.CurrentViewModel = _tabStorageWordsViewModel;
                    break;
                case 1:
                    
                    _navigationStore.CurrentViewModel = _tabStorageExpressionsViewModel;
                    break;
            }
        }

        private void createTabViewModels()
        {
            _tabStorageWordsViewModel = new TabStorageWordsViewModel(new StorageWordsModel(WordServices.getAllWords()));
            _tabStorageExpressionsViewModel = new TabStorageExpressionsViewModel(new StorageExpressionsModel(WordServices.getAllExpressions()));
        }


        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentLearnViewModel));
        }

        public override void updateTheFields()
        {
            _tabStorageWordsViewModel.updateTheFields();
            _tabStorageExpressionsViewModel.updateTheFields();
        }



    }
}
