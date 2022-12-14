using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learning;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SubProgWPF.Commands
{
    public class TabAddWordCommand : CommandBase
    {
        TabAddWordViewModel _vm;
        public TabAddWordCommand(TabAddWordViewModel vm)
        {
            _vm = vm;
        }

        public override void Execute(object parameter)
        {
            if(_vm.Word.Length > 0)
            {
                createWord();
                _vm.Word = "";
            }
            
            
        }

        private void createWord()
        {
            Word word = new Word()
            {
                Name = _vm.Word,
                TypeOfLearnedMedium = MediaTypes.TYPE.Random.ToString(),
                WordContext_Ids = ""
            };
            int result = WordCreator.CreateWord(word);
            if(result == -1)
            {
                MessageBox.Show("The word already exists.");
            }
        }
    }
}
