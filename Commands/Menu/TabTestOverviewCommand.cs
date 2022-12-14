using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabTestOverviewCommand : CommandBase
    {
        private MenuTestDashViewModel _tabTestViewModel;

        public TabTestOverviewCommand(MenuTestDashViewModel tabTestViewModel)
        {
            _tabTestViewModel = tabTestViewModel;
        }

        public override void Execute(object parameter)
        {
            if(parameter is CollectionTestModel)
            {

            }
            _tabTestViewModel.switchTab(parameter.ToString());
        }

    }
}
