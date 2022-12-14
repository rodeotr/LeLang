using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Linq;
using FFMpegCore;
using System.Collections.ObjectModel;
using LangDataAccessLibrary.Services;
using System.Media;
using SubProgWPF.Windows;
using static SubProgWPF.Models.TestOverviewModel;

namespace SubProgWPF.ViewModels.Test
{
    public class TabTestViewModel : ViewModelBase
    {
        private readonly ICommand _tabTestCommand;
        private TestModel _testModel;
        private int _remainingWordCount;
        private int _wordIndex = 0;
        private int _selectedSourceIndex = -1;
        private string _remainingWord;
        private string _textTotalWordsToPractice;
        private ObservableCollection<TestMediaModel> _members;
        SoundPlayer player;
        MenuTestDashViewModel _tabTestDashViewModel; 
        private TestWord _currentTestWord;

        public string ProgressBarValue
        {
            get => (((TotalWordCount - (float)_remainingWordCount) / TotalWordCount) * 100).ToString();
            set { }
        }
        public ICommand TabTestCommand => _tabTestCommand;

        public ObservableCollection<TestMediaModel> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                OnPropertyChanged(nameof(Members));
            }
        }

        public string Word
        {
            get
            {
                if (_testModel.WordsToBeTested.Count > 0 && RemainingWordCount > 0) { return _testModel.WordsToBeTested[_wordIndex].Name; }
                else { return ""; }
            }
        }
        public int Index
        {
            get { return _wordIndex; }
            set
            {
                _wordIndex = value;
                OnPropertyChanged(nameof(Word));
                OnPropertyChanged(nameof(Index));
            }
        }





        public List<string> SourceList
        {
            get
            {
                if (_testModel.WordsToBeTested.Count > 0) { return _testModel.WordsToBeTested[_wordIndex].WordContexts.Select(x => x.Type.ToString()).ToList(); }
                else { return new List<string>(); }
            }
        }
        //public ObservableCollection<TestWordContextWithMedia> SourceList { get { return new ObservableCollection<TestWordContextWithMedia>(_testModel.WordsToBeTested[_wordIndex].WordContexts); } }
        public int SelectedSourceIndex
        {
            get { return _selectedSourceIndex; }
            set
            {
                _selectedSourceIndex = value;
                OnPropertyChanged(nameof(SelectedSourceIndex));
                if (SelectedSourceIndex != -1)
                {
                    string wordStr = _currentTestWord.WordContexts[SelectedSourceIndex].MediaLocation;
                    string timeAppendix = getTimeAppendix(_currentTestWord.WordContexts[SelectedSourceIndex].Address.SubLocation);
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        //FileName = "https://www.google.com/search?q=" + wordStr + " meaning",
                        FileName = wordStr + timeAppendix,
                        UseShellExecute = true
                    });

                    // Uncomment- out this section
                    //_testModel.openMedia(_testModel.WordsToBeTested[_wordIndex].WordContexts[SelectedSourceIndex]);
                    SelectedSourceIndex = -1;
                }



            }
        }




        internal void ExitSession()
        {
            _tabTestDashViewModel.ExitCurrentSession();
        }

        


        public TabTestViewModel(TYPE type, MenuTestDashViewModel tabTestDashView)
        {
            _tabTestDashViewModel = tabTestDashView;
            _tabTestCommand = new TabTestCommand(this);
            _testModel = new TestModel(type);
            
                if(_testModel.WordsToBeTested.Count > 0)
            {
                _currentTestWord = _testModel.WordsToBeTested[0];
                setRemainingWordText();
                setTestOverviewTotalWordText();
                setWordMediaOptions();
                loadSoundFile();
            }
                
        }
            
            
        

        internal void promptWarning()
        {
            PromptTestExitWarningWindow prompt = new PromptTestExitWarningWindow(this);
            prompt.Show();
        }

        private void setWordMediaOptions()
        {
            if(_currentTestWord == null) { return; }
            _currentTestWord = _testModel.WordsToBeTested[Index];
            Members = new ObservableCollection<TestMediaModel>();
            foreach (TestWordContextWithMedia t in _currentTestWord.WordContexts)
            {
                TestMediaModel TMM = new TestMediaModel() { MediaName = t.Title};
                switch (t.Type.ToString())
                {
                    case "Youtube":
                        TMM.IconKind = "Youtube";
                        break;
                    case "TVSeries":
                        TMM.IconKind = "DesktopMac";
                        break;
                    case "Movie":
                        TMM.IconKind = "Movie";
                        break;
                }
                Members.Add(TMM);
            }
        }

        internal void LaunchTestSession()
        {
            
        }

        internal void executeAnswer(bool isSuccess)
        {
            createRepetition(isSuccess);
            decrementTestProperties();
            setWordMediaOptions();
            if (isSuccess) { incrementScore(); }
        }

        private void incrementScore()
        {
            ScoreServices.IncrementScoreRepetition();
        }

        private void decrementTestProperties()
        {
            SelectedSourceIndex = -1;
            Index = TotalWordCount - 1 > Index ?
                            Index + 1 : Index;
            RemainingWordCount = RemainingWordCount - 1;
            if(RemainingWordCount == 0)
            {
                playFinish();
                ExitSession();
            }
            else
            {
                playClick();
            }
        }
        private void loadSoundFile()
        {
            player = new SoundPlayer();
            player.Stream = SubProgWPF.Properties.Resources.click;
        }

        public void playClick()
        {
            player.Play();
        }
        public void playFinish()
        {
            player.Stream = SubProgWPF.Properties.Resources.success;
            player.Play();
        }

        private void createRepetition(bool isSuccess)
        {
            Repetition repetition = new Repetition();
            repetition.Time = DateTime.Now;
            repetition.Success = isSuccess;
            TestServices.AddRepetition(TestModel.WordsToBeTested[Index].WordDBId, repetition);
            SubProgWPF.Utils.Test.updateEbbingIndexOfWord(TestModel.WordsToBeTested[Index].WordDBId, isSuccess);
        }

        

        private void setRemainingWordText()
        {
            _remainingWordCount = _testModel.WordsToBeTested.Count - _wordIndex;
            _remainingWord = "Remaining Words: " + _remainingWordCount;
        }
        private void setTestOverviewTotalWordText()
        {
            _remainingWordCount = _testModel.WordsToBeTested.Count - _wordIndex;
            _textTotalWordsToPractice = "Total Words To Practice: " + _remainingWordCount;
        }

        

        private string getTimeAppendix(string subLocation)
        {
            string prefix = "&t=";
            int hour = Int32.Parse(subLocation.Substring(0, 2));
            int minute = Int32.Parse(subLocation.Substring(3, 2));
            int second = Int32.Parse(subLocation.Substring(6, 2));

            second -= 3;
            if(second < 0)
            {
                minute -= 1;
                second += 60;
            }
            string time = prefix + ((hour * 60) + minute).ToString() + "m" + second.ToString() + "s";
            
            return time;
        }

        public override void updateTheFields()
        {
            throw new NotImplementedException();
        }

        public string RemainingWords { get => _remainingWord; set { _remainingWord = value; OnPropertyChanged(nameof(RemainingWords)); } }
        public string TextTotalWordsToTest { get => _textTotalWordsToPractice; set { _textTotalWordsToPractice = value; OnPropertyChanged(nameof(TextTotalWordsToTest)); } }

        public int TotalWordCount { get => _testModel.WordsToBeTested.Count;}
        public List<Word> AllWords { get => _testModel.AllLearnedWords;}
        public int RemainingWordCount { get => _remainingWordCount; set { _remainingWordCount = value;
                RemainingWords = "Remaining Words: " + _remainingWordCount;
                OnPropertyChanged(nameof(RemainingWordCountString));
                OnPropertyChanged(nameof(ProgressBarValue));
            }  }

        public string RemainingWordCountString
        {
            get => _remainingWordCount.ToString();
        }


        public TestModel TestModel { get => _testModel; set => _testModel = value; }
        

        public enum TestCommands
        {
            Meaning,
            ShowImage,
            YES,
            NO
        }
    }
    public class TestMediaModel
    {
        public string IconKind { get; set; }
        public string MediaName { get; set; }
    }
    
}
