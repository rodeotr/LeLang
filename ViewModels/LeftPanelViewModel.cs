using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SubProgWPF.ViewModels
{
    public class LeftPanelViewModel : ViewModelBase
    {
        private readonly Visibility _active_tab;
        private readonly ICommand _leftPanelCommand;
        private readonly MainViewModel _mainViewModel;
        private  Visibility _dashVisibility;
        private  Visibility _addVisibility;
        private  Visibility _testVisibility;

        private readonly LeftPanelModel leftPanel;
        
        public ICommand LeftPanelCommand => _leftPanelCommand;

        public Visibility DashVisibility
        {
            get { return _dashVisibility; }
            set { _dashVisibility = value; OnPropertyChanged(nameof(DashVisibility)); }
        }
        public Visibility AddVisibility
        {
            get { return _addVisibility; }
            set { _addVisibility = value; OnPropertyChanged(nameof(AddVisibility)); }
        }
        public Visibility TestVisibility
        {
            get { return _testVisibility; }
            set { _testVisibility = value; OnPropertyChanged(nameof(TestVisibility)); }
        }


        public LeftPanelViewModel(LeftPanelModel leftPanel, MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            this.leftPanel = leftPanel;
            _leftPanelCommand = new LeftPanelCommand(this);
            _dashVisibility = Visibility.Visible;
            _testVisibility = Visibility.Hidden;
            _active_tab = _dashVisibility;

        }


        public void switchTab(string param)
        {
            _mainViewModel.switchTab(param);
        }
    }
}
