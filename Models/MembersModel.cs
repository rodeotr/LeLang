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
        private int _currentPage = 1;
        private int _PPI = 50;   // Per Page Item
        private bool _lastPage = false;
        private ObservableCollection<MemberNewWord> _currentMembers;
        private ObservableCollection<MemberNewWord> _allMembers;
        private ObservableCollection<MemberNewWord> _removedMembers;
        private readonly List<TempWord> _tempWordList;
        private readonly BackgroundWorker _worker;

        public MembersModel(List<TempWord> words)
        {
            _worker = new BackgroundWorker();
            _tempWordList = words;
            
            populateAllMembers();
            updateCurrentMembers();

            
        }

        public void populateAllMembers()
        {
            _allMembers = new ObservableCollection<MemberNewWord>();
            //List<Word> knownWords = WordServices.getAllWords();

            var converter = new BrushConverter();
            int counter = 1;
            foreach (TempWord w in _tempWordList)
            {
                ObservableCollection<StorageContext> contexts;
               
                //Word word = knownWords.FirstOrDefault(a => a.Name.Equals(w.Name));
                //if (word != null)
                //{
                //    WordServices.addContextArrayToWord(word.Id, w.WordContext_Ids);
                //    TempServices.deleteTempWordFromDB(w);
                //    continue;
                //}

                string randColor = getRandomColor();
                //string context = getContextString(w);
                contexts = getContextCollection(w);
                string time = getPlayHeadTimeString(w);

                MemberNewWord m = new MemberNewWord { 
                    Number = counter.ToString(),
                    WordDBId = w.Id,
                    WordObj = w,
                    Character = w.Name.Substring(0, 1),
                    BGColor = (Brush)converter.ConvertFromString(randColor),
                    Name = w.Name,
                    Position = time,
                    Contexts = contexts };
                _allMembers.Add(m);

                counter += 1;
            }
            //removeExistingWords();
        }

        private ObservableCollection<StorageContext> getContextCollection(TempWord w)
        {
            ObservableCollection<StorageContext> list = new ObservableCollection<StorageContext>();
            string[] contextArray = w.WordContext_Ids.Split(",");
            if (contextArray[0].Length > 0)
            {
                for (int i = 0; i < contextArray.Length; i++)
                {
                    WordContext a = WordServices.getWordContextByID(Int32.Parse(contextArray[i]));

                    StorageContext sC = new StorageContext()
                    {
                        Word = w.Name,
                        IconKind = a.Type == MediaTypes.TYPE.TVSeries ? "DesktopMac" :
                        a.Type == MediaTypes.TYPE.Book ? "Book" :
                        a.Type == MediaTypes.TYPE.Youtube ? "Youtube" : "Close",
                        Time = a.Address.SubLocation,
                        Context = a.Content,
                        Medium = a.Type.ToString(),
                        Number = i.ToString()
                    };
                    list.Add(sC);
                    if(a.Type == MediaTypes.TYPE.Youtube)
                    {
                        sC.Time = a.Address.SubLocation;
                        sC.MediaLocation = TranscriptionServices.getTranscriptionByID(a.Address.TranscriptionAddress_Id).MediaLocation;
                    }
                }
                return list;
            }
            return null;

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
            if ((Current_page) * _PPI > _allMembers.Count)
            {
                _lastPage = true;
            }
            if((_lastPage && command.Equals("next")) || (Current_page <= 0 && command.Equals("prev")))
            {
                return;
            }
            
          
            updateCurrentMembers();
        }
        public void updateCurrentMembers()
        {
            List<MemberNewWord> members_ = new List<MemberNewWord>(AllMembers);
            members_ = Splice(members_, (Current_page - 1) * _PPI, _PPI);
            _currentMembers = new ObservableCollection<MemberNewWord>(members_);
        }
        private string getPlayHeadTimeString(TempWord w)
        {
            string s = "";
            string[] contextArray = w.WordContext_Ids.Split(",");
            if(contextArray[0].Length > 0)
            {
                for (int i = 0; i < contextArray.Length; i++)
                {
                    WordContext wC = WordServices.getWordContextByID(Int32.Parse(contextArray[i]));
                    s = string.Concat(s, wC.Address.SubLocation);
                    s = string.Concat(s, "\n");
                }
            }
            return s;
        }
        private string getContextString(TempWord w)
        {
            string s = "";
            List<WordContext> wContexts = WordServices.getWordContextsFromTempWord(w);
            for (int i = 0; i < wContexts.Count; i++)
            {
                WordContext wC = wContexts[i];
                s = string.Concat(s, wC.Content);
            }
            return s;
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
        public void removeExistingWords()
        {
            _removedMembers = new ObservableCollection<MemberNewWord>();

            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            _worker.ProgressChanged += worker_update;
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync();
        }
        private void worker_update(object sender, ProgressChangedEventArgs e)
        {
            _removedMembers.Add((MemberNewWord)e.UserState);
            _currentMembers.Remove((MemberNewWord)e.UserState);
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //string messageBoxText = "";
            //foreach (MemberNewWord m in _removedMembers)
            //{
            //    messageBoxText += m.Name;
            //    messageBoxText += "\n\t";
            //    messageBoxText += m.Context;
            //    messageBoxText += "\n\n";


            //    for (int i = 0; i < m.WordObj.WordContext_Ids.Split(",").Length; i++)
            //    {
            //        WordContext wContext = WordServices.getWordContextByID(m.WordObj.WordContext_Ids[i]);
            //        WordServices.addContextToWord(m.WordDBId, wContext);
            //    }

            //}
            ////string messageBoxText = "Do you want to save changes?";
            //string caption = "The Words already existing in database.";
            //MessageBoxButton button = MessageBoxButton.OK;
            //MessageBoxImage icon = MessageBoxImage.Information;
            //MessageBoxResult result;

            //result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (MemberNewWord m in _allMembers)
            {
                int id = WordServices.checkIfWordExists(m.Name);
                if (id != -1)
                {
                    m.WordDBId = id;
                    _worker.ReportProgress(id, m);
                }
            }
        }

        public int Current_page { get => _currentPage; set => _currentPage = value; }
        public bool IsLastPage { get => _lastPage; set => _lastPage = value; }
        public List<TempWord> TempWordList => _tempWordList;
        public ObservableCollection<MemberNewWord> AllMembers { get => _allMembers; set => _allMembers = value; }
        public ObservableCollection<MemberNewWord> CurrentMembers { get => _currentMembers; set { _currentMembers = value;} }
    }
    public class MemberNewWord
    {
        public int WordDBId { get; set; }
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public ObservableCollection<StorageContext> Contexts { get; set; }
        //public string Context { get; set; }
        public Brush BGColor { get; set; }
        public TempWord WordObj { get; set; }
    }
}
