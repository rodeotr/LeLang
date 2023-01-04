using SubProgWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn.Tabs.AddMediaOptions
{
    public class TabAddMediaYoutubeViewModel : ViewModelBase
    {

        private string _link;

        public TabAddMediaYoutubeViewModel(){}

        public string Link { get => _link; set => _link = value; }

        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }
    }
}
