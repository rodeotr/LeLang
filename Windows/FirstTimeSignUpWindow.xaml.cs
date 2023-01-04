using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SubProgWPF.Windows
{
    /// <summary>
    /// Interaction logic for FirstTimeSignUpWindow.xaml
    /// </summary>
    public partial class FirstTimeSignUpWindow : Window
    {
        private readonly App _app;
        private bool _maleActive = true;
        public FirstTimeSignUpWindow(App app)
        {
            InitializeComponent();
            _app = app;
            setLanguageSources();
        }

        private void setLanguageSources()
        {
            string[] languageArray = SettingServices.getAllLanguages().Select(a => a.Name).ToArray();
            LanguageComboBox.ItemsSource = languageArray;
            LanguageToLearnComboBox.ItemsSource = languageArray;
        }

        private void maleClicked(object sender, MouseButtonEventArgs e)
        {
            if (!_maleActive)
            {
                maleAvatar.Opacity = 1;
                femaleAvatar.Opacity = 0.5;
                _maleActive = true;
            }
        }

        private void femaleClicked(object sender, MouseButtonEventArgs e)
        {
            if (_maleActive)
            {
                maleAvatar.Opacity = 0.5;
                femaleAvatar.Opacity = 1;
                _maleActive = false;
            }
        }

        private void Save_MouseDowned(object sender, MouseButtonEventArgs e)
        {
            if(NameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please Enter Your Name");
                return;
            }

            string gender = _maleActive ? "M" : "F";
            IHost host = (IHost)App.Current.Properties["AppHost"];
            IAppServices appServices = (IAppServices)host.Services.GetRequiredService<IAppServices>();


            Language mainLanguage = SettingServices.getLanguageByString(LanguageComboBox.SelectedItem.ToString());
            Language toLearnLanguage = SettingServices.getLanguageByString(LanguageToLearnComboBox.SelectedItem.ToString());
            User user = new User()
            {
                Name = NameTextBox.Text,
                MotherLanguage = mainLanguage,
                CurrentLanguage = toLearnLanguage,
                Gender = gender,
                Score = new Score()
            };



            //appServices.initializeFirstTimeObjects(
            //    NameTextBox.Text,
            //    LanguageComboBox.SelectedItem.ToString(),
            //    LanguageToLearnComboBox.SelectedItem.ToString(),
            //    gender);
            appServices.addUserToDB(user);

            _app.launchMainWindow();
            Close();
        }
    }
    

}
