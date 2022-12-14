using SubProgWPF.Models;
using SubProgWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.ViewModels.Learning
{
    public class TabLearnViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private NavigationStore _navigationStore;

        private TabAddMediaViewModel _tabAddMediaViewModel;
        private TabContinueMediaViewModel _tabContinueMediaViewModel;
        private TabAddWordViewModel _tabAddWordViewModel;
        private TabAddExpressionViewModel _tabAddExpressionViewModel;


        private ListWordsModel _gridContinueModel;
        private DataGridNewWordsViewModel _listNewWordsGridViewModel;

        private int _selectedTabIndex;
        private int _selectedTranscriptionId;


        public ViewModelBase CurrentLearnViewModel => _navigationStore.CurrentViewModel;
        public int SelectedTabIndex { get { return _selectedTabIndex; } set { _selectedTabIndex = value; changeViewModel(); } }
        public int SelectedTranscriptionId { get => _selectedTranscriptionId; set => _selectedTranscriptionId = value; }


        public TabLearnViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            createTabViewModels();
            setDefaultTab();
        }

        private void setDefaultTab()
        {
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = _tabAddMediaViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        
       
        private void changeViewModel()
        {
            switch (SelectedTabIndex)
            {
                case 0:
                    _navigationStore.CurrentViewModel = _tabAddMediaViewModel;
                    break;
                case 1:
                    if(_gridContinueModel != null)
                    {
                        _listNewWordsGridViewModel = new DataGridNewWordsViewModel(_gridContinueModel, this);
                        _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
                    }
                    else
                    {
                        _navigationStore.CurrentViewModel = _tabContinueMediaViewModel;
                    }
                    break;
                case 2:
                    _navigationStore.CurrentViewModel = _tabAddWordViewModel;
                    break;
                case 3:
                    _navigationStore.CurrentViewModel = _tabAddExpressionViewModel;
                    break;
            }
        }

        private void createTabViewModels()
        {
            _tabAddMediaViewModel = new TabAddMediaViewModel(this);
            _tabContinueMediaViewModel = new TabContinueMediaViewModel(this);
            _tabAddWordViewModel = new TabAddWordViewModel();
            _tabAddExpressionViewModel = new TabAddExpressionViewModel();
        }


        public void switchToNewWordsTab(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {
            _listNewWordsGridViewModel = new DataGridNewWordsViewModel(dataGridNewWordModel, this);
            _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
        }
        public void launchNewWordGridContinue(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {
            _gridContinueModel = dataGridNewWordModel;
            _selectedTranscriptionId = transcriptionId;
            _listNewWordsGridViewModel = new DataGridNewWordsViewModel(dataGridNewWordModel, this);
            _navigationStore.CurrentViewModel = _listNewWordsGridViewModel;
        }

        public void notifyTheMainViewModelForUpdate()
        {
            _mainViewModel.updateTheFields();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentLearnViewModel));
        }

        public override void updateTheFields()
        {
            _tabAddMediaViewModel.updateTheFields();
            _tabContinueMediaViewModel.updateTheFields();
            //_tabAddWordViewModel.updateTheFields();
        }

        internal void switchToAddMediaView()
        {
            _navigationStore.CurrentViewModel = _tabAddMediaViewModel;
            _listNewWordsGridViewModel = null;
            

        }
    }
}
