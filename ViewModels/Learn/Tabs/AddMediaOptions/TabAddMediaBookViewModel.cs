using SubProgWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn.Tabs.AddMediaOptions
{
    public class TabAddMediaBookViewModel : ViewModelBase
    {

        private string _startPage;
        private string _endPageOfSection;

        public TabAddMediaBookViewModel(){}

        public string StartPage { get => _startPage; set => _startPage = value; }
        public string EndPageOfSection { get => _endPageOfSection; set => _endPageOfSection = value; }

        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }
    }
}
