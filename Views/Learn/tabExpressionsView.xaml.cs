﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SubProgWPF.Views.Learn
{
    /// <summary>
    /// Interaction logic for tabContinueMediaView.xaml
    /// </summary>
    public partial class tabExpressionsView : UserControl
    {
        //private Regex _regex;
        public tabExpressionsView()
        {
            InitializeComponent();
        }
        //private void PreviewTextInputMaxFreq(object sender, TextCompositionEventArgs e)
        //{
        //    //Console.WriteLine(e.Text);
        //    e.Handled = !IsTextAllowed(e.Text);
        //}
        //private bool IsTextAllowed(string text)
        //{
        //    _regex = new Regex("[^0-9.-]+");
        //    return !_regex.IsMatch(text);
        //}
    }
}
