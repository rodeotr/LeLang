using SubProgWPF.Stores;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels.Learning;
using System.Linq;

namespace SubProgWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private List<LeftMenuItemModel> _leftMenuItems;
        private ViewModelBase _tabTestDashViewModel;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase LeftPanelViewModel { get; set; }
        public ViewModelBase TabTestDashViewModel { get => _tabTestDashViewModel; }

        

        
        public MainViewModel(NavigationStore appNavigationStore)
        {
            _navigationStore = appNavigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            createLeftPanel();
        }

        private void createLeftPanel()
        {
            createLeftPanelItems();

            _navigationStore.CurrentViewModel = _leftMenuItems.FirstOrDefault(a => a.ItemName.Equals("DashBoard")).VM;
            _tabTestDashViewModel = _leftMenuItems.FirstOrDefault(a => a.ItemName.Equals("Test")).VM;
            LeftPanelViewModel = new LeftPanelViewModel(new LeftPanelModel(_leftMenuItems), this);
        }

        private void createLeftPanelItems()
        {
            _leftMenuItems = new List<LeftMenuItemModel>();
            _leftMenuItems.Add(new LeftMenuItemModel("HomeVariant","DashBoard", new MenuDashViewModel()));
            _leftMenuItems.Add(new LeftMenuItemModel("DatabasePlus", "Learn", new TabLearnViewModel(this)));
            _leftMenuItems.Add(new LeftMenuItemModel("Flask", "Test", new MenuTestDashViewModel(_navigationStore)));
            _leftMenuItems.Add(new LeftMenuItemModel("Book", "Media", new MenuMediaViewModel(_navigationStore)));
            _leftMenuItems.Add(new LeftMenuItemModel("Archive", "Storage", new MenuStorageMainViewModel()));
            _leftMenuItems.Add(new LeftMenuItemModel("Rhombus", "Collections", new MenuCollectionsMainViewModel()));
            _leftMenuItems.Add(new LeftMenuItemModel("Earth", "Browse", null));
            _leftMenuItems.Add(new LeftMenuItemModel("Help", "FAQ", null));
            _leftMenuItems.Add(new LeftMenuItemModel("Cog", "Settings", new MenuSettingsViewModel(this)));
            
            
        }

        public void switchTab(string param)
        {
            _navigationStore.CurrentViewModel = _leftMenuItems.FirstOrDefault(a => a.ItemName.Equals(param.ToString())).VM;
        }

        public override void updateTheFields()
        {
            foreach(LeftMenuItemModel model in _leftMenuItems)
            {
                if (model.ItemName.Equals("Ranking"))
                {
                    continue;
                }
                model.VM.updateTheFields();
            }
            ((LeftPanelViewModel)LeftPanelViewModel).updateTheFields();
            ((LeftPanelViewModel)LeftPanelViewModel).TestWordCount = ((MenuTestDashViewModel)_tabTestDashViewModel).Model.TotalWordsToBeTested.ToString();
            
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
