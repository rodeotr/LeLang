using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;

namespace SubProgWPF.Models
{
    public class TestOverviewModel
    {
        private List<CollectionTestModel> _collections;
        private List<Word> _allLearnedWords;
        private List<TestWord> _wordsToBeTested;
        private int _totalWordsToBeTested;
        private int _streakCount;
        private int _allTestedWordCount;
        private int _allAcedWordCount;
        private int _todayPracticedWordCount;
        private int _todayPracticedGoalProgress;
        private string _lastTimeReviewed = "05.10.2022";

        public enum TYPE
        {
            Practice30,
            Practice60,
            PracticeAll
        }

        public TestOverviewModel()
        {
            
            setAllWords();
            setTestWordsList();

            _totalWordsToBeTested = _wordsToBeTested.Count;
            _streakCount = TestServices.getStreakCount();
            _allTestedWordCount = TestServices.getTotalTestedWordCount();
            _allAcedWordCount = TestServices.getTotalAcedWordCount();
            _todayPracticedWordCount = TestServices.getTodayPracticedWordCount();
        }


        private void setCollections()
        {
            _collections = new List<CollectionTestModel>();
            List<LangDataAccessLibrary.Models.Collections> collections = CollectionServices.getCollections();
            for (int i = 0; i < collections.Count; i++)
            {
                LangDataAccessLibrary.Models.Collections c = collections[i];
                string iconKind = c.MediaType == MediaTypes.TYPE.Youtube ? "Youtube" :
                    c.MediaType == MediaTypes.TYPE.TVSeries ? "DesktopMac" :
                    c.MediaType == MediaTypes.TYPE.Book ? "Book" :
                    c.MediaType == MediaTypes.TYPE.Random ? "BowlMix" : "FilmStrip";

                CollectionTestModel model = new CollectionTestModel()
                {
                    Name = c.Name,
                    IconKind = iconKind
                };
                _collections.Add(model);
                if (_collections.Count == 3)
                {
                    break;
                }
            }
        }

        private void setTestWordsList()
        {
            _wordsToBeTested = new List<TestWord>();
            foreach (Word w in _allLearnedWords)
            {
                if (!TestWordDeterminer.determineIfPracticeNeeded(w))
                {
                    continue;
                }

                TestWord testWord = new TestWord() { Name = w.Name, WordDBId = w.Id };
                _wordsToBeTested.Add(testWord);
            }
        }




        public List<Word> AllLearnedWords { get => _allLearnedWords; set => _allLearnedWords = value; }
        public List<TestWord> WordsToBeTested { get => _wordsToBeTested; set => _wordsToBeTested = value; }
        public int TotalWordsToBeTested { get => _totalWordsToBeTested; set => _totalWordsToBeTested = value; }
        public int StreakCount { get => _streakCount; set => _streakCount = value; }
        public int AllTestedWordCount { get => _allTestedWordCount; set => _allTestedWordCount = value; }
        public int AllAcedWordCount { get => _allAcedWordCount; set => _allAcedWordCount = value; }
        public int TodayPracticedWordCount { get => _todayPracticedWordCount; set => _todayPracticedWordCount = value; }
        public List<CollectionTestModel> Collections { get => _collections; set => _collections = value; }
        public string LastTimeReviewed { get => _lastTimeReviewed; set => _lastTimeReviewed = value; }
        public int TodayPracticedGoalProgress { get => _todayPracticedGoalProgress; set => _todayPracticedGoalProgress = value; }
        public int TotalLearnedWordCount { get => AllLearnedWords.Count; }
        public int TotalWordsToBeTested1 { get => _totalWordsToBeTested; set => _totalWordsToBeTested = value; }

        private void setAllWords()
        {
            _allLearnedWords = WordServices.getAllWords();
        }

        public void updateWords()
        {
            setAllWords();
            setTestWordsList();

            _totalWordsToBeTested = _wordsToBeTested.Count;
        }
    }
    public class CollectionTestModel
    {
        public string Name { get; set; }
        public string IconKind { get; set; }
    }
}
