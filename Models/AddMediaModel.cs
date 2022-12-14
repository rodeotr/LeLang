using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public class AddMediaModel
    {
        public MediaTypes.TYPE Type;
        public string TypeStr;
        public string MediaName { get; set; } = "";
        public string EpisodeIndex;
        public string SeasonIndex;
        public string Link;
        public string TranscriptionLocation;
        public string MaxWordFrequency = "2";
    }
    
}
