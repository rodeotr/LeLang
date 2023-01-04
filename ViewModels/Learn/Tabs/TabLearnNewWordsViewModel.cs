using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.ViewModels.Learn;
using SubProgWPF.Windows.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn
{
    public class TabLearnNewWordsViewModel : ViewModelBase
    {
        private ListWordsModel _dataGridNewWordModel;
        private MenuLearnViewModel _tabLearnViewModel;
        private readonly MembersModel _membersModel;
        private readonly AddMediaModel _mediaModel;
        private ICommand _command;
        private int _lastLearnedWordIndex;
        private string _pageNumString = "1";
        private string _lastLearnedText;
        private bool _finishVisibility;
        private bool _isLoading = false;







        public TabLearnNewWordsViewModel(MembersModel membersModel, AddMediaModel mediaModel, MenuLearnViewModel tabLearnViewModel)
        {
            _membersModel = membersModel;
            _mediaModel = mediaModel;
            _tabLearnViewModel = tabLearnViewModel;
            _command = new TabLearnNewWordsCommand(this);

            setProperties();

            
        }

        private void setProperties()
        {
            _lastLearnedWordIndex = TranscriptionServices.getTranscriptionPlayHeadByID(_mediaModel.TranscriptionId);
            _lastLearnedText = "Last Learned Word Index: " + _lastLearnedWordIndex.ToString();
            _pageNumString = _membersModel.Current_page.ToString();

            _membersModel.setCurrentPage((_lastLearnedWordIndex / 50));
            OnPropertyChanged(nameof(Members));
            FinishVisibility = false;
        }

        public ICommand NewWordsCommand { get { return _command; }}
        public ObservableCollection<MemberNewWord> Members { get { return _membersModel.CurrentMembers; }
            set { _membersModel.CurrentMembers = value; OnPropertyChanged(nameof(Members)); }
        }


        public bool FinishVisibility { get { return _finishVisibility; } set { _finishVisibility = value; OnPropertyChanged(nameof(FinishVisibility));}}
        public string PageNumString
        {
            get { return _pageNumString; }
            set { _pageNumString = value; OnPropertyChanged(nameof(PageNumString)); }
        }
        public MembersModel MembersModel { get => _membersModel;}
        public AddMediaModel MediaModel { get => _mediaModel; }
        public string MediaName { get => _mediaModel.MediaName; set => _mediaModel.MediaName = value; }


        public ListWordsModel DataGridNewWordModel { get => _dataGridNewWordModel; set => _dataGridNewWordModel = value; }

        internal void checkIfLastPage()
        {
            FinishVisibility = MembersModel.IsLastPage;
        }

        public string TotalWordString { 
            get { return _membersModel.TotalWordCount.ToString(); }}

        public MenuLearnViewModel TabLearnViewModel { get => _tabLearnViewModel; set => _tabLearnViewModel = value; }
        public string LastLearnedText { get => _lastLearnedText; set { _lastLearnedText = value; OnPropertyChanged(nameof(LastLearnedText)); } }

        public int LastLearnedWordIndex { get => _lastLearnedWordIndex; set => _lastLearnedWordIndex = value; }
        public bool IsLoading { get => _isLoading; set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); } }

        public int TranscriptionId { get => _mediaModel.TranscriptionId; set => _mediaModel.TranscriptionId = value; }

        public void launchContextWindow(StorageContext context)
        {
            ShowContextsWindow contextsWindow = new ShowContextsWindow(context);
            contextsWindow.Show();
        }
        public void OnMembersChanged()
        {
            OnPropertyChanged(nameof(Members));
        }
        public override void updateTheFields()
        {
        }
        public void finishSession()
        {
            _tabLearnViewModel.switchToAddMediaView();
        }
    }
    
}
