using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabTestCommand : CommandBase
    {
        private TabTestViewModel _tabTestViewModel;

        public TabTestCommand(TabTestViewModel tabTestViewModel)
        {
            _tabTestViewModel = tabTestViewModel;
        }

        public override void Execute(object parameter)
        {
            if (parameter.ToString().Equals("Meaning"))
            {
                getMeaning();
                return;
            }
            string IsSuccessStr = parameter.ToString();
            bool IsSuccess = false;
            if (IsSuccessStr.Equals("YES")) { IsSuccess = true; }else if (IsSuccessStr.Equals("No")) { IsSuccess = false; } else { return; }
            Repetition repetition = new Repetition();
            repetition.Time = DateTime.Now;
            repetition.Success = IsSuccess;
            WordServices.AddRepetition(_tabTestViewModel.TestModel.WordsToBeTested[_tabTestViewModel.Index].WordDBId, repetition);
            //Console.WriteLine(_tabTestViewModel.AllWords[_tabTestViewModel.Index].WordContexts[0].Content);
            //Console.WriteLine("\n");
            _tabTestViewModel.SelectedSourceIndex = -1;
            _tabTestViewModel.Index = _tabTestViewModel.TotalWordCount - 1 > _tabTestViewModel.Index ?
                            _tabTestViewModel.Index + 1 : _tabTestViewModel.Index;
            _tabTestViewModel.RemainingWordCount = _tabTestViewModel.RemainingWordCount - 1;
            //Console.WriteLine(_tabTestViewModel.RemainingWordCount);

        }

        private void getMeaning()
        {
            string wordStr = _tabTestViewModel.TestModel.WordsToBeTested[_tabTestViewModel.Index].Name;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.google.com/search?q=" + wordStr + " meaning",
                UseShellExecute = true
            });
        }
    }
}
