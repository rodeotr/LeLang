using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using Microsoft.EntityFrameworkCore;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace SubProgWPF.Commands
{
    public class TabStorageCommand : CommandBase
    {
        private readonly TabStorageViewModel _storageViewModel;

      
        public TabStorageCommand(TabStorageViewModel storageViewModel)
        {
            _storageViewModel = storageViewModel;
           
        }

        public async override void Execute(object parameter)
        {
           
            
            if(IsNumeric(parameter.ToString())){
                int index = Int32.Parse(parameter.ToString()) ;
                string[] aa = WordServices.getAllAffliatedWords(index);
                List<WordContext> wC = LangUtils.getAllMatchesOfWordContextsFromDir(aa);
                Console.WriteLine();
                

            }
            else
            {
                _storageViewModel.StorageMemberModel.updateGrid(parameter.ToString());
                _storageViewModel.CurrentMembers = _storageViewModel.StorageMemberModel.GetCurrentGridItems();
                _storageViewModel.PageNum = _storageViewModel.StorageMemberModel.Current_page.ToString();
                
            }
            
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
    }
}
