using LangDataAccessLibrary;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Collections;
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

namespace SubProgWPF.Windows.Collections
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class CreateCollectionWindow : Window
    {
        private TabCollectionsViewModel _vM;
        public CreateCollectionWindow(TabCollectionsViewModel vM)
        {
            InitializeComponent();
            _vM = vM;
            this.DataContext = this;
            collection.ItemsSource = Enum.GetValues(typeof(CollectionTypes.TYPE)).Cast<CollectionTypes.TYPE>();
            medium.ItemsSource = Enum.GetValues(typeof(MediaTypes.TYPE));


        }
        
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Create_Down(object sender, RoutedEventArgs e)
        {
            _vM.createCollection(word.Text, collection.SelectedItem.ToString(),medium.SelectedItem.ToString());
            ScoreServices.IncrementScoreCollectionAdding();
            Close();
        }
    }
}
