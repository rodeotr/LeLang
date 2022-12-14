using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IYoutube : IMedia
    {
        public string Link { get; }
        public IMedia Media { get; }
    }
}
