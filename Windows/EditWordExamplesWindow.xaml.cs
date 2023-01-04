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
using LangDataAccessLibrary.Services;

namespace SubProgWPF.Windows
{
    /// <summary>
    /// Interaction logic for ShowContextsWindow.xaml
    /// </summary>
    public partial class EditWordExamplesWindow : Window
    {
        Word _word;
        public EditWordExamplesWindow(Word word)
        {
            _word = word;
            InitializeComponent();
            textBlock_wordName.Text = word.Name;
            listView_examples.ItemsSource = word.Example;


        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WordAddClick(object sender, RoutedEventArgs e)
        {
            if(textBox_addExample.Text.Length != 0)
            {
                int result = WordServices.addExample(_word, textBox_addExample.Text);
                if(result == 1)
                {
                    textBox_addExample.Text = "";
                    _word = WordServices.getWordByID(_word.Id);
                    listView_examples.ItemsSource = _word.Example;
                }
            }
        }
        private void MeaningEditClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void DifferentFormEditClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void DeleteExample(object sender, RoutedEventArgs e)
        {
            Example ex = (Example)((Button)sender).DataContext;
            int result = WordServices.deleteWordExample(ex.Id);
            if (result == 1)
            {
                _word = WordServices.getWordByID(_word.Id);
                listView_examples.ItemsSource = _word.Example;
            }
        }

    }
}

