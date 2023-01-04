using SubProgWPF.Commands;
using SubProgWPF.Commands.Learn.Tabs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn.Tabs
{
    public class TabAddExpressionViewModel : ViewModelBase
    {

        private ICommand _tabAddExpressionCommand;
        private string _expression;

        public TabAddExpressionViewModel()
        {
            _tabAddExpressionCommand = new TabAddExpressionCommand(this);
        }

        public string Expression { get => _expression; set { _expression = value; OnPropertyChanged(nameof(Expression)); } }

        public ICommand TabAddExpressionCommand { get => _tabAddExpressionCommand; set => _tabAddExpressionCommand = value; }

        public override void updateTheFields()
        {
        }
    }
}
