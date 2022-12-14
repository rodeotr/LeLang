using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.ViewModels.MenuTabs
{
    class TVEpisode : IDashItem
    {
        public TVEpisode()
        {
            Title = "Episodes";
        }

        public string Title { get; }
        public string TotalElementCount { get; }
        public string TotalLearnedWords { get; }
    }
}
