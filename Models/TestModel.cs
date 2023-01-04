using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Utils;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using static SubProgWPF.Models.TestOverviewModel;

namespace SubProgWPF.Models
{
    public class TestModel
    {
        
        private List<Word> _allLearnedWords;
        private List<Word> _wordsToBeTested;
        

        public TestModel(TYPE type)
        {
            _wordsToBeTested = new List<Word>();
            _allLearnedWords = WordServices.getAllWords();
            
            setTestWordsList(type);
            ShuffleMe(_wordsToBeTested);

        }

       

        private void setTestWordsList(TYPE type)
        {
            int maxWordCount = int.MaxValue;
            switch (type)
            {
                case TYPE.Practice30:
                    maxWordCount = 30;
                    break;
                case TYPE.Practice60:
                    maxWordCount = 60;
                    break;
            }

            foreach (Word w in _allLearnedWords)
            {
                if (!TestWordDeterminer.determineIfPracticeNeeded(w))
                {
                    continue;
                }

                //TestWord testWord = new TestWord() { Name = w.Name, WordDBId = w.Id };
                //List<TestWordContextWithMedia> wordContextList = new List<TestWordContextWithMedia>();
                //int context_count = 0;
                ////string[] wContexts = w.WordContext_Ids.Split(",");
                //if(w.Contexts.Count > 0)
                //{
                //    for (int i = 0; i < w.Contexts.Count; i++)
                //    {
                //        //int wC_Id = Int32.Parse(wContexts[i]);

                //        //WordContext c = WordServices.getWordContextByID(wC_Id);
                //        WordContext c = w.Contexts[i];
                //        //TranscriptionAddress tA = TranscriptionServices.getTranscriptionByID(c.Address.TranscriptionAddress_Id);
                //        TranscriptionAddress tA = c.Address.TranscriptionAddress;
                //        if (tA.MediaLocation != null)
                //        {
                //            context_count += 1;
                //            wordContextList.Add(new TestWordContextWithMedia()
                //            {
                //                Title = tA.Title,
                //                Type = MediaTypes.TYPE.TVSeries,
                //                Address = c.Address,
                //                Content = c.Content,
                //                MediaLocation = tA.MediaLocation
                //            });
                //        }
                //    }
                //}
                
                
                //context_count = 0;
                //testWord.WordContexts = wordContextList;
                _wordsToBeTested.Add(w);
                
                if(_wordsToBeTested.Count == maxWordCount)
                {
                    break;
                }
            }
        }

        


        public List<Word> AllLearnedWords { get => _allLearnedWords; set => _allLearnedWords = value; }
        public List<Word> WordsToBeTested { get => _wordsToBeTested; set => _wordsToBeTested = value; }


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

    public class MediaPlayer
    {
        public Vlc.DotNet.Core.VlcMediaPlayer _mediaPlayer;
        public BackgroundWorker _backgroundWorker;
        public string mediaPlayTimeOption;
        public string mediaSubtitleOption;
    }


    public class TestWordContextWithMedia
    {
        public string Title;
        public MediaTypes.TYPE Type { get; set; }
        public Address Address { get; set; }
        public string MediaLocation { get; set; }
        public string SubLocation { get; set; }
        public string Content { get; set; }
    }
}
