using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace SubProgWPF.Views.LeftPanel
{
    public class ContextConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Type t = values[0] as Type;
            
            String s = values[1] as String;
            Console.WriteLine(values[0]);
            Console.WriteLine(values[1]);
            return values[0];
            //return new Tuple<StorageContext, string>((StorageContext)values[0], "dsa");
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
