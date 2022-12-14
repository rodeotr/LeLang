using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class TabStorageExpressionsViewModel : ViewModelBase
    {
        private ObservableCollection<Models.WordMember> _members;
        private ObservableCollection<Models.WordMember> _currentMembers;
        private StorageExpressionsModel _membersModel;
        private bool _expressionVisibility;
        private string _pageNum;

        
        private ICommand _command;
        public ICommand GridCommand { get { return _command; }}
        public StorageExpressionsModel StorageMemberModel { get => _membersModel; set => _membersModel = value; }
        public ObservableCollection<WordMember> CurrentMembers { get => _currentMembers; set => _currentMembers = value; }
        public string PageNum { get => _pageNum; set { _pageNum = value; OnPropertyChanged(nameof(PageNum)); } }
        public bool ExpressionVisibility { get => _expressionVisibility; set => _expressionVisibility = value; }

        public TabStorageExpressionsViewModel(StorageExpressionsModel memberModel)
        {
            _membersModel = memberModel;
            _currentMembers = _membersModel.CurrentMembers;
            _expressionVisibility = _currentMembers.Count == 0 ? false : true;
        }

        public override void updateTheFields()
        {
            _membersModel = new StorageExpressionsModel(WordServices.getAllExpressions());
            _currentMembers = _membersModel.CurrentMembers;
        }
    }
}
