using SubProgWPF.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn.Tabs.AddMediaOptions
{
    public class TabAddMediaTVSeriesViewModel : ViewModelBase
    {

        private string _episodeIndex;
        private string _seasonIndex;

        public TabAddMediaTVSeriesViewModel(){}

        public string EpisodeIndex { get => _episodeIndex; set => _episodeIndex = value; }
        public string SeasonIndex { get => _seasonIndex; set => _seasonIndex = value; }

        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }
    }
}
