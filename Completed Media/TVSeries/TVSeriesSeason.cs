using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF
{
    class TVSeriesSeason
    {
        private string seasonIndex;     //starting from 1 (NOT 0)
        private string episodeCount;
        private string isFinished;
        private List<TVSeriesEpisode> finishedEpisodes;

        public TVSeriesSeason(string seasonIndex, string episodeCount)
        {
            this.seasonIndex = seasonIndex;
            this.episodeCount = episodeCount;
            this.isFinished = "False";
            this.finishedEpisodes = new List<TVSeriesEpisode>();
        }

        public string SeasonIndex { get => seasonIndex; set => seasonIndex = value; }
        public string EpisodeCount { get => episodeCount; set => episodeCount = value; }
        public string IsFinished { get => isFinished; set => isFinished = value; }
        internal List<TVSeriesEpisode> FinishedEpisodes { get => finishedEpisodes; set => finishedEpisodes = value; }
    }
}
