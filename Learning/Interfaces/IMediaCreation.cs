using LangDataAccessLibrary.Models;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IMediaCreation
    {
        // Creates the transcription and returns the ID of the transcription.
        int CreateTranscription(IMedia iMedia);
        void saveTheWords(IMedia iMedia, BackgroundWorker worker); 
        
    }
}
