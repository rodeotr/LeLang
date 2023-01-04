using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.ServerDBModels;
using LangDataAccessLibrary.Services;
using Newtonsoft.Json;
using SubProgWPF.Utils;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands.Collections
{
    public class TabCollectionsBrowseCommand : CommandBase
    {
        private TabCollectionsBrowseViewModel _tabCollectionsBrowseViewModel;

        public TabCollectionsBrowseCommand(TabCollectionsBrowseViewModel tabCollectionsViewModel)
        {
            _tabCollectionsBrowseViewModel = tabCollectionsViewModel;
        }

        public async override void Execute(object parameter)
        {
            if(parameter is ServerCollectionItem)
            {
                ServerCollectionItem item = (ServerCollectionItem)parameter;
                LangDataAccessLibrary.ServerDBModels.Collections collection = await ServerUtils.getCollectionFromServerAsync(item.Id);
                LangDataAccessLibrary.Models.Collections collection_ = CollectionServices.convertServerCollectionToCollection(collection);
                CollectionCreator.CreateCollectionFromServer(collection_, collection.Language);
            }
        }
    }
}
