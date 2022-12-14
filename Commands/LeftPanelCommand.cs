using LangDataAccessLibrary.Services;
using SubProgWPF.ViewModels;


namespace SubProgWPF.Commands
{
    public class LeftPanelCommand : CommandBase
    {
        LeftPanelViewModel _viewModel;

        public LeftPanelCommand(LeftPanelViewModel viewModel)
        {
            _viewModel = viewModel;

        }

        public override void Execute(object parameter)
        {
            if(parameter is UserLanguage)
            {
                UserLanguage userLanguage = (UserLanguage)parameter;
                SettingServices.setUserLanguage(userLanguage.Name);
                _viewModel.updateTheFields();
                return;
            }
            
            _viewModel.switchTab((string)parameter);
        }

    }
}
