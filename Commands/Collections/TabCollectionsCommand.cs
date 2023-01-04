using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands.Collections
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
            if(parameter is Tuple<string, object>)
            {
                Tuple<string, object> tuple = (Tuple<string, object>)parameter;
                switch (tuple.Item1)
                {
                    case "Main":
                        _tabCollectionsViewModel.OpenCollectionItemTab((CollectionModel)tuple.Item2);
                        return;
                    case "Edit":
                        _tabCollectionsViewModel.editCollection((CollectionModel)tuple.Item2);
                        return;

                }
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
