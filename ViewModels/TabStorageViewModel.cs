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
    public class TabStorageViewModel : ViewModelBase
    {
        private ObservableCollection<Models.WordMember> _members;
        private StorageMemberModel _membersModel;

        public StorageMemberModel StorageMemberModel { get; }
        public ObservableCollection<WordMember> CurrentMembers { get { return StorageMemberModel.GetCurrentGridItems(); } set { OnPropertyChanged(nameof(CurrentMembers)); } }

        private ICommand _command;

        
        private string _mediaName;
        private string _pageNum;
        
        List<Word> words;
        private string _selectedName = "Enter Word";

        public ObservableCollection<WordMember> WordName { get { return _members; } }
        public string SelectedName { get => _selectedName; set { _selectedName = value; OnPropertyChanged(nameof(SelectedName)); } }

        public ICommand GridCommand { get { return _command; }}
        public ObservableCollection<Models.WordMember> Members { get { return _members; }
            set { _members = value; OnPropertyChanged(nameof(Members)); }
        }


        public string PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; OnPropertyChanged(nameof(PageNum)); }
        }
        //public StorageMemberModel MembersModel { get => _membersModel;}
        public string MediaName { get => _mediaName; set => _mediaName = value; }


        public TabStorageViewModel(StorageMemberModel _storageMemberModel)
        {
            StorageMemberModel = _storageMemberModel;
            _command = new TabStorageCommand(this);
            words = WordServices.getAllWords();
        }

        
    }
}
