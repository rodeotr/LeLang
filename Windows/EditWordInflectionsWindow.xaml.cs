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
    public partial class EditWordInflectionsWindow : Window
    {
        Word _word;
        public EditWordInflectionsWindow(Word word)
        {
            _word = word;
            InitializeComponent();
            textBlock_wordName.Text = word.Name;
            listView_inflections.ItemsSource = word.WordInflections;


        }
        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WordAddClick(object sender, RoutedEventArgs e)
        {
            if(textBox_addWord.Text.Length != 0)
            {
                int result = WordServices.addInflectionWord(_word, textBox_addWord.Text);
                if(result == 1)
                {
                    _word = WordServices.getWordByID(_word.Id);
                    listView_inflections.ItemsSource = _word.WordInflections;
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
        
        private void DeleteWord(object sender, RoutedEventArgs e)
        {
            WordData wD = (WordData)((Button)sender).DataContext;
            int result = WordServices.deleteWordInflection(wD.Id);
            if (result == 1)
            {
                _word = WordServices.getWordByID(_word.Id);
                listView_inflections.ItemsSource = _word.WordInflections;
            }
        }

    }
}

