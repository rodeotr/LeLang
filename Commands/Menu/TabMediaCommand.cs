using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabMediaCommand : CommandBase
    {
        private MenuMediaViewModel _tabMediaViewModel;

        public TabMediaCommand(MenuMediaViewModel tabMediaViewModel)
        {
            _tabMediaViewModel = tabMediaViewModel;
        }

        public override void Execute(object parameter)
        {
            //int index = Int32.Parse(parameter.ToString()) - 1;
            //_tabMediaViewModel.SelectedIndex = parameter.ToString();

        }
    }
}
