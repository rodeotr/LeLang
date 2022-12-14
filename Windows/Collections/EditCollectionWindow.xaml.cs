using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LangDataAccessLibrary.Models;
using SubProgWPF.ViewModels;
using LangDataAccessLibrary.ServerServices;
using SubProgWPF.ViewModels.Collections;
using SubProgWPF.Utils;

namespace SubProgWPF.Windows.Collections
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class EditCollectionWindow : Window
    {
        CollectionModel _collection;
        public EditCollectionWindow(CollectionModel collection)
        {
            InitializeComponent();
            _collection = collection;
            this.DataContext = collection;
           
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void PublishToServer(object sender, RoutedEventArgs e)
        {
            ServerUtils.publishCollectionToServer(_collection.Name);
        }

    }
}

