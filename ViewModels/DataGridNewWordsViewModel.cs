using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class DataGridNewWordsViewModel : ViewModelBase
    {
        DataGridNewWordModel _dataGridNewWordModel;
        private ICommand _command;
        private string _alternativeName = "";
        private string _pageNum = "1";
        private ObservableCollection<MemberNewWord> _members;

        public DataGridNewWordsViewModel(DataGridNewWordModel _dataGridModel)
        {
            _dataGridNewWordModel = _dataGridModel;
            _command = new DataGridNewWordsCommand(this);
            _members = _dataGridModel.MembersModel.GetCurrentGridItems();

        }

        public ICommand GridCommand { get { return _command; }}
        public ObservableCollection<MemberNewWord> Members { get { return _members; }
            set { _members = value; OnPropertyChanged(nameof(Members)); }
        }

        public string Operation { get => "0"; }
        public string AlternativeName
        {
            get { return _alternativeName; }
            set { _alternativeName = value; OnPropertyChanged(nameof(AlternativeName)); }
        }

        public string ScanSourceLocation
        {
            get { return _dataGridNewWordModel.AddMediaModel.TranscriptionLocation; }
            set { _dataGridNewWordModel.AddMediaModel.TranscriptionLocation = value; } 
        }

        public string PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; OnPropertyChanged(nameof(PageNum)); }
        }
        public MembersModel MembersModel { get => _dataGridNewWordModel.MembersModel;}
        public string MediaName { get => _dataGridNewWordModel.AddMediaModel.MediaName; set => _dataGridNewWordModel.AddMediaModel.MediaName = value; }

        internal void RemoveItemFromCurrent(int index)
        {
            _dataGridNewWordModel.MembersModel.GetCurrentGridItems().Remove(_dataGridNewWordModel.MembersModel.GetAllMembers()[index]);
        }

        public string EpisodeIndex { get => _dataGridNewWordModel.AddMediaModel.EpisodeIndex; set => _dataGridNewWordModel.AddMediaModel.EpisodeIndex = value; }
        public string SeasonIndex { get => _dataGridNewWordModel.AddMediaModel.SeasonIndex; set => _dataGridNewWordModel.AddMediaModel.SeasonIndex = value; }
        public DataGridNewWordModel DataGridNewWordModel { get => _dataGridNewWordModel; set => _dataGridNewWordModel = value; }
    }
    
}
