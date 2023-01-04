using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SubProgWPF.Models
{
    public class MembersModel
    {
        private const int _PPI = 50;   // Per Page Item
        private int _currentPage = 1;
        private bool _lastPage = false;
        private int _totalWordCount;
        private ObservableCollection<MemberNewWord> _currentMembers;
        private List<ObservableCollection<MemberNewWord>> _allPagesList;


        public int Current_page { get => _currentPage; set => _currentPage = value; }
        public bool IsLastPage { get => _lastPage; set => _lastPage = value; }
        public ObservableCollection<MemberNewWord> CurrentMembers { get => _currentMembers; set { _currentMembers = value; } }
        public int TotalWordCount { get => _totalWordCount; set => _totalWordCount = value; }

        public MembersModel(List<TempWord> words)
        {
            _totalWordCount = words.Count;
            populateAllMembers(words);
            _currentMembers = _allPagesList[0];
        }

        public void populateAllMembers(List<TempWord> _tempWordList)
        {
            _allPagesList = new List<ObservableCollection<MemberNewWord>>();
            _allPagesList.Add(new ObservableCollection<MemberNewWord>());

            var converter = new BrushConverter();
            int counter = 1;

            foreach (TempWord w in _tempWordList)
            {
                ObservableCollection<StorageContext> contexts = getContextCollection(w); ;
                MemberNewWord m = new MemberNewWord {
                    Number = counter.ToString(),
                    WordObj = w,
                    Character = w.Name.Substring(0, 1),
                    BGColor = (Brush)converter.ConvertFromString(getRandomColor()),
                    Contexts = contexts,
                };
                _allPagesList[_allPagesList.Count-1].Add(m);

                if (counter % _PPI == 0)
                {
                    _allPagesList.Add(new ObservableCollection<MemberNewWord>());
                }


                counter += 1;
                
            }
        }

        internal void setCurrentPage(int v)
        {
            _currentMembers = _allPagesList[v];
            _currentPage = v + 1;

        }

        private string getRandomColor()
        {
            Random rnd = new Random();

            string[] colors = new string[10] {"#32a852","#316e78", "#7919c2", "#a86c05", "#e02f1b",
                                                "#3d0a04","#0d8aff","#e300d0","#ab495b","#5ec936"};
            return colors[rnd.Next(colors.Length)];
        }


        public void updateGrid(string command)
        {
            switch (command)
            {
                case "next":
                    if (!_lastPage)
                    {
                        Current_page += 1;
                    }
                    break;
                case "prev":
                    if(Current_page != 1)
                    {
                        Current_page -= 1;
                    }
                    _lastPage = false;
                    break;
            }
            if ((Current_page) * _PPI > _totalWordCount)
            {
                _lastPage = true;
            }
            _currentMembers = _allPagesList[Current_page - 1];
        }

        private ObservableCollection<StorageContext> getContextCollection(TempWord w)
        {
            ObservableCollection<StorageContext> list = new ObservableCollection<StorageContext>();
            if (w.Contexts.Count > 0)
            {
                for (int i = 0; i < w.Contexts.Count; i++)
                {
                    TempWordContext a = w.Contexts[i];
                    StorageContext sC = new StorageContext()
                    {
                        Word = w.Name,
                        IconKind = a.Type == MediaTypes.TYPE.TVSeries ? "DesktopMac" :
                            a.Type == MediaTypes.TYPE.Book ? "Book" :
                            a.Type == MediaTypes.TYPE.Youtube ? "Youtube" : "Close",
                        Time = a.Address.SubLocation,
                        Medium = a.Type.ToString(),
                        Number = i.ToString(),
                        Context = a

                    };

                    list.Add(sC);

                    if (a.Type == MediaTypes.TYPE.Youtube)
                    {
                        sC.MediaLocation = a.Address.TranscriptionAddress.MediaLocation;
                    }
                }
                return list;
            }
            return null;
        }



    }
    public class MemberNewWord
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public ObservableCollection<StorageContext> Contexts { get; set; }
        public Brush BGColor { get; set; }
        public TempWord WordObj { get; set; }
    }
}
