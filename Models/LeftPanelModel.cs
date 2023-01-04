using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public class LeftPanelModel
    {
        private List<LeftMenuItemModel> _leftMenuItems;

        public LeftPanelModel(List<LeftMenuItemModel> leftMenuItems)
        {
            _leftMenuItems = leftMenuItems;
        }

        public List<LeftMenuItemModel> LeftMenuItems { get => _leftMenuItems; set => _leftMenuItems = value; }
    }
}
