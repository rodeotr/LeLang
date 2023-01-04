using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learn;
using SubProgWPF.ViewModels.Learn.Tabs;
using SubProgWPF.ViewModels.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SubProgWPF.Commands.Learn.Tabs
{
    public class TabAddWordCommand : CommandBase
    {
        TabAddWordViewModel _vm;
        public TabAddWordCommand(TabAddWordViewModel vm)
        {
            _vm = vm;
        }

        public override async void Execute(object parameter)
        {
            if(_vm.Word.Length > 0)
            {
                createWordAsync();
                _vm.Word = "";
                IHost _hostMain = (IHost)App.Current.Properties["MainViewModelHost"];
                MenuStorageMainViewModel vM = _hostMain.Services.GetRequiredService<MenuStorageMainViewModel>();
                vM.TabStorageWordsViewModel.Refresh();
            }


        }

        private int createWordAsync()
        {
            Word word = new Word()
            {
                Name = _vm.Word,
                TypeOfLearnedMedium = MediaTypes.TYPE.Random.ToString(),
                Contexts = new List<WordContext>()
            };
            //return await WordCreator.CreateWord(word);
            return WordCreator.CreateWord(word);

            //if(result == -1)
            //{
            //    MessageBox.Show("The word already exists.");
            //}
        }
    }
}
