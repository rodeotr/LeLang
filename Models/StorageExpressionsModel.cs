using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;

namespace SubProgWPF.Models
{
    public class StorageExpressionsModel
    {
        private bool _lastPage;
        private int _currentPage = 1;
        private int _PPI = 50;   // Per Page Item
        private ObservableCollection<WordMember> _currentMembers;
        private ObservableCollection<WordMember> _allMembers;
        private List<IdiomsAndExpressions> _expressions;
        

      
        public StorageExpressionsModel(List<IdiomsAndExpressions> expressions)
        {
            _expressions = expressions;
            populateAllMembers();
            updateCurrentMembers();
        }

        public void populateAllMembers()
        {
            var converter = new BrushConverter();
            _allMembers = new ObservableCollection<WordMember>();
            int counter = 1;
            foreach(IdiomsAndExpressions w in _expressions)
            {
                WordMember m = new WordMember { 
                    Number = counter.ToString(), 
                    Character = w.Text.Substring(0, 1), 
                    BGColor = (Brush)converter.ConvertFromString(getRandomColor()), 
                    Name = w.Text, 
                    Contexts = new ObservableCollection<StorageContext>()};
                _allMembers.Add(m);
                counter += 1;
            }
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
            if (Current_page * _PPI > _allMembers.Count)
            {
                _lastPage = true;
            }
            if ((_lastPage && command.Equals("next")) || (Current_page == 1 && command.Equals("prev")))
            {
                return;
            }
            switch (command)
            {
                case "next":
                    Current_page += 1;
                    break;
                case "prev":
                    Current_page -= 1;
                    _lastPage = false;
                    break;

            }

            updateCurrentMembers();
        }
        private void updateCurrentMembers()
        {
            List<WordMember> members_ = new List<WordMember>(AllMembers);
            members_ = Splice(members_, (Current_page - 1) * _PPI, _PPI);
            _currentMembers = new ObservableCollection<WordMember>(members_);

        }
        private string getContextString(Word w)
        {
            string s = "";
            
            for (int i = 0; i < w.WordContext_Ids.Split(",").Length; i++)
            {
                WordContext a = WordServices.getWordContextByID(Int32.Parse(w.WordContext_Ids.Split(",")[i]));
                s = string.Concat(s, a.Content);
                s = string.Concat(s, "\n");
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
        public int Current_page { get => _currentPage; set => _currentPage = value; }
        public ObservableCollection<WordMember> CurrentMembers { get => _currentMembers; set => _currentMembers = value; }
        public ObservableCollection<WordMember> AllMembers { get => _allMembers; set => _allMembers = value; }
        public List<IdiomsAndExpressions> Expressions { get => _expressions; set => _expressions = value; }


    }
    public class ExpressionMember
    {
        public string Character { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }
        public Brush BGColor { get; set; }
    }
}
