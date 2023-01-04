using SubProgWPF.Stores;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using LangDataAccessLibrary.Services;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SubProgWPF.ViewModels.Dashboard;
using SubProgWPF.ViewModels.Learn;
using SubProgWPF.ViewModels.Test;
using SubProgWPF.ViewModels.Media;
using SubProgWPF.ViewModels.Storage;
using SubProgWPF.ViewModels.Settings;


namespace SubProgWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private IHost _host;
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

            
            createLeftPanelViewModels();
            createLeftPanel();
        }

        private void createLeftPanelViewModels()
        {

            IHost appHost = (IHost)App.Current.Properties["AppHost"];

            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton(new MenuTestDashViewModel(_navigationStore));
                services.AddSingleton(new LeftPanelViewModel(new LeftPanelModel(_leftMenuItems), this));
                services.AddSingleton(new MenuDashboardViewModel());
                services.AddSingleton(new MenuLearnViewModel(this));
                services.AddSingleton(new MenuMediaViewModel(_navigationStore));
                services.AddSingleton(new MenuStorageMainViewModel());
                services.AddSingleton(new MenuCollectionsMainViewModel());
                services.AddSingleton(new MenuSettingsViewModel(this));

            }).Build();
            App.Current.Properties["MainViewModelHost"] = _host;

            _host.Start();

            LeftPanelViewModel = _host.Services.GetRequiredService<LeftPanelViewModel>();

        }

        private void createLeftPanel()
        {
            createLeftPanelItems();
            MenuTestDashViewModel vM = _host.Services.GetRequiredService<MenuTestDashViewModel>(); ;
            ((LeftPanelViewModel)LeftPanelViewModel).TestWordCount = vM.Model.TotalWordsToBeTested.ToString();
            ((LeftPanelViewModel)LeftPanelViewModel).TestVisibility = vM.Model.TotalWordsToBeTested > 0;

            _navigationStore.CurrentViewModel = _leftMenuItems.FirstOrDefault(a => a.ItemName.Equals("DashBoard")).VM;
            _tabTestDashViewModel = _leftMenuItems.FirstOrDefault(a => a.ItemName.Equals("Test")).VM;


            //LeftPanelViewModel = new LeftPanelViewModel(new LeftPanelModel(_leftMenuItems), this);
        }

        private void createLeftPanelItems()
        {
            _leftMenuItems = new List<LeftMenuItemModel>();
            _leftMenuItems.Add(new LeftMenuItemModel("HomeVariant","DashBoard", _host.Services.GetRequiredService<MenuDashboardViewModel>()));
            _leftMenuItems.Add(new LeftMenuItemModel("DatabasePlus", "Learn", _host.Services.GetRequiredService<MenuLearnViewModel>()));
            _leftMenuItems.Add(new LeftMenuItemModel("Flask", "Test", _host.Services.GetRequiredService<MenuTestDashViewModel>()));
            _leftMenuItems.Add(new LeftMenuItemModel("Book", "Media", _host.Services.GetRequiredService<MenuMediaViewModel>()));
            _leftMenuItems.Add(new LeftMenuItemModel("Archive", "Storage", _host.Services.GetRequiredService<MenuStorageMainViewModel>()));
            _leftMenuItems.Add(new LeftMenuItemModel("Rhombus", "Collections", _host.Services.GetRequiredService<MenuCollectionsMainViewModel>()));
            _leftMenuItems.Add(new LeftMenuItemModel("Earth", "Browse", null));
            _leftMenuItems.Add(new LeftMenuItemModel("Help", "FAQ", null));
            _leftMenuItems.Add(new LeftMenuItemModel("Cog", "Settings", _host.Services.GetRequiredService<MenuSettingsViewModel>()));

            ((LeftPanelViewModel)LeftPanelViewModel).LeftPanel.LeftMenuItems = _leftMenuItems;

            ((LeftPanelViewModel)LeftPanelViewModel).LeftPanelChanged();
        }

        public void switchTab(string param)
        {
            _navigationStore.CurrentViewModel = _leftMenuItems.FirstOrDefault(a => a.ItemName.Equals(param.ToString())).VM;
        }

        public override void updateTheFields()
        {
            foreach(LeftMenuItemModel model in _leftMenuItems)
            {
                if (model.ItemName.Equals("Ranking") || model.VM == null)
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
