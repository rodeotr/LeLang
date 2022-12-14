using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;

namespace SubProgWPF.Models
{
    public class StorageWordsModel
    {

        private bool _lastPage;
        private int _PPI = 50;   // Per Page Item
        private int _currentPage = 1;
        private ObservableCollection<WordMember> _currentMembers;
        private ObservableCollection<WordMember> _allMembers;
        List<Word> _words;

        public int CurrentPage { get => _currentPage; set => _currentPage = value; }
        public ObservableCollection<WordMember> CurrentMembers { get => _currentMembers; set => _currentMembers = value; }
        public ObservableCollection<WordMember> AllMembers { get => _allMembers; set => _allMembers = value; }

        public StorageWordsModel(List<Word> words)
        {
            _words = words;
            populateAllMembers();
            updateCurrentMembers();
        }

        public List<T> Splice<T>(List<T> source, int index, int count)
        {
            if (index + count > source.Count)
            {
                count = source.Count - index;
            }
            var items = source.GetRange(index, count);
            source.RemoveRange(index, count);
            return items;
        }

        private string getRandomColor()
        {
            Random rnd = new Random();
            string[] colors = new string[10] {"#32a852","#316e78", "#7919c2", "#a86c05", "#e02f1b",
                                                "#3d0a04","#0d8aff","#e300d0","#ab495b","#5ec936"};
            return colors[rnd.Next(colors.Length)];
        }

        public void populateAllMembers()
        {
            var converter = new BrushConverter();
            _allMembers = new ObservableCollection<WordMember>();
            
            int counter = 1;
            foreach(Word w in _words)
            {
                ObservableCollection<StorageContext> contexts = getContextCollection(w);
      
                WordMember m = new WordMember { 
                    Number = counter.ToString(), 
                    Character = w.Name.Substring(0, 1), 
                    BGColor = (Brush)converter.ConvertFromString(getRandomColor()), 
                    Name = w.Name, 
                    Contexts = contexts};
                _allMembers.Add(m);
                counter += 1;
            }
        }
        public void updateGrid(string command)
        {
            if (CurrentPage * _PPI > _allMembers.Count)
            {
                _lastPage = true;
            }
            if ((_lastPage && command.Equals("next")) || (CurrentPage == 1 && command.Equals("prev")))
            {
                return;
            }
            switch (command)
            {
                case "next":
                    CurrentPage += 1;
                    break;
                case "prev":
                    CurrentPage -= 1;
                    _lastPage = false;
                    break;

            }

            updateCurrentMembers();
        }
        private void updateCurrentMembers()
        {
            List<WordMember> members_ = new List<WordMember>(AllMembers);
            members_ = Splice(members_, (CurrentPage - 1) * _PPI, _PPI);
            _currentMembers = new ObservableCollection<WordMember>(members_);

        }
        private ObservableCollection<StorageContext> getContextCollection(Word w)
        {
            ObservableCollection<StorageContext> list = new ObservableCollection<StorageContext>();
            string[] contextArray = w.WordContext_Ids.Split(",");
            if(contextArray[0].Length > 0)
            {
                for (int i = 0; i < contextArray.Length; i++)
                {
                    WordContext a = WordServices.getWordContextByID(Int32.Parse(contextArray[i]));

                    TranscriptionAddress tA = TranscriptionServices.getTranscriptionByID(a.Address.TranscriptionAddress_Id);
                    StorageContext sC = new StorageContext()
                    {
                        Word = w.Name,
                        IconKind = a.Type == MediaTypes.TYPE.TVSeries ? "DesktopMac" :
                        a.Type == MediaTypes.TYPE.Book ? "Book" :
                        a.Type == MediaTypes.TYPE.Youtube ? "Youtube" : "Close",
                        Context = a.Content,
                        Medium = a.Type.ToString(),
                        Time = a.Address.SubLocation,
                        MediaLocation = tA.MediaLocation,
                        Number = i.ToString()
                    };
                    list.Add(sC);
                }
                return list;
            }
            return null;
            
        }
    }
    public class WordMember
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public ObservableCollection<StorageContext> Contexts { get; set; }
        public Brush BGColor { get; set; }
    }
    public class StorageContext
    {
        public string Number { get; set; }
        public string Word { get; set; }
        public string Medium { get; set; }
        public string IconKind { get; set; }
        public string Context { get; set; }
        public string MediaLocation { get; set; }
        public string Time { get; set; }
    }
}
