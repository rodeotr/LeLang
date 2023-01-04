using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Add;
using SubProgWPF.Learning.AddMedia;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learn;
using SubProgWPF.ViewModels.Learn.Tabs;
using SubProgWPF.ViewModels.Learn.Tabs.AddMediaOptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using SubProgWPF.Learning.AddMedia;

namespace SubProgWPF.Commands.Learn
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
            if (IsFormValid())
            {
                _tabAddViewModel.launchProgresBar();
                worker.RunWorkerAsync();
            }
            

        }

        private bool IsFormValid()
        {
            if(_tabAddViewModel.SelectedMediaName.Length == 0
                || _tabAddViewModel.TranscriptionLocation.Length == 0
                || _tabAddViewModel.MaxWordFreq.Length == 0
                )
            {
                return false;
            }
            switch (_tabAddViewModel.MediaType.ToString())
            {
                case "Youtube":
                    if (_tabAddViewModel.MediaType.ToString().Equals("Youtube") &&
                ((TabAddMediaYoutubeViewModel)_tabAddViewModel.CurrentViewModel).Link.Length == 0
                )
                    {
                        return false;
                    }
                    break;
                case "TVSeries":
                    if(((TabAddMediaTVSeriesViewModel)_tabAddViewModel.CurrentViewModel).SeasonIndex.Length == 0
                        || ((TabAddMediaTVSeriesViewModel)_tabAddViewModel.CurrentViewModel).EpisodeIndex.Length == 0)
                    {
                        return false;
                    }
                    break;

            }

            return true;
            

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
            _tabAddViewModel.Progress.CurrentProgress = object_.ReportProgress;
            _tabAddViewModel.Progress.CurrentWord = object_.Text;
            //_tabAddViewModel.ProgressValue = object_.ReportProgress;
            //_tabAddViewModel.ProgressText = object_.Text;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            MediaTypes.TYPE Type = getMediaTypeFromString();
            
            SubProgWPF.Learning.AddMedia.Media media = new SubProgWPF.Learning.AddMedia.Media(
                    _tabAddViewModel.SelectedMediaName,
                    _tabAddViewModel.TranscriptionLocation,
                    _tabAddViewModel.MaxWordFreq,
                    Type
                );

            AddMedia addMedia = new AddMedia();

            bool isFileTypeCompatible = IsFileTypeCompatible(_tabAddViewModel.TranscriptionLocation);

            if (!isFileTypeCompatible)
            {
                promptError();
                return;
            }

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
                    string startPage = ((TabAddMediaBookViewModel)_tabAddViewModel.CurrentViewModel).StartPage;
                    string endPage = ((TabAddMediaBookViewModel)_tabAddViewModel.CurrentViewModel).EndPageOfSection;
                    Book book = new Book(media, Int32.Parse(startPage), Int32.Parse(endPage));
                    addMedia.createBook(book, worker);
                    break;
            }


            allWords = TempServices.getTempWordsByTranscriptionLocation(_tabAddViewModel.TranscriptionLocation);
            //List<Word> knownWords = WordServices.getAllWords();

            //foreach (TempWord w in allWords)
            //{

            //    Word word = knownWords.FirstOrDefault(a => a.Name.Equals(w.Name));
            //    if (word != null)
            //    {
            //        foreach(TempWordContext tWC in w.Contexts)
            //        {
            //            WordServices.addContextToWord(word.Id, tWC);
            //        }
            //    }
            //    //TempServices.deleteTempWordFromDB(w);
            //}
            TranscriptionServices.updateTranscriptionTotalPossibleWords(_tabAddViewModel.TranscriptionLocation, allWords.Count);

        }
        private MediaTypes.TYPE getMediaTypeFromString()
        {
            MediaTypes.TYPE Type;
            Enum.TryParse(_tabAddViewModel.MediaType, out Type);
            return Type;

        }

        private void promptError()
        {
            MessageBox.Show("Filetype should be 'SRT'");
        }

        private bool IsFileTypeCompatible(string location)
        {
            string fileExt = System.IO.Path.GetExtension(location);
            if (getMediaTypeFromString() == MediaTypes.TYPE.Book)
            {
                if (fileExt == ".pdf")
                {
                    return true;
                }
            }
            
            if (fileExt == ".srt")
            {
                return true;
            }
            return false;
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
