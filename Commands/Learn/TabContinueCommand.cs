using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Continue;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learning;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabContinueCommand : CommandBase
    {
        private TabContinueMediaViewModel _tabContinueViewModel;

        public TabContinueCommand(TabContinueMediaViewModel tabContinueViewModel)
        {
            _tabContinueViewModel = tabContinueViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_tabContinueViewModel.SelectedMediaName.Length == 0) { return; }
            List<TempWord> allWords;

            _tabContinueViewModel.IsLoading = true;

            switch (_tabContinueViewModel.MediaType)
            {
                case "TVSeries":
                    FTVEpisode fTVEpisode = getSelectedEpisode();
                    allWords = ContinueMedia.getNewWordsToBeLearned(fTVEpisode.TranscriptionAddress_Id);
                    LaunchGridEpisode(fTVEpisode, allWords);
                    break;
                case "Youtube":
                    FYoutube fYoutube = getSelectedYoutubeVideo();
                    int transcriptionId = getTranscriptionIDFromYoutubeObject(fYoutube);
                    allWords = ContinueMedia.getNewWordsToBeLearned(transcriptionId);
                    LaunchGridYoutube(fYoutube, allWords);
                    break;
                case "Book":
                    Books book = getSelectedBook();
                    allWords = ContinueMedia.getNewWordsToBeLearned(book.TranscriptionAddress_Id);
                    LaunchGridBook(book, allWords);
                    break;
            }
        }

        private int getTranscriptionIDFromYoutubeObject(FYoutube fYoutube)
        {
            return TranscriptionServices.getTranscriptionIDFromYoutubeObject(fYoutube);
        }

        private FTVEpisode getSelectedEpisode()
        {
            for (int i = 0; i < _tabContinueViewModel.MediaNames.Length; i++)
            {
                if (_tabContinueViewModel.SelectedMediaName.Equals(_tabContinueViewModel.MediaNames[i]))
                {
                    return _tabContinueViewModel.Episodes[i];
                }
            }
            return null;
        }
        private FYoutube getSelectedYoutubeVideo()
        {
            for (int i = 0; i < _tabContinueViewModel.MediaNames.Length; i++)
            {
                if (_tabContinueViewModel.SelectedMediaName.Equals(_tabContinueViewModel.MediaNames[i]))
                {
                    return _tabContinueViewModel.YoutubeVideos[i];

                }
            }
            return null;
        }
        private Books getSelectedBook()
        {
            for (int i = 0; i < _tabContinueViewModel.MediaNames.Length; i++)
            {
                if (_tabContinueViewModel.SelectedMediaName.Equals(_tabContinueViewModel.MediaNames[i]))
                {
                    return _tabContinueViewModel.Books[i];

                }
            }
            return null;
        }
        private void LaunchGridEpisode(FTVEpisode fTVEpisode, List<TempWord> allWords)
        {

            string transcriptionLocation = TranscriptionServices.getTranscriptionByID(fTVEpisode.TranscriptionAddress_Id).TranscriptionLocation;
            MembersModel model = new MembersModel(allWords);

            AddMediaModel addMediaModel = new AddMediaModel() {
                EpisodeIndex = fTVEpisode.EpisodeIndex.ToString(),
                SeasonIndex = fTVEpisode.Season.SeasonIndex.ToString(),
                MediaName = _tabContinueViewModel.SelectedMediaName.Split(",")[0],
                Type = LangDataAccessLibrary.MediaTypes.TYPE.TVSeries,
                TranscriptionLocation = transcriptionLocation
            };
            ListWordsModel gridNewWordModel = new ListWordsModel()
            {
                MembersModel = model,
                AddMediaModel = addMediaModel
            };

            _tabContinueViewModel.IsLoading = true;
            _tabContinueViewModel._transcriptionId = fTVEpisode.TranscriptionAddress_Id;
            _tabContinueViewModel._dataGridNewWordModel = gridNewWordModel;
            _tabContinueViewModel._worker.RunWorkerAsync();
            //_tabContinueViewModel.launchGridView(gridNewWordModel, fTVEpisode.TranscriptionAddress_Id);
        }
        private void LaunchGridYoutube(FYoutube fYoutube, List<TempWord> allWords)
        {
            string transcriptionLocation = TranscriptionServices.getTranscriptionByID(TranscriptionServices.getTranscriptionIDFromYoutubeObject(fYoutube)).TranscriptionLocation;

            MembersModel model = new MembersModel(allWords);

            AddMediaModel addMediaModel = new AddMediaModel()
            {
                MediaName = _tabContinueViewModel.SelectedMediaName.Split(",")[0],
                Link = fYoutube.Link,
                Type = LangDataAccessLibrary.MediaTypes.TYPE.Youtube,
                TranscriptionLocation = transcriptionLocation
            };
            ListWordsModel gridNewWordModel = new ListWordsModel()
            {
                MembersModel = model,
                AddMediaModel = addMediaModel
            };

            _tabContinueViewModel.IsLoading = true;
            _tabContinueViewModel._transcriptionId = TranscriptionServices.getTranscriptionIDFromYoutubeObject(fYoutube);
            _tabContinueViewModel._dataGridNewWordModel = gridNewWordModel;
            _tabContinueViewModel._worker.RunWorkerAsync();
            //_tabContinueViewModel.launchGridView(gridNewWordModel, TranscriptionServices.getTranscriptionIDFromYoutubeObject(fYoutube));
        }
        private void LaunchGridBook(Books book, List<TempWord> allWords)
        {

            MembersModel model = new MembersModel(allWords);

            AddMediaModel addMediaModel = new AddMediaModel()
            {
                MediaName = _tabContinueViewModel.SelectedMediaName.Split(",")[0],
                Type = LangDataAccessLibrary.MediaTypes.TYPE.Book
            };
            ListWordsModel gridNewWordModel = new ListWordsModel()
            {
                MembersModel = model,
                AddMediaModel = addMediaModel
            };

            _tabContinueViewModel.IsLoading = true;
            _tabContinueViewModel._transcriptionId = book.TranscriptionAddress_Id;
            _tabContinueViewModel._dataGridNewWordModel = gridNewWordModel;
            _tabContinueViewModel._worker.RunWorkerAsync();
            //_tabContinueViewModel.launchGridView(gridNewWordModel, book.TranscriptionAddress_Id);
        }




    }
}
