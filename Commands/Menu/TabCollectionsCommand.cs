using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabCollectionsCommand : CommandBase
    {
        private TabCollectionsViewModel _tabCollectionsViewModel;

        public TabCollectionsCommand(TabCollectionsViewModel tabCollectionsViewModel)
        {
            _tabCollectionsViewModel = tabCollectionsViewModel;
        }

        public override void Execute(object parameter)
        {

            if(parameter is CollectionModel)
            {
                _tabCollectionsViewModel.editCollection((CollectionModel)parameter);
                //_tabCollectionsViewModel.OpenCollectionItemTab((CollectionModel)parameter);
                return;
            }
            string paramStr = parameter.ToString();
            switch (paramStr)
            {
                case "Create":
                    _tabCollectionsViewModel.launchCreateCollectionWindow();
                    break;
                
            }
            

            //_tabCollectionsViewModel.launchCreateCollectionWindow();
            //Console.WriteLine(parameter);
        }
    }
}
