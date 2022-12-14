using LangDataAccessLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IMedia
    {
        string Name { get;}
        string TranscriptionLocation { get;}
        string MaxWordFreq { get;}
        MediaTypes.TYPE Type { get;}
        int TranscriptionId { get; set; }
    }
}
