using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands.Test
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
            if(parameter is TestMediaModel)
            {
                TestMediaModel t = (TestMediaModel)parameter;
                _tabTestViewModel.showContext(t);
                return;
            }

            string paramString = parameter.ToString();
            bool IsSuccess = false;

            switch (paramString)
            {
                case "Meaning":
                    getMeaning();
                    return;
                case "ShowImage":
                    showImage();
                    return;
                case "PromptWarning":
                    promptWarning();
                    return;
                case "YES":
                    IsSuccess = true;
                    break;
                case "NO":
                    IsSuccess = false;
                    break;

            }

            _tabTestViewModel.executeAnswer(IsSuccess);
            
        }

        private void promptWarning()
        {
            _tabTestViewModel.promptWarning();
        }

        private void showImage()
        {
            string wordStr = _tabTestViewModel.TestModel.WordsToBeTested[_tabTestViewModel.Index].Name;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.google.com/search?hl=en&site=imghp&tbm=isch&source=hp&q=" + wordStr,
                UseShellExecute = true
            });
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
