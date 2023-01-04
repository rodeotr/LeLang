using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Settings;
using SubProgWPF.Models;
using SubProgWPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Settings
{
    public class MenuSettingsViewModel : ViewModelBase
    {

        private User _currentUser;
        public Language _currentLanguage;
        MainViewModel _mainViewModel;
        private int _selectedLanguageIndex;
        private string _currentUserString;
        private string _selectedLanguage;
        private string _currentLanguageString;
        private string[] _usersList;
        private string[] _languageArray;
        private string[] _allLanguages;
        private ICommand _command;
        SoundPlayer player;




        public ICommand SettingCommand { get { return _command; }}

        public string[] UsersList { get => _usersList; set { _usersList = value; OnPropertyChanged(nameof(UsersList)); } }

        public string CurrentUserString { get => _currentUserString; set => _currentUserString = value; }

        

        public string CurrentLanguageString { get => _currentLanguage.Name; set => _currentUserString = value; }
        public string[] LanguageList { get => _languageArray; set => _languageArray = value; }
        public ICommand Command { get => _command; set => _command = value; }
        public string SelectedLanguage { get => _selectedLanguage; set { _selectedLanguage = value; OnPropertyChanged(nameof(SelectedLanguage)); } }

        public int SelectedLanguageIndex { get => _selectedLanguageIndex; set => _selectedLanguageIndex = value; }
        public MainViewModel MainViewModel { get => _mainViewModel; set => _mainViewModel = value; }
        public User CurrentUser { get => _currentUser; set { _currentUser = value; OnPropertyChanged(nameof(CurrentUser)); } }

        public MenuSettingsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _command = new TabSettingsCommand(this);

            updateTheFields();
            loadSoundFile();
            
            
        }

        private void loadSoundFile()
        {
            player = new SoundPlayer();
            player.Stream = SubProgWPF.Properties.Resources.click;
        }

        public void playClick()
        {
            player.Play();
        }

        public override void updateTheFields()
        {
            List<string> userStringList = new List<string>();
            List<string> languageList = new List<string>();
            List<User> users = SettingServices.getAllUsers();
            List<Language> languages = SettingServices.getAllLanguages();
            _allLanguages = languages.Select(a => new string(a.Name)).ToArray();

            foreach (User user in users)
            {
                userStringList.Add(user.Name);
            }
            foreach (Language language in languages)
            {
                languageList.Add(language.Name);
            }
            _usersList = userStringList.ToArray();
            _currentUser = SettingServices.getCurrentUser();
            _languageArray = _allLanguages;
            //_languageArray = _currentUser.Languages.Select(a => new string(a.Name)).ToArray();
            _currentLanguage = SettingServices.getCurrentLanguage();
            _selectedLanguage = _currentLanguage.Name;
            

            OnPropertyChanged(nameof(UsersList));
            OnPropertyChanged(nameof(LanguageList));
        }
        internal void launchNewLanguageWindow()
        {
            string[] copy = new string[_allLanguages.Length];
            _allLanguages.CopyTo(copy,0);
            
            AddLanguageWindow window = new AddLanguageWindow(copy.Except(LanguageList).ToArray(), this);
            window.Show();
        }

    }
}
