using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SubProgWPF.Models
{
    public class MembersModel
    {

        private int PPI = 50;   // Per Page Item
        ObservableCollection<MemberNewWord> members;
        ObservableCollection<MemberNewWord> members_copy;
        ObservableCollection<MemberNewWord> removedMembers;
        private List<int> removed_indexes;
        List<TempWord> copyWordList;
        private int current_page = 1;
        BackgroundWorker worker;

        public int Current_page { get => current_page; set => current_page = value; }

        public MembersModel(List<TempWord> words)
        {
            copyWordList = words;
            removed_indexes = new List<int>();
            
            populateAllMembers(words);
            updateCurrentMembers();
        }

        //public void createDataMembers(List<Word> words)
        //{

        //    List<Word> wordList = new List<Word>(words);
        //    copyWordList = new List<Word>(words);
        //    wordList = Splice(wordList, (Current_page - 1) * PPI, PPI);
        //    updateCurrentMembers();
        //    //addMembersToGrid(wordList);
        //}
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

        public ObservableCollection<MemberNewWord> GetCurrentGridItems()
        {
            return members;
        }
        public ObservableCollection<MemberNewWord> GetAllMembers()
        {
            return members_copy;
        }
        public List<TempWord> getWordsCopy()
        {
            return copyWordList;
        }
        public void populateAllMembers(List<TempWord> words)
        {
            members_copy = new ObservableCollection<MemberNewWord>();
            Random rnd = new Random();
            var converter = new BrushConverter();

            string[] colors = new string[10] {"#32a852","#316e78", "#7919c2", "#a86c05", "#e02f1b",
                                                "#3d0a04","#0d8aff","#e300d0","#ab495b","#5ec936"};
            int counter = 1;
            foreach(TempWord w in words)
            {
                if(w.WordContext_Ids.Split(",").Length == 0)
                {
                    continue;
                }
                string context = getContextString(w);
                //string time = getPlayHeadTimeString(w);
                string time = "";
                MemberNewWord m = new MemberNewWord { Number = counter.ToString(), WordDBId = w.Id, WordObj = w, Character = w.Name.Substring(0, 1), BGColor = (Brush)converter.ConvertFromString(colors[rnd.Next(colors.Length)]), Name = w.Name, Position = time, Context = context};
                members_copy.Add(m);
                
                counter += 1;
            }
            //removeExistingWords();
        }

        public void removeExistingWords()
        {
            worker = new BackgroundWorker();
            removedMembers = new ObservableCollection<MemberNewWord>();

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_update;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }

        private void worker_update(object sender, ProgressChangedEventArgs e)
        {
            removedMembers.Add((MemberNewWord)e.UserState);
            members.Remove((MemberNewWord)e.UserState);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("worker finished");
            string messageBoxText = "";
            foreach(MemberNewWord m in removedMembers)
            {
                messageBoxText += m.Name;
                messageBoxText += "\n\t";
                messageBoxText += m.Context;
                messageBoxText += "\n\n";

                
                for (int i = 0; i < m.WordObj.WordContext_Ids.Split(",").Length; i++)
                {
                    WordContext wContext = WordServices.getWordContextByID(m.WordObj.WordContext_Ids[i]);
                    WordServices.addContextToWord(m.WordDBId, wContext);
                }
                
            }
            //string messageBoxText = "Do you want to save changes?";
            string caption = "The Words already existing in database.";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (Models.MemberNewWord m in members_copy)
            {
                int id = WordServices.checkIfWordExists(m.Name);
                if (id != -1)
                {
                    Console.WriteLine("removed " + m.Name);
                    m.WordDBId = id;
                    worker.ReportProgress(id, m);
                }
            }
        }




        public void updateGrid(string command)
        {
            bool last_page = false;
            if (Current_page * PPI > members_copy.Count)
            {
                last_page = true;
            }
            if((last_page && command.Equals("next")) || (Current_page == 1 && command.Equals("prev")))
            {
                return;
            }
            if (command.Equals("next") && !last_page)
            {
                Current_page += 1;
            }else if (command.Equals("prev") && Current_page > 1)
            {
                Current_page -= 1;
            }
            //List<Word> wordList = new List<Word>(copyWordList);
            updateCurrentMembers();
            //addMembersToGrid(Splice(wordList, (Current_page - 1) * PPI, PPI));
        }

        private void updateCurrentMembers()
        {
            List<MemberNewWord> members_ = new List<MemberNewWord>(GetAllMembers());
            members_ = Splice(members_, (Current_page - 1) * PPI, PPI);
            members = new ObservableCollection<MemberNewWord>(members_);

        }

        //private void addMembersToGrid(List<Word> copy_list)
        //{
        //    //Random rnd = new Random();
        //    //var converter = new BrushConverter();
        //    members = new ObservableCollection<Member>();
        //    //members_copy = new ObservableCollection<Member>();
        //    //string[] colors = new string[10] {"#32a852","#316e78", "#7919c2", "#a86c05", "#e02f1b",
        //    //                                    "#3d0a04","#0d8aff","#e300d0","#ab495b","#5ec936"};
        //    //int counter = (Current_page - 1) * PPI + 1;
        //    foreach (Word w in copy_list)
        //    {
        //        //string context = getContextString(w);
        //        //string time = getPlayHeadTimeString(w);
        //        Member m = new Member { Number = counter.ToString(), Character = w.Name.Substring(0, 1), BGColor = (Brush)converter.ConvertFromString(colors[rnd.Next(colors.Length)]), Name = w.Name, Position = time, Context = context};
        //        members.Add(m);
        //        //members_copy.Add(m);
        //        //counter += 1;
        //    }

        //    //wordTitleText.Text = copy_list.Count.ToString() + " Words";
        //    //membersDataGrid.ItemsSource = members;        //uncomment out
        //}

        private string getPlayHeadTimeString(TempWord w)
        {
            string s = "";
            
            for (int i = 0; i < w.WordContext_Ids.Split(",").Length; i++)
            {
                WordContext wC = WordServices.getWordContextByID(w.WordContext_Ids[i]);
                s = string.Concat(s, wC.Address.SubLocation);
                s = string.Concat(s, "\n");
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
                //if(i != w.WordContext_Ids.Split(",").Length - 1)
                //{
                //    s = string.Concat(s, "\n+ ");
                //}
                
            }
            //foreach(WordContext a in w.WordContexts)
            //{
            //    s = string.Concat(s, a.Content);
            //    s = string.Concat(s, "\n+ ");
            //}
            return s;
        }
    }
    public class MemberNewWord
    {
        public int WordDBId { get; set; }
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Context { get; set; }
        public Brush BGColor { get; set; }
        public TempWord WordObj { get; set; }
    }
}
