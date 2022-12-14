using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SubProgWPF.Learning.AddMedia
{
    public abstract class BaseYoutubeCreation : BaseMediaCreation ,IYoutubeCreation
    {
        protected int _transcriptionId;


        public abstract bool createYoutube(IYoutube youtube);






        //public int createEpisode(int transcriptionId)
        //{
        //    return TVSeriesCreator.CreateEpisode(
        //        new FTVEpisode()
        //        {
        //            EpisodeIndex = Int32.Parse(EpisodeIndex),
        //            TranscriptionAddress_Id = transcriptionId
        //        },
        //    MediaName,
        //    SeasonIndex);
        //}


    }
}
