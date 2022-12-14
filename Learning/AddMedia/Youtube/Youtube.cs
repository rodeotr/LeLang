using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public class Youtube : IYoutube
    {
        IMedia _media;
        string _link;

        public Youtube(IMedia media, string link)
        {
            _media = media;
            _link = link;
        }

        public IMedia Media { get => _media; }
        public string Name { get => _media.Name; }
        public string TranscriptionLocation { get => _media.TranscriptionLocation; }
        public string MaxWordFreq { get => _media.MaxWordFreq; }
        public string Link { get => _link; }
        public MediaTypes.TYPE Type { get => _media.Type; }
        public int TranscriptionId { get => _media.TranscriptionId ; set => _media.TranscriptionId = value ; }
    }
}
