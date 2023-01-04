using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SubProgWPF.Windows
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class AddLanguageWindow : Window
    {
        MenuSettingsViewModel _vM;
        public AddLanguageWindow(string[] _languages,MenuSettingsViewModel vM)
        {
            InitializeComponent();
            _vM = vM;
            comboLanguages.ItemsSource = _languages;
            this.DataContext = this;



        }

        private void AddButtonDown(object sender, MouseButtonEventArgs e)
        {
            SettingServices.AddLanguageToCurrentUser(comboLanguages.SelectedItem.ToString());
            _vM.updateTheFields();
            ScoreServices.IncrementScoreLanguageAdding();
            Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
    }
}
