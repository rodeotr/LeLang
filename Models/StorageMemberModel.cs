using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;

namespace SubProgWPF.Models
{
    public class StorageMemberModel
    {

        private int PPI = 50;   // Per Page Item
        ObservableCollection<Word> members;
        ObservableCollection<WordMember> currentMembers;
        ObservableCollection<WordMember> allMembers;

        List<Word> words;
        private int current_page = 1;

        public int Current_page { get => current_page; set => current_page = value; }

        public StorageMemberModel(List<Word> words)
        {
            this.words = words;
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

        public ObservableCollection<WordMember> GetCurrentGridItems()
        {
            return currentMembers;
        }
       
        public List<Word> getWordsCopy()
        {
            return words;
        }
        public void populateAllMembers(List<Word> words)
        {
            Random rnd = new Random();
            var converter = new BrushConverter();
            allMembers = new ObservableCollection<WordMember>();
            string[] colors = new string[10] {"#32a852","#316e78", "#7919c2", "#a86c05", "#e02f1b",
                                                "#3d0a04","#0d8aff","#e300d0","#ab495b","#5ec936"};
            int counter = 1;
            foreach(Word w in words)
            {
                string context = getContextString(w);
                WordMember m = new WordMember { Number = counter.ToString(), Character = w.Name.Substring(0, 1), BGColor = (Brush)converter.ConvertFromString(colors[rnd.Next(colors.Length)]), Name = w.Name, Context = context};
                allMembers.Add(m);
                counter += 1;
            }
        }
        

        

        public void updateGrid(string command)
        {
            bool last_page = false;
            if (Current_page * PPI > words.Count)
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

        public ObservableCollection<WordMember> GetAllMembers()
        {
            return allMembers;
        }

        private void updateCurrentMembers()
        {
            List<WordMember> members_ = new List<WordMember>(GetAllMembers());
            members_ = Splice(members_, (Current_page - 1) * PPI, PPI);
            currentMembers = new ObservableCollection<WordMember>(members_);

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



        private string getContextString(Word w)
        {
            string s = "";
            
            for (int i = 0; i < w.WordContext_Ids.Split(",").Length; i++)
            {
                WordContext a = WordServices.getWordContextByID(w.WordContext_Ids[i]);
                s = string.Concat(s, a.Content);
                s = string.Concat(s, "\n");
            }
          
            return s;
        }
    }
    public class WordMember
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public Brush BGColor { get; set; }
    }
}
