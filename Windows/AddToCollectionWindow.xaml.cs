using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Storage;
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
            if(collection.Text.Length != 0)
            {
                int result = CollectionServices.addWordToCollection(collection.SelectedItem.ToString(), _wm.Name);
                if(result == -1)
                {
                    MessageBox.Show("Collection Type Mismatch.");
                }
                else{
                    IHost _hostMain = (IHost)App.Current.Properties["MainViewModelHost"];
                    MenuStorageMainViewModel vM = _hostMain.Services.GetRequiredService<MenuStorageMainViewModel>();
                    MenuCollectionsMainViewModel vM_collections = _hostMain.Services.GetRequiredService<MenuCollectionsMainViewModel>();
                    vM_collections.TabcollectionsViewModel.updateTheFields();
                    vM.TabStorageWordsViewModel.Refresh();
                    vM.TabStorageWordsViewModel.raisePropertyChangedEvent(nameof(vM.TabStorageWordsViewModel.CurrentMembers));
                }
            }
            Close();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
     
    }
}
