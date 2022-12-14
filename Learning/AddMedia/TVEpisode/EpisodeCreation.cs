using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Add
{
    public class EpisodeCreation
    {
        private string MediaName;
        private string SeasonIndex;
        private string EpisodeIndex;

        public EpisodeCreation(string mediaName, string seasonIndex, string episodeIndex)
        {
            MediaName = mediaName;
            SeasonIndex = seasonIndex;
            EpisodeIndex = episodeIndex;
        }

        public int createEpisode(int transcriptionId)
        {
            return TVSeriesCreator.CreateEpisode(
                new FTVEpisode()
                {
                    EpisodeIndex = Int32.Parse(EpisodeIndex),
                    TranscriptionAddress_Id = transcriptionId,
                    Name = MediaName
                },
            MediaName,
            SeasonIndex);
        }
        
    }
}
