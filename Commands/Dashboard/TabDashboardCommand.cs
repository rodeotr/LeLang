using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;
using SubProgWPF.ViewModels.Dashboard;

namespace SubProgWPF.Commands.Dashboard
{
    public class TabDashboardCommand : CommandBase
    {
        private MenuDashboardViewModel _tabDashViewModel;

        public TabDashboardCommand(MenuDashboardViewModel tabDashViewModel)
        {
            _tabDashViewModel = tabDashViewModel;
        }

        public override void Execute(object parameter)
        {
            _tabDashViewModel.switchToLearn((DashUnfinishedMediaModel)parameter);
        }
    }
}
