using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.ViewModels.Learning;
using SubProgWPF.Windows.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class DataGridNewWordsViewModel : ViewModelBase
    {
        ListWordsModel _dataGridNewWordModel;
        TabLearnViewModel _tabLearnViewModel;
        private ICommand _command;
        private string _alternativeName = "";
        private string _pageNumString = "1";
        private string _totalWordString;
        private int _lastLearnedWordIndex;
        private string _lastLearnedText;
        private ObservableCollection<MemberNewWord> _members;
        private bool _finishVisibility;
        private bool _isLoading = false;

        public DataGridNewWordsViewModel(ListWordsModel _dataGridModel, TabLearnViewModel tabLearnViewModel)
        {
            _dataGridNewWordModel = _dataGridModel;
            _tabLearnViewModel = tabLearnViewModel;
            _command = new TabListWordsCommand(this);
            _members = _dataGridModel.MembersModel.CurrentMembers;
            _lastLearnedWordIndex = TranscriptionServices.getTranscriptionPlayHeadByID(_tabLearnViewModel.SelectedTranscriptionId);
            _lastLearnedText = _lastLearnedWordIndex.ToString();
            FinishVisibility = false;

            _dataGridModel.MembersModel.Current_page = (_lastLearnedWordIndex / 50) + 1;
            _dataGridModel.MembersModel.updateCurrentMembers();
            _members = _dataGridModel.MembersModel.CurrentMembers;
            _pageNumString = _dataGridModel.MembersModel.Current_page.ToString();
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

        public bool FinishVisibility { get { return _finishVisibility; } set { _finishVisibility = value; OnPropertyChanged(nameof(FinishVisibility));}}
        public string PageNumString
        {
            get { return _pageNumString; }
            set { _pageNumString = value; OnPropertyChanged(nameof(PageNumString)); }
        }
        public MembersModel MembersModel { get => _dataGridNewWordModel.MembersModel;}
        public AddMediaModel AddMediaModel { get => _dataGridNewWordModel.AddMediaModel; }
        public string MediaName { get => _dataGridNewWordModel.AddMediaModel.MediaName; set => _dataGridNewWordModel.AddMediaModel.MediaName = value; }

        internal void RemoveItemFromCurrent(int index)
        {
            _dataGridNewWordModel.MembersModel.CurrentMembers.Remove(_dataGridNewWordModel.MembersModel.AllMembers[index]);
        }

        public string EpisodeIndex { get => _dataGridNewWordModel.AddMediaModel.EpisodeIndex; set => _dataGridNewWordModel.AddMediaModel.EpisodeIndex = value; }
        public string SeasonIndex { get => _dataGridNewWordModel.AddMediaModel.SeasonIndex; set => _dataGridNewWordModel.AddMediaModel.SeasonIndex = value; }
        public ListWordsModel DataGridNewWordModel { get => _dataGridNewWordModel; set => _dataGridNewWordModel = value; }

        internal void checkIfLastPage()
        {
            FinishVisibility = MembersModel.IsLastPage;
        }

        public string TotalWordString { 
            get { return _dataGridNewWordModel.MembersModel.TempWordList.Count.ToString(); } 
            set { _totalWordString = value; } }

        public TabLearnViewModel TabLearnViewModel { get => _tabLearnViewModel; set => _tabLearnViewModel = value; }
        public string LastLearnedText { get => _lastLearnedText; set { _lastLearnedText = value; OnPropertyChanged(nameof(LastLearnedText)); } }

        public int LastLearnedWordIndex { get => _lastLearnedWordIndex; set => _lastLearnedWordIndex = value; }
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }

        public void launchContextWindow(StorageContext context)
        {
            ShowContextsWindow contextsWindow = new ShowContextsWindow(context);
            contextsWindow.Show();
        }
        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }
        public void finishSession()
        {
            _tabLearnViewModel.switchToAddMediaView();
        }
    }
    
}
