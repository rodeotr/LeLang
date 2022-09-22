using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;

namespace SubProgWPF.Models
{
    public class TestModel
    {
        private Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer;
        private BackgroundWorker _backgroundWorker;
        private List<Word> _allLearnedWords;
        private List<TestWord> _wordsToBeTested;
        private string mediaPlayTimeOption;
        private string mediaSubtitleOption;

        public TestModel(Vlc.DotNet.Core.VlcMediaPlayer mediaPlayer)
        {
            _wordsToBeTested = new List<TestWord>();
            _mediaPlayer = mediaPlayer;
            setBackgroundWorker();
            setAllWords();
            setTestWordsList();
            shuffleTestWordList();

        }

        private void shuffleTestWordList()
        {
            ShuffleMe(_wordsToBeTested);
        }

        private void setBackgroundWorker()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += worker_DoWork;
            _backgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _backgroundWorker.ProgressChanged += worker_update;
            
            //_backgroundWorker.WorkerReportsProgress = true;
            //_backgroundWorker.RunWorkerAsync();

        }

        private void worker_update(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine();
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("worker completed");
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            _mediaPlayer.SetMedia((Uri)e.Argument, new string[] { mediaPlayTimeOption, mediaSubtitleOption});
            Console.WriteLine(_mediaPlayer.Audio.Volume);
            _mediaPlayer.Play();
            Thread.Sleep(30000);
            _mediaPlayer.Stop();
            _mediaPlayer.ResetMedia();
        }

        private void setTestWordsList()
        {
            foreach (Word w in _allLearnedWords)
            {
                if (!shouldTheWordBeTested(w))
                {
                    continue;
                }

                TestWord testWord = new TestWord() { Name = w.Name, WordDBId = w.Id };
                List<TestWordContextWithMedia> wordContextList = new List<TestWordContextWithMedia>();
                int context_count = 0;
                for(int i = 0; i < w.WordContext_Ids.Split(",").Length; i++)
                {
                    WordContext c = WordServices.getWordContextByID(w.WordContext_Ids[i]);
                    TranscriptionAddress tA = MediaServices.getTranscriptionByID(c.Address.TranscriptionAddress_Id);
                    if (tA.MediaLocation != null)
                    {
                        context_count += 1;
                        wordContextList.Add(new TestWordContextWithMedia()
                        {
                            Type = MediaTypes.TYPE.TVSeries,
                            Address = c.Address,
                            Content = c.Content,
                            MediaLocation = tA.MediaLocation
                        });
                    }
                }
                if(context_count == 0)
                {
                    Console.WriteLine("");
                }
                context_count = 0;
                testWord.WordContexts = wordContextList;
                _wordsToBeTested.Add(testWord);
            }
        }

        private bool shouldTheWordBeTested(Word w)
        {
            bool IsSuccess;
            DateTime date;
            if (w.Repetition.Count > 0)
            {
                date = w.Repetition[w.Repetition.Count - 1].Time;
                IsSuccess = w.Repetition[w.Repetition.Count - 1].Success;
            }
            else
            {
                date = DateTime.Now;
                IsSuccess = false;
            }

            if ((DateTime.Now - date).TotalDays < 3 && IsSuccess)
            {
                return false;
            }
            return true;
        }


        public List<Word> AllLearnedWords { get => _allLearnedWords; set => _allLearnedWords = value; }
        public List<TestWord> WordsToBeTested { get => _wordsToBeTested; set => _wordsToBeTested = value; }

        private void setAllWords()
        {
            _allLearnedWords = WordServices.getAllWords();
        }

        //internal void openMedia(TestWordContextWithMedia testModelWithMediaAvailable)
        //{
        //    //var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(new DirectoryInfo(@"C:\\Program Files (x86)\\VideoLAN\\VLC"));
        //    // time in seconds
        //    //string mediaLocation = "C:\\Users\\Dean\\Desktop\\The.Big.Bang.Theory.S03.Season.3.Complete.720p.HDTV.x264-[maximersk]\\The.Big.Bang.Theory.S05E01.720p.HDTV.ReEnc-Max.mkv";
        //    //string timeCopy = ":start-time=7";
        //    int time = convertTime(testModelWithMediaAvailable.Address.SubLocation);
        //    int seekBeforeTime = 15;
        //    time = time > seekBeforeTime ? time - seekBeforeTime : time;
        //    string timeStr = ":start-time=" + time.ToString();
        //    mediaPlayTimeOption = timeStr;

        //    mediaSubtitleOption = ":sub-file=" + testModelWithMediaAvailable.Address.TranscriptionAddress.TranscriptionLocation;
        //    //_mediaPlayer.SetMedia(new Uri(testModelWithMediaAvailable.MediaLocation), new string[] { ":no-audio", timeStr, ":sout-all", ":qt-pause-minimized" });

        //    //_mediaPlayer.SetMedia(new Uri(testModelWithMediaAvailable.MediaLocation), new string[] { timeStr, ":sout-all"});
        //    //_mediaPlayer.Play();
        //    _backgroundWorker.RunWorkerAsync(new Uri(testModelWithMediaAvailable.MediaLocation));

        //}

        private int convertTime(string subLocation)
        {
            int hour = Int32.Parse(subLocation.Substring(0, 2));
            int minute = Int32.Parse(subLocation.Substring(3, 2));
            int second = Int32.Parse(subLocation.Substring(6, 2));

            return hour * 3600 + minute * 60 + second;
        }

        public void ShuffleMe<T>(IList<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = random.Next(i + 1);

                T value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
        }
    }

    public class TestWord
    {
        public string Name { get; set; }
        public int WordDBId { get; set; }
        public List<TestWordContextWithMedia> WordContexts { get; set; }
    }


    public class TestWordContextWithMedia
    {
        public MediaTypes.TYPE Type { get; set; }
        public Address Address { get; set; }
        public string MediaLocation { get; set; }
        public string SubLocation { get; set; }
        public string Content { get; set; }
    }
}
