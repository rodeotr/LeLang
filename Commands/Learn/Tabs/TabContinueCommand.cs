using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Continue;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learn;
using SubProgWPF.ViewModels.Learn.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SubProgWPF.Commands.Learn.Tabs
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

            ContinueMediaItem item = _tabContinueViewModel.AllItemList.FirstOrDefault(a => a.Name.Equals(_tabContinueViewModel.SelectedMediaName));
            
            switch (_tabContinueViewModel.MediaType)
            {
                case "TVSeries":
                    List<FTVEpisode> episodes = (List<FTVEpisode>)_tabContinueViewModel.UnfinishedMedia.FirstOrDefault(a => a is List<FTVEpisode>);
                    FTVEpisode fTVEpisode = episodes.FirstOrDefault(a=>a.TranscriptionAddress.Id == item.TranscriptionId);
                    allWords = ContinueMedia.getNewWordsToBeLearned(item.TranscriptionId);
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
                    allWords = ContinueMedia.getNewWordsToBeLearned(item.TranscriptionId);
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
                    return null;
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
                    return null;

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
                    return null;
                    //return _tabContinueViewModel.Books[i];

                }
            }
            return null;
        }
        private void LaunchGridEpisode(FTVEpisode fTVEpisode, List<TempWord> allWords)
        {

            string transcriptionLocation = TranscriptionServices.getTranscriptionByID(fTVEpisode.TranscriptionAddress.Id).TranscriptionLocation;
            MembersModel model = new MembersModel(allWords);

            AddMediaModel addMediaModel = new AddMediaModel() {
                EpisodeIndex = fTVEpisode.EpisodeIndex.ToString(),
                SeasonIndex = fTVEpisode.Season.SeasonIndex.ToString(),
                MediaName = _tabContinueViewModel.SelectedMediaName.Split(",")[0],
                Type = LangDataAccessLibrary.MediaTypes.TYPE.TVSeries,
                TranscriptionLocation = transcriptionLocation,
                TranscriptionId = fTVEpisode.TranscriptionAddress.Id

            };
            ListWordsModel gridNewWordModel = new ListWordsModel()
            {
                MembersModel = model,
                AddMediaModel = addMediaModel
            };

            _tabContinueViewModel.IsLoading = true;
            _tabContinueViewModel._transcriptionId = fTVEpisode.TranscriptionAddress.Id;
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
                TranscriptionLocation = transcriptionLocation,
                TranscriptionId = fYoutube.TranscriptionAddress.Id
            };
            ListWordsModel gridNewWordModel = new ListWordsModel()
            {
                MembersModel = model,
                AddMediaModel = addMediaModel
            };

            _tabContinueViewModel.IsLoading = true;
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
            _tabContinueViewModel._dataGridNewWordModel = gridNewWordModel;
            _tabContinueViewModel._worker.RunWorkerAsync();
            //_tabContinueViewModel.launchGridView(gridNewWordModel, book.TranscriptionAddress_Id);
        }




    }
}
