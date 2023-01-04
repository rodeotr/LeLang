using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Test;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using SubProgWPF.ViewModels.Test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using static SubProgWPF.Models.TestOverviewModel;

namespace SubProgWPF.ViewModels.Test
{
    public class MenuTestDashViewModel : ViewModelBase
    {
        private readonly ICommand _tabTestOverviewCommand;
        private readonly NavigationStore _navigationStore;
        private TabTestViewModel _tabTestViewModel;
        TestOverviewModel _testOverviewModel;

        

        public List<CollectionTestModel> Collections { get => _testOverviewModel.Collections; set => _testOverviewModel.Collections = value; }
        public ICommand TabTestCommand => _tabTestOverviewCommand;
        public ViewModelBase CurrentLearnViewModel => _navigationStore.CurrentViewModel;
        public TestOverviewModel Model { get => _testOverviewModel; set => _testOverviewModel = value; }

       

        public MenuTestDashViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _tabTestOverviewCommand = new TabTestOverviewCommand(this);
            _testOverviewModel = new TestOverviewModel();
            createTabViewModels();

        }

        

        
     
        private void createTabViewModels()
        {
            _tabTestViewModel = new TabTestViewModel(TYPE.Practice30, this);
        }


        

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentLearnViewModel));
        }

        public void switchTab(string param)
        {
            if(_tabTestViewModel.RemainingWordCount > 0)
            {
                switch (param.ToString())
                {
                    case "Practice_30":
                        _navigationStore.CurrentViewModel = new TabTestViewModel(TYPE.Practice30,this);
                        break;
                    case "Practice_60":
                        _navigationStore.CurrentViewModel = new TabTestViewModel(TYPE.Practice60,this);
                        break;
                    case "Practice_All":
                        _navigationStore.CurrentViewModel = new TabTestViewModel(TYPE.PracticeAll,this);
                        break;
                }
            }
            
        }
        public void ExitCurrentSession()
        {
            updateTheFields();
            _navigationStore.CurrentViewModel = this;
        }

        public override void updateTheFields()
        {
            _testOverviewModel.updateWords();
        }
    }
    
}
