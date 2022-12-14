using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Add;
using SubProgWPF.Learning.AddMedia;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learning;
using SubProgWPF.ViewModels.Learning.AddMediaOptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace SubProgWPF.Commands
{
    public class TabAddCommand : CommandBase
    {
        private TabAddMediaViewModel _tabAddViewModel;
        BackgroundWorker worker;
        List<TempWord> allWords;

        public TabAddCommand(TabAddMediaViewModel tabAddViewModel)
        {
            _tabAddViewModel = tabAddViewModel;

            worker = new BackgroundWorker();
            setWorkerProperties();
            
            
        }

        public override void Execute(object parameter)
        {

            _tabAddViewModel.launchProgresBar();
            worker.RunWorkerAsync();

        }


        private void setWorkerProperties()
        {
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;
        }

        
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(allWords != null)
            {
                LaunchGrid(allWords);
                _tabAddViewModel.closeProgresBar();
            }
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var object_ = (ReportClass)e.UserState;
            _tabAddViewModel.ProgressValue = object_.ReportProgress;
            _tabAddViewModel.ProgressText = object_.Text;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            MediaTypes.TYPE Type = getMediaTypeFromString();
            
            Media media = new Media(
                    _tabAddViewModel.SelectedMediaName,
                    _tabAddViewModel.TranscriptionLocation,
                    _tabAddViewModel.MaxWordFreq,
                    Type
                );

            AddMedia addMedia = new AddMedia();

            switch (Type)
            {
                case MediaTypes.TYPE.Youtube:
                    string link = ((TabAddMediaYoutubeViewModel)_tabAddViewModel.CurrentViewModel).Link;
                    Youtube youtube = new Youtube(media, link);
                    addMedia.createYoutube(youtube, worker);
                    break;
                case MediaTypes.TYPE.TVSeries:
                    string seasonIndex = ((TabAddMediaTVSeriesViewModel)_tabAddViewModel.CurrentViewModel).SeasonIndex;
                    string episodeIndex = ((TabAddMediaTVSeriesViewModel)_tabAddViewModel.CurrentViewModel).EpisodeIndex;
                    Episode episode = new Episode(media, seasonIndex, episodeIndex);
                    addMedia.EpisodeCreationSequence(episode, worker);
                    break;
                case MediaTypes.TYPE.Book:
                    string PPS = ((TabAddMediaBookViewModel)_tabAddViewModel.CurrentViewModel).PagePerSection;
                    Book book = new Book(media, Int32.Parse(PPS));
                    addMedia.createBook(book, worker);
                    break;
            }


            allWords = TempServices.getTempWordsByTranscriptionLocation(_tabAddViewModel.TranscriptionLocation);
            List<Word> knownWords = WordServices.getAllWords();

            foreach (TempWord w in allWords)
            {

                Word word = knownWords.FirstOrDefault(a => a.Name.Equals(w.Name));
                if (word != null)
                {
                    WordServices.addContextArrayToWord(word.Id, w.WordContext_Ids);
                }
            }

            TranscriptionServices.updateTranscriptionTotalPossibleWords(_tabAddViewModel.TranscriptionLocation, allWords.Count);

        }
        private MediaTypes.TYPE getMediaTypeFromString()
        {
            MediaTypes.TYPE Type;
            Enum.TryParse(_tabAddViewModel.MediaType, out Type);
            return Type;

        }
        private void LaunchGrid(List<TempWord> allWords)
        {
            MembersModel model = new MembersModel(allWords);

            ListWordsModel gridNewWordModel = new ListWordsModel()
            {
                MembersModel = model,
                AddMediaModel = new AddMediaModel()
            };
            gridNewWordModel.AddMediaModel.TranscriptionLocation = _tabAddViewModel.TranscriptionLocation;
            int transcriptionId = TranscriptionServices.getTranscriptionByLocation(_tabAddViewModel.TranscriptionLocation).Id;
            _tabAddViewModel.switchToNewWordsTab(gridNewWordModel, transcriptionId);
        }
    }
    
}
