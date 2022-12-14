using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.ViewModels.MenuTabs
{
    public interface IDashItem
    {
        public string Title { get;}
        public string TotalElementCount { get; }
        public string TotalLearnedWords { get; }

    }
}
