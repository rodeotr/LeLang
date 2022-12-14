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
    public class EpisodeCreationImplementation : BaseEpisodeCreation
    {

        public override bool createEpisode(IEpisode episode)
        {
            TranscriptionAddress tA = TranscriptionServices.getTranscriptionByLocation(episode.TranscriptionLocation);
            int success = TVSeriesCreator.CreateEpisode(
                new FTVEpisode()
                {
                    EpisodeIndex = Int32.Parse(episode.EpisodeIndex),
                    TranscriptionAddress_Id = tA.Id
                },
            episode.Name,
            episode.SeasonIndex);
            return success == 1 ? true : false;
        }
    }
}
