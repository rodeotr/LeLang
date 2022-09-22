using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public class LeftPanelModel
    {
        private readonly string _appLogoName;
        private readonly List<LeftMenuItemModel> _leftMenuItems;

        public LeftPanelModel(string appLogoName, List<LeftMenuItemModel> leftMenuItems)
        {
            _leftMenuItems = leftMenuItems;
            _appLogoName = appLogoName;
        }

        public List<LeftMenuItemModel> LeftMenuItems => _leftMenuItems;
    }
}
