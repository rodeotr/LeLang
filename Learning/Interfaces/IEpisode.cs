using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IEpisode : IMedia
    {
        public string SeasonIndex { get; set; }
        public string EpisodeIndex { get; set; }
        public IMedia Media { get; }
    }
}
