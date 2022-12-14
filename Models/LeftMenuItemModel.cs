using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public class LeftMenuItemModel
    {
        private readonly string _itemLogoName;
        private readonly string _itemName;
        private readonly ViewModelBase _vM;


        public LeftMenuItemModel(string itemLogoName, string itemName, ViewModelBase vM)
        {
            _vM = vM;
            _itemLogoName = itemLogoName;
            _itemName = itemName;
        }

        public string ItemLogoName => _itemLogoName;
        public string ItemName => _itemName;
        public ViewModelBase VM => _vM;
    }
}
