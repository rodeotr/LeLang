using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabDashCommand : CommandBase
    {
        private MenuDashViewModel _tabDashViewModel;

        public TabDashCommand(MenuDashViewModel tabDashViewModel)
        {
            _tabDashViewModel = tabDashViewModel;
        }

        public override void Execute(object parameter)
        {
            Console.WriteLine(parameter);
        }
    }
}
