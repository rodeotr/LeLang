using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IBook : IMedia
    {
        public int StartPage { get; }    // Page Count Per Section
        public int EndPageOfSection { get; }    // Page Count Per Section

        public IMedia Media { get; }
    }
}
