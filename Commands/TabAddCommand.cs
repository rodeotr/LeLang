using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabAddCommand : CommandBase
    {
        private TabAddMediaViewModel _tabAddViewModel;

        public TabAddCommand(TabAddMediaViewModel tabAddViewModel)
        {
            _tabAddViewModel = tabAddViewModel;
        }

        public override void Execute(object parameter)
        {

            TranscriptionAddress transcription = createTempMedia();


            string type = _tabAddViewModel.MediaType;
            switch (type)
            {
                case "TVSeries":
                    if (createEpisode(transcription) != -1) { 

                        addTempWordsToTempMedia(transcription); 
                    }
                    break;
                case "Podcast":
                    if (createPodcast(transcription) != -1) { addTempWordsToTempMedia(transcription); }
                    break;

            }
            
        }

        private TranscriptionAddress createTempMedia()
        {
            TranscriptionAddress transcription = new TranscriptionAddress()
            {
                Id = MediaServices.getTranscriptionCount(),
                TranscriptionLocation = _tabAddViewModel.TranscriptionLocation
            };

            TempMedia tempMedia = new TempMedia()
            {
                Name = _tabAddViewModel.SelectedMediaName,
                TranscriptionAddress_Id = transcription.Id,
                SelectedWordFrequency = Int32.Parse(_tabAddViewModel.MaxWordFreq)
            };

            TempServices.CreateTempMedia(tempMedia);

            return transcription;
        }

        private int createPodcast(TranscriptionAddress transcription)
        {
            FPodcast podcast = new FPodcast()
            {
                InitTime = DateTime.Now,
                Name = _tabAddViewModel.SelectedMediaName,
                TranscriptionAddress = transcription
            };
            //PodcastCreator.CreatePodcast(podcast);
            return 1;
        }

        /// <summary>
        ///     Creates the episode.
        /// </summary>
        /// <returns>Returns -1 if the method fails.</returns>
        private int createEpisode(TranscriptionAddress transcription)
        {
            
            return TVSeriesCreator.CreateEpisode(
                new FTVEpisode()
                {
                    EpisodeIndex = Int32.Parse(_tabAddViewModel.EpisodeIndex),
                    TranscriptionAddress_Id = transcription.Id
                },
            _tabAddViewModel.SelectedMediaName,
            _tabAddViewModel.SeasonIndex); ;
        }
        private void addTempWordsToTempMedia(TranscriptionAddress transcription)
        {
            List<TempWord> allWords = LangUtils.GetAllWordObjects(_tabAddViewModel.TranscriptionLocation, Int32.Parse(_tabAddViewModel.MaxWordFreq), null);
            TempMedia tempMedia = TempServices.getTempMediaByTranscription(transcription);
            TempServices.addTempWordsToTempMedia(tempMedia, allWords);
            LaunchGrid(allWords);
        }

        private void LaunchGrid(List<TempWord> allWords)
        {
            MembersModel model = new MembersModel(allWords);

            DataGridNewWordModel gridNewWordModel = new DataGridNewWordModel()
            {
                MembersModel = model,
                AddMediaModel = new AddMediaModel()
            };

            _tabAddViewModel.launchNewWordsTab(gridNewWordModel);
        }

    }
}
