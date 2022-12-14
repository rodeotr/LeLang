using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabCollectionsItemCommand : CommandBase
    {
        private TabCollectionsItemViewModel _tabCollectionsViewModel;

        public TabCollectionsItemCommand(TabCollectionsItemViewModel tabCollectionsViewModel)
        {
            _tabCollectionsViewModel = tabCollectionsViewModel;
        }

        public override void Execute(object parameter)
        {
            _tabCollectionsViewModel.ShowContext((CollectionItemContext)parameter);
            Console.WriteLine("");
        }
    }
}
