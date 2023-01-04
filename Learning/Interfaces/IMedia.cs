using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IMedia
    {
        public string Name { get; set; }
        public string TranscriptionLocation { get; set; }
        public string MaxWordFreq { get; set; }
        public MediaTypes.TYPE Type { get; set; }
        public int TranscriptionId { get; set; }
    }
}
