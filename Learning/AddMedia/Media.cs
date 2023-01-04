using LangDataAccessLibrary;
using SubProgWPF.Learning.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.AddMedia
{
    public class Media : IMedia
    {
        private string _name;
        private string _transcriptionLocation;
        private string _mediaLocation;
        private string _maxWordFreq;
        private MediaTypes.TYPE _type;
        private int _transcriptionId;

        public Media(string name, string transcriptionLocation, string maxWordFreq, MediaTypes.TYPE type)
        {
            _name = name;
            _transcriptionLocation = transcriptionLocation;
            _maxWordFreq = maxWordFreq;
            _type = type;
        }

        public string Name { get => _name; set => _name = value; }
        public string TranscriptionLocation { get => _transcriptionLocation; set => _transcriptionLocation = value; }
        public string MaxWordFreq { get => _maxWordFreq; set => _maxWordFreq = value; }
        public MediaTypes.TYPE Type { get => _type; set => _type = value; }
        public int TranscriptionId { get => _transcriptionId; set => _transcriptionId = value; }
        public string MediaLocation { get => _mediaLocation; set => _mediaLocation = value; }
    }
}
