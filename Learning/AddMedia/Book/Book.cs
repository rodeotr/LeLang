using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public class Book : IBook
    {
        IMedia _media;
        private int _pps;
        

        public Book(IMedia media, int pps)
        {
            _pps = pps;
            _media = media;
        }

        public IMedia Media { get => _media; }
        
        public string Name { get => _media.Name; }
        public string TranscriptionLocation { get => _media.TranscriptionLocation; }
        public string MaxWordFreq { get => _media.MaxWordFreq; }
        public MediaTypes.TYPE Type { get => _media.Type; }
        public int TranscriptionId { get => _media.TranscriptionId ; set => _media.TranscriptionId = value ; }
        public int PPS { get=>_pps; }
    }
}
