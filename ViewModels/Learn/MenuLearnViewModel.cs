using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.ViewModels.Learn.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SubProgWPF.ViewModels.Learn
{
    public class MenuLearnViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;
        private NavigationStore _navigationStore;
        private List<TabModel> _tabModels;
        private int _selectedTabIndex;


        public ViewModelBase CurrentLearnViewModel => _navigationStore.CurrentViewModel;
        public int SelectedTabIndex { get { return _selectedTabIndex; } set { _selectedTabIndex = value; TabSelected(); } }

        public MenuLearnViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            createTabViewModels();
            setDefaultTab();
        }

        private void setDefaultTab()
        {
            _navigationStore = new NavigationStore();
            _navigationStore.CurrentViewModel = _tabModels.FirstOrDefault(a => a.Index == 0).ViewModel;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        
       
        private void TabSelected()
        {
            TabModel tab = _tabModels.FirstOrDefault(a => a.Index == SelectedTabIndex);
            if(tab.Name.Equals("Continue") && _tabModels.FirstOrDefault(a => a.Index == -1) != null)
            {
                _navigationStore.CurrentViewModel = _tabModels.FirstOrDefault(a => a.Index == -1).ViewModel;
            }
            else
            {
                _navigationStore.CurrentViewModel = tab.ViewModel;
            }
        }

        private void createTabViewModels()
        {
            _tabModels = new List<TabModel>();
            _tabModels.Add(new TabModel()
            {
                 Index = 0,
                 Name = "Add",
                 ViewModel = new TabAddMediaViewModel(this)
            });
            _tabModels.Add(new TabModel()
            {
                Index = 1,
                Name = "Continue",
                ViewModel = new TabContinueMediaViewModel(this)
            });
            _tabModels.Add(new TabModel()
            {
                Index = 2,
                Name = "Word",
                ViewModel = new TabAddWordViewModel()
            });
            _tabModels.Add(new TabModel()
            {
                Index = 3,
                Name = "Expression",
                ViewModel = new TabAddExpressionViewModel()
            });

        }


        public void switchToNewWordsTab(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {
            _tabModels.Add(new TabModel()
            {
                Index = -1,
                Name = "LearnNewWords",
                ViewModel = new TabLearnNewWordsViewModel(dataGridNewWordModel.MembersModel, dataGridNewWordModel.AddMediaModel, this)
            });
            _navigationStore.CurrentViewModel = _tabModels.FirstOrDefault(a => a.Index == -1).ViewModel;
        }
        public void launchNewWordGridContinue(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {
            _tabModels.Add(new TabModel()
            {
                 Index = -1,
                 Name = "LearnNewWords",
                 ViewModel = new TabLearnNewWordsViewModel(dataGridNewWordModel.MembersModel, dataGridNewWordModel.AddMediaModel, this)
            });
            _navigationStore.CurrentViewModel = _tabModels.FirstOrDefault(a => a.Index == -1).ViewModel;
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
            foreach(TabModel tab in _tabModels)
            {
                tab.ViewModel.updateTheFields();
            }
        }

        internal void switchToAddMediaView()
        {
            _navigationStore.CurrentViewModel = _tabModels.FirstOrDefault(a => a.Index == -0).ViewModel;
            _tabModels.Remove(_tabModels.FirstOrDefault(a => a.Index == -1));
        }
    }
    public class TabModel
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public ViewModelBase ViewModel { get; set; }

    }
}
