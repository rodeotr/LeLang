using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public class Episode : IEpisode
    {
        IMedia _media;
        private string _seasonIndex;
        private string _episodeIndex;

        public Episode(IMedia media, string seasonIndex, string episodeIndex)
        {
            _seasonIndex = seasonIndex;
            _episodeIndex = episodeIndex;
            _media = media;
        }

        public IMedia Media { get => _media; }
        public string SeasonIndex { get => _seasonIndex; set => _seasonIndex = value; }
        public string EpisodeIndex { get => _episodeIndex; set => _episodeIndex = value; }
        public string Name { get => _media.Name; set { Name = value; } }
        public string TranscriptionLocation { get => _media.TranscriptionLocation; set { TranscriptionLocation = value; } }
        public string MaxWordFreq { get => _media.MaxWordFreq; set { MaxWordFreq = value; } }
        public MediaTypes.TYPE Type { get => _media.Type; set { Type = value; } }
        public int TranscriptionId { get => _media.TranscriptionId ; set => _media.TranscriptionId = value ; }
    }
}
