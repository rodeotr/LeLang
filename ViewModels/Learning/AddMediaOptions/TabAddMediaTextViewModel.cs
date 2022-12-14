using SubProgWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learning.AddMediaOptions
{
    public class TabAddMediaTextViewModel : ViewModelBase
    {

        private string _text;

        public TabAddMediaTextViewModel(){}

        public string Text { get => _text; set { _text = value; OnPropertyChanged(nameof(Text)); } }

        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }
    }
}
