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
    public partial class EditWordWindow : Window
    {
        WordMember _wordMember;
        public EditWordWindow(WordMember wordMember)
        {
            _wordMember = wordMember;
            InitializeComponent();
            textBox_Name.Text = _wordMember.Word.Name;
            textBox_Meaning.Text = _wordMember.Word.Description;
            textBlock_initDate.Text = _wordMember.Word.InitDate.ToString();
            textBlock_differentForms.Text = getDifferentWordsString();
            itemsControlContexts.ItemsSource = _wordMember.Contexts;

            listView_examples.ItemsSource = _wordMember.Word.Example;
            itemsControl_repetition.ItemsSource = _wordMember.Word.Repetition;
            

        }

        private string getDifferentWordsString()
        {
            if(_wordMember.Word.WordInflections.Count == 0)
            {
                return "";
            }
            string text = "";
            for(int i = 0; i < _wordMember.Word.WordInflections.Count; i++)
            {
                WordData wordData = _wordMember.Word.WordInflections[i];
                text += wordData.Name + ",";
            }
            return text.Substring(0, text.Length - 1);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WordEditClick(object sender, RoutedEventArgs e)
        {
            int result = WordServices.editWordName(_wordMember.Word, textBox_Name.Text);
            if(result == 1)
            {
                resetValues();
            }
            Close();
        }





        private void ExampleClicked(object sender, RoutedEventArgs e)
        {
            Example ex = (Example)((Button)sender).DataContext;
            ShowExampleWindow window_example = new ShowExampleWindow(ex);
            window_example.Show();
        }
        private void MeaningEditClick(object sender, RoutedEventArgs e)
        {
            int result = WordServices.editWordMeaning(_wordMember.Word, textBox_Meaning.Text);
            if (result == 1)
            {
                resetValues();
            }
        }
        private void DifferentFormEditClick(object sender, RoutedEventArgs e)
        {
            EditWordInflectionsWindow window_inflections = new EditWordInflectionsWindow(_wordMember.Word);
            window_inflections.Show();
            //Close();
        }
        
        private void ExamplesEditClick(object sender, RoutedEventArgs e)
        {
            EditWordExamplesWindow window_examples = new EditWordExamplesWindow(_wordMember.Word);
            window_examples.Show();
            //Close();
        }

        private void ContextClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void resetValues()
        {
            _wordMember.Word = WordServices.getWordByID(_wordMember.Word.Id);
            textBox_Name.Text = _wordMember.Word.Name;
            textBox_Meaning.Text = _wordMember.Word.Description;
            textBlock_initDate.Text = _wordMember.Word.InitDate.ToString();
            textBlock_differentForms.Text = getDifferentWordsString();
            itemsControlContexts.ItemsSource = _wordMember.Contexts;
            itemsControl_repetition.ItemsSource = _wordMember.Word.Repetition;
        }

    }
}

