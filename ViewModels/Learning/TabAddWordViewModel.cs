using SubProgWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learning
{
    public class TabAddWordViewModel : ViewModelBase
    {
        private string _word;
        private ICommand _tabAddWordCommand;

        public TabAddWordViewModel()
        {
            _tabAddWordCommand = new TabAddWordCommand(this);
        }

        public override void updateTheFields()
        {
            
        }
        public string Word { get => _word; set { _word = value; OnPropertyChanged(nameof(Word)); } }

        public ICommand TabAddWordCommand { get => _tabAddWordCommand; set => _tabAddWordCommand = value; }
    }
}
