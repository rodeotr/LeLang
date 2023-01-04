using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.ViewModels.Test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SubProgWPF.ViewModels
{
    public class LeftPanelViewModel : ViewModelBase
    {
        private readonly ICommand _leftPanelCommand;
        private readonly MainViewModel _mainViewModel;
        private readonly MenuTestDashViewModel _tabTestDashViewModel;
        private ObservableCollection<UserLanguage> _languages;
        private UserInfo _userInfo;
        private LeftPanelModel _leftPanel;
        private string _avatarPath;
        private string _languageSymbolPath;
        private string _testWordCount = "0";
        private bool _testVisibility;
        
        
        public ICommand LeftPanelCommand => _leftPanelCommand;
        public string AvatarPath { get => _avatarPath; set { _avatarPath = value; OnPropertyChanged(nameof(AvatarPath)); } }
        public string LanguageSymbolPath { get => _languageSymbolPath; set { _languageSymbolPath = value; OnPropertyChanged(nameof(LanguageSymbolPath)); } }

        public bool TestVisibility { get => _testVisibility; set { _testVisibility = value; OnPropertyChanged(nameof(TestVisibility)); } }
        public string TestWordCount { get => _testWordCount; set { _testWordCount = value; OnPropertyChanged(nameof(TestWordCount)); } }

        public LeftPanelModel LeftPanel => _leftPanel;

        public ObservableCollection<UserLanguage> Languages { get => _languages; set { _languages = value; OnPropertyChanged(nameof(Languages)); } }

        public UserInfo UserInfo { get => _userInfo; set { _userInfo = value; OnPropertyChanged(nameof(UserInfo)); } }

        public LeftPanelViewModel(LeftPanelModel leftPanel, MainViewModel mainViewModel)
        {
            IHost mainHost = (IHost)App.Current.Properties["MainViewModelHost"];

            _mainViewModel = mainViewModel;
            TestVisibility = Int32.Parse(_testWordCount) > 0;
            _leftPanel = leftPanel;
            _leftPanelCommand = new LeftPanelCommand(this);

            
            updateTheFields();
        }
        public void updateTestWordCount(int count)
        {
            TestWordCount = count.ToString();
            TestVisibility = Int32.Parse(_testWordCount) > 0;
        }

        private void setUserInfo()
        {
            string _avatarprefix = "/Assets/Images/Avatars/";
            User _user = SettingServices.getCurrentUser();
            _userInfo = new UserInfo
            {
                Name = _user.Name,
                AvatarSource = _user.Gender.Equals("M") ?
                _avatarprefix + "Male/male1.png" :
                _avatarprefix + "Female/female1.png"
            };

        }

        private void setLanguages()
        {
            Language _currentLanguage = SettingServices.getCurrentLanguage();
            _languages = new ObservableCollection<UserLanguage>();
            string _imageSourcePrefix = "/Assets/Images/LanguageFlags/";
            List<Language> _allLanguages = SettingServices.getCurrentUser().Languages;
            foreach(Language l in _allLanguages)
            {
                double opacity = l.Name.Equals(_currentLanguage.Name) ? 1.0 : 0.5;
                UserLanguage userLanguage = new UserLanguage()
                {
                    Name = l.Name,
                    ImageSource = _imageSourcePrefix + l.LangCode.ToLower() + ".png",
                    Opacity = opacity
                };
                _languages.Add(userLanguage);
            }

        }

        public void switchTab(string param)
        {
            _mainViewModel.switchTab(param);
        }

        public override void updateTheFields()
        {
            setUserInfo();
            setLanguages();

            User user = SettingServices.getCurrentUser();
            Language language = SettingServices.getCurrentLanguage();
            string langCode = language.LangCode.ToLower();
            bool isMale = user.Gender.Equals("M") ? true : false;
            _languageSymbolPath = "/Assets/Images/LanguageFlags/" + langCode + ".png";
            _avatarPath = "/Assets/Images/Avatars/";
            _avatarPath += isMale ? "Male/male1.png" : "Female/female1.png";
            LanguageSymbolPath = _languageSymbolPath;

            //TestVisibility = Int32.Parse(TestWordCount) > 0;

            OnPropertyChanged(nameof(Languages));
            OnPropertyChanged(nameof(UserInfo));
        }
        public void LeftPanelChanged()
        {
            OnPropertyChanged(nameof(LeftPanel));
        }
    }
    public class UserLanguage
    {
        public string Name { get; set; }
        public string ImageSource { get; set; }
        public double Opacity { get; set; }
    }
    public class UserInfo
    {
        public string Name { get; set; }
        public string AvatarSource { get; set; }
    }
    
}
