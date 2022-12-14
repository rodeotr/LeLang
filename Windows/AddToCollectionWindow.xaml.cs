using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
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
    public partial class AddToCollectionWindow : Window
    {
        WordMember _wm;
        public AddToCollectionWindow(WordMember wM, List<LangDataAccessLibrary.Models.Collections> collections)
        {
            InitializeComponent();
            _wm = wM;
            this.DataContext = this;
            collection.ItemsSource = collections.Select(s=>s.Name).ToList();


        }

        private void AddButtonDown(object sender, MouseButtonEventArgs e)
        {
            CollectionServices.addWordToCollection(collection.SelectedItem.ToString(), _wm.Name);
            Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
    }
}
