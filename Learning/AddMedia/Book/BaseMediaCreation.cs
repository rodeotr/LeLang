using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.Utils;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SubProgWPF.Learning.AddMedia
{
    public abstract class BaseMediaCreation : IMediaCreation
    {
       

    
            public int CreateTranscription(IMedia iMedia)
        {
            TranscriptionAddress transcription = new TranscriptionAddress()
            {
                Id = TranscriptionServices.getTranscriptionCount(),
                TranscriptionLocation = iMedia.TranscriptionLocation,
                Title = iMedia.Name
                
            };
            //if (iMedia.Type.Equals(MediaTypes.TYPE.Youtube))
            //{
                
            //}
            return TranscriptionServices.createTranscriptionAddress(transcription);
        }

        //public void saveTheWords(IMedia iMedia)
        //{
        //    GetWordsFromTranscriptionFile getWordsObj = new GetWordsFromTranscriptionFile(
        //    Int32.Parse(iMedia.MaxWordFreq),
        //    iMedia.TranscriptionLocation,
        //    iMedia.Type);

        //    List<TempWord> tempWords = getWordsObj.GetWords("0", _worker);
        //    MediaServices.addWordsByTranscriptionLocation(iMedia.TranscriptionLocation, tempWords);

        //}

        //public async Task PerformScanAsync(IProgress<ReportClass> progress)
        //{
        //    vM.progressBar.Value = Int32.Parse(report.ReportProgress);
        //    vM.progressText.Text = report.Text;
        //}

       
        public virtual void saveTheWords(IMedia iMedia, BackgroundWorker worker)
        {
            GetWordsFromTranscriptionFile getWordsObj = new GetWordsFromTranscriptionFile(iMedia);

            //Progress<ReportClass> progress = new Progress<ReportClass>(progressUpdate);

            //progress.ProgressChanged += progressUpdate;

            List<TempWord> tempWords = getWordsObj.GetWords(worker);
            TempServices.addWordsToTempWords(tempWords);
        }
       
    }
    public class ReportClass
    {
        public string ReportProgress { get; set; }
        public string Text { get; set; }
    }
}
