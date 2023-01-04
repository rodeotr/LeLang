using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Commands.Settings
{
    public class TabSettingsCommand : CommandBase
    {
        private MenuSettingsViewModel _tabSettingsViewModel;

        public TabSettingsCommand(MenuSettingsViewModel tabSettingsViewModel)
        {
            _tabSettingsViewModel = tabSettingsViewModel;
        }

        public override void Execute(object parameter)
        {
            _tabSettingsViewModel.playClick();
            string paramString = parameter.ToString();
            switch (paramString)
            {
                case "Save":
                    SettingServices.setUserLanguage(_tabSettingsViewModel.SelectedLanguage);
                    _tabSettingsViewModel.MainViewModel.updateTheFields();
                    break;
                case "AddLanguage":
                    _tabSettingsViewModel.launchNewLanguageWindow();
                    break;
            }
            
            
        }
    }
}
