using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF
{
    class TVSeries
    {
        private string name;
        private string initDate;
        private List<TVSeriesSeason> seasons;
        private string totalSeasonCount;
        private string isFinished;

        public TVSeries(string name, string initDate, string totalSeasonCount)
        {
            this.name = name;
            this.initDate = initDate;
            this.totalSeasonCount = totalSeasonCount;
            this.isFinished = "False";
            this.seasons = new List<TVSeriesSeason>();
        }

        public string Name { get => name; set => name = value; }
        public string InitDate { get => initDate; set => initDate = value; }
        public string TotalSeasonCount { get => totalSeasonCount; set => totalSeasonCount = value; }
        public string IsFinished { get => isFinished; set => isFinished = value; }
        internal List<TVSeriesSeason> Seasons { get => seasons; set => seasons = value; }
    }
}
