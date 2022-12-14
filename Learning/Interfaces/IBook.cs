using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IBook : IMedia
    {
        public int PPS { get; }    // Page Count Per Section
        
        public IMedia Media { get; }
    }
}
