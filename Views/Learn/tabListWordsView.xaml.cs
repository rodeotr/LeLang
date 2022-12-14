using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace SubProgWPF.Views.Learn
{
    /// <summary>
    /// Interaction logic for tabListWords.xaml
    /// </summary>
    public partial class tabListWordsView : UserControl
    {
        ScrollViewer scroll;
        public tabListWordsView()
        {
            InitializeComponent();
            
            

        }


        public static ScrollViewer GetScrollViewer(UIElement element)
        {
            if (element == null) return null;

            ScrollViewer retour = null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element) && retour == null; i++)
            {
                if (VisualTreeHelper.GetChild(element, i) is ScrollViewer)
                {
                    retour = (ScrollViewer)(VisualTreeHelper.GetChild(element, i));
                }
                else
                {
                    retour = GetScrollViewer(VisualTreeHelper.GetChild(element, i) as UIElement);
                }
            }
            return retour;
        }
        public void pageClicked(object sender, RoutedEventArgs e)
        {
            if(scroll == null)
            {
                scroll = GetScrollViewer(membersDataGrid);
            }
            scroll.ScrollToTop();
        }



    }
}
