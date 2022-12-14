using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using Microsoft.EntityFrameworkCore;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace SubProgWPF.Commands
{
    public class TabStorageCommand : CommandBase
    {
        private readonly TabStorageWordsViewModel _storageViewModel;

      
        public TabStorageCommand(TabStorageWordsViewModel storageViewModel)
        {
            _storageViewModel = storageViewModel;
           
        }

        public async override void Execute(object parameter)
        {
            if(parameter is StorageContext)
            {
                StorageContext context = (StorageContext)parameter;
                _storageViewModel.launchContextWindow(context);
            }
            else if(parameter is WordMember)
            {
                WordMember wM = (WordMember)parameter;
                _storageViewModel.launchAddToCollectionWindow(wM);
            }else
            {
                string paramString = parameter.ToString();
                switch (paramString)
                {
                    case "next":
                        updatePage(paramString);
                        return;
                    case "prev":
                        updatePage(paramString);
                        return;
                }
            }



            //int index = Int32.Parse(paramString) ;
            //string[] words = WordServices.getAllAffliatedWords(index);
            //List<WordContext> wC = LangUtils.getAllMatchesOfWordContextsFromDir(words);


        }

        private void updatePage(string paramString)
        {
            _storageViewModel.StorageMemberModel.updateGrid(paramString);
            _storageViewModel.CurrentMembers = _storageViewModel.StorageMemberModel.CurrentMembers;
            _storageViewModel.PageNum = _storageViewModel.StorageMemberModel.CurrentPage.ToString();
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
    }
}
