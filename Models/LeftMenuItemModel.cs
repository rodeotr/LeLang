using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public class LeftMenuItemModel
    {
        private readonly string _itemLogoName;
        private readonly string _itemName;

        public LeftMenuItemModel(string itemLogoName, string itemName)
        {
            _itemLogoName = itemLogoName;
            _itemName = itemName;
        }

        public string ItemLogoName => _itemLogoName;
        public string ItemName => _itemName;
    }
}
