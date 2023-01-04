using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public class Book : IBook
    {
        IMedia _media;
        private readonly int _startPage;
        private readonly int _endPage;
        private int _pps;
        

        public Book(IMedia media, int startPage, int endPage)
        {
            _media = media;
            _startPage = startPage;
            _endPage = endPage;
        }

        public IMedia Media { get => _media; }
        
        public string Name { get => _media.Name; set { Name = value; } }
        public string TranscriptionLocation { get => _media.TranscriptionLocation; set { TranscriptionLocation = value; } }
        public string MaxWordFreq { get => _media.MaxWordFreq; set { MaxWordFreq = value; } }
        public MediaTypes.TYPE Type { get => _media.Type; set { Type = value; } }
        public int TranscriptionId { get => _media.TranscriptionId ; set => _media.TranscriptionId = value ; }
        public int PPS { get=>_pps; }
        public int StartPage { get => _startPage;}
        public int EndPageOfSection { get => _endPage; }
    }
}
