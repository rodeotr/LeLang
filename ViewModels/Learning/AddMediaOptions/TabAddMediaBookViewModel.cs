using SubProgWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learning.AddMediaOptions
{
    public class TabAddMediaBookViewModel : ViewModelBase
    {

        private string _pagePerSection;

        public TabAddMediaBookViewModel(){}

        public string PagePerSection { get => _pagePerSection; set => _pagePerSection = value; }

        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }
    }
}
