using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public class LeftPanelModel
    {
        private readonly List<LeftMenuItemModel> _leftMenuItems;

        public LeftPanelModel(List<LeftMenuItemModel> leftMenuItems)
        {
            _leftMenuItems = leftMenuItems;
        }
        public List<LeftMenuItemModel> LeftMenuItems => _leftMenuItems;
    }
}
