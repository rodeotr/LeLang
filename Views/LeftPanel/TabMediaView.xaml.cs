using Microsoft.Win32;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Media;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubProgWPF.Views.LeftPanel
{
    /// <summary>
    /// Interaction logic for TabMediaView.xaml
    /// </summary>
    public partial class TabMediaView : UserControl
    {
        public TabMediaView()
        {
            InitializeComponent();
        }
        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            { 
                //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
                string myValue = ((Button)sender).Tag.ToString();
                ((MenuMediaViewModel)(this.DataContext)).SelectedIndex = myValue;
                ((MenuMediaViewModel)(this.DataContext)).MediaLocation = openFileDialog.FileName;
            }
        }
    }
}
