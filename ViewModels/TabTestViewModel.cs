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

namespace SubProgWPF.ViewModels
{
    public class TabTestViewModel : ViewModelBase
    {
        private readonly ICommand _tabTestCommand;
        private TestModel _testModel;
        private int _remainingWordCount;
        private int _wordIndex = 0;
        private int _selectedSourceIndex = -1;
        private string _remainingWord;


        public TabTestViewModel(Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer)
        {
            _tabTestCommand = new TabTestCommand(this);
            _testModel = new TestModel(_mediaPlayer);
            setRemainingWordText();
            //createFrames();
        }

        private void createFrames()
        {
            //FFMpegCore.FFMpegOptions.Configure(new FFMpegCore.FFMpegOptions { RootDirectory = @"", TempDirectory = @"" });

            var mediaInfo = FFProbe.Analyse("C:\\Users\\Dean\\Desktop\\Musics\\programming music.mp4");
            //FFMpegCore.FFMpegArguments.FromFileInput("C:\\Users\\Dean\\Desktop\\Musics\\interstellar.mp4")
            FFMpeg.Snapshot(mediaInfo, new System.Drawing.Size(355, 200), TimeSpan.FromMinutes(1));
        }

        private void setRemainingWordText()
        {
            _remainingWordCount = _testModel.WordsToBeTested.Count - _wordIndex;
            _remainingWord = "Remaining Words: " + _remainingWordCount;
        }

        public ICommand TabTestCommand => _tabTestCommand;
        public string Word { get {
                if (_testModel.WordsToBeTested.Count > 0) { return _testModel.WordsToBeTested[_wordIndex].Name; }
                else { return ""; }
            }}
        public int Index { get { return _wordIndex; } set { 
                _wordIndex = value;
                OnPropertyChanged(nameof(Word));
                OnPropertyChanged(nameof(Index));
            } }





        public List<string> SourceList { get
            {
                if (_testModel.WordsToBeTested.Count > 0) { return _testModel.WordsToBeTested[_wordIndex].WordContexts.Select(x => x.Type.ToString()).ToList(); }
                else { return new List<string>(); }
            }
        }
        //public ObservableCollection<TestWordContextWithMedia> SourceList { get { return new ObservableCollection<TestWordContextWithMedia>(_testModel.WordsToBeTested[_wordIndex].WordContexts); } }


        public int SelectedSourceIndex { get { return _selectedSourceIndex; } set {
                _selectedSourceIndex = value;
                OnPropertyChanged(nameof(SelectedSourceIndex));
                if(SelectedSourceIndex != -1)
                {
                    // Uncomment- out this section
                    //_testModel.openMedia(_testModel.WordsToBeTested[_wordIndex].WordContexts[SelectedSourceIndex]);
                    SelectedSourceIndex = -1;
                }

                
                
            } }
        public string RemainingWords { get => _remainingWord; set { _remainingWord = value; OnPropertyChanged(nameof(RemainingWords)); } }

        public int TotalWordCount { get => _testModel.WordsToBeTested.Count;}
        public List<Word> AllWords { get => _testModel.AllLearnedWords;}
        public int RemainingWordCount { get => _remainingWordCount; set { _remainingWordCount = value;
                RemainingWords = "Remaining Words: " + _remainingWordCount;
            }  }

        public TestModel TestModel { get => _testModel; set => _testModel = value; }
    }
}
