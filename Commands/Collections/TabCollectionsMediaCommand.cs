using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands.Collections
{
    public class TabCollectionsMediaCommand : CommandBase
    {
        private TabCollectionsMediaViewModel _tabCollectionsMediaViewModel;

        public TabCollectionsMediaCommand(TabCollectionsMediaViewModel tabCollectionsMediaViewModel)
        {
            _tabCollectionsMediaViewModel = tabCollectionsMediaViewModel;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Tuple<string, object>)
            {
                Tuple<string, object> tuple = (Tuple<string, object>)parameter;
                switch (tuple.Item1)
                {
                    case "Main":
                        _tabCollectionsMediaViewModel.OpenCollectionItemTab((CollectionMediaModel)tuple.Item2);
                        return;
                    case "Edit":
                        //_tabCollectionsMediaViewModel.editCollection((CollectionModel)tuple.Item2);
                        return;

                }
            }
        }
    }
}
