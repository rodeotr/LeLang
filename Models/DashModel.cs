using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using static SubProgWPF.ViewModels.MenuDashViewModel;

namespace SubProgWPF.Models
{
    public class DashModel
    {
        private List<DashStatisticsModel> _dashStatisticsModel;
        private int _totalMediaCount = 0;
        //List<IDashMedia> _tvSeries;
        //List<IDashMedia> _youtubeVideos;
        //List<IDashMedia> _tvEpisodes;
        //List<IDashMedia> _movies;
        //List<IDashMedia> _podcastAndVideos;
        //List<IDashMedia> _books;

        ObservableCollection<DashUnfinishedMediaModel> _unfinishedMediaList;

        public DashModel()
        {
            _dashStatisticsModel = new List<DashStatisticsModel>();

            createYoutubeList();
            createTVEpisodesList();
            createMoviesList();
            createBooksList();

            createUnfinishedMediaList();
        }


        public ObservableCollection<DashUnfinishedMediaModel> UnfinishedMediaList { get => _unfinishedMediaList; set => _unfinishedMediaList = value; }
        public List<DashStatisticsModel> DashMediaOverviews { get => _dashStatisticsModel; set => _dashStatisticsModel = value; }
        public int TotalMediaCount { get => _totalMediaCount; set => _totalMediaCount = value; }

        public void createUnfinishedMediaList()
        {
            _unfinishedMediaList = new ObservableCollection<DashUnfinishedMediaModel>();
            List<FTVEpisode> e = MediaServices.getUnfinishedTVSeries();
            List<FYoutube> y = MediaServices.getUnfinishedYoutubeVideos();
            List<Books> b = MediaServices.getUnfinishedBooks();
            foreach (FTVEpisode ee in e)
            {
                string transcriptionLocation = TranscriptionServices.getTranscriptionByID(ee.TranscriptionAddress_Id).TranscriptionLocation;
                string name = ee.Name + " S" + ee.Season.SeasonIndex.ToString().PadLeft(2, '0') +
                    "E" + ee.EpisodeIndex.ToString().PadLeft(2, '0');

                DashUnfinishedMediaModel model = new DashUnfinishedMediaModel()
                {
                    Name = name,
                    IconKind = "DesktopMac",
                    Progress = TranscriptionServices.getMediaProgress(transcriptionLocation),
                    IconBackgroundColor = "#271d80"
                };
                _unfinishedMediaList.Add(model);
            }
            foreach (FYoutube yy in y)
            {
                DashUnfinishedMediaModel model = new DashUnfinishedMediaModel()
                {
                    Name = yy.Name,
                    IconKind = "Youtube",
                    Progress = TranscriptionServices.getYoutubeMediaProgress(yy.Link),
                    IconBackgroundColor = "#c4061f"


                };
                _unfinishedMediaList.Add(model);
            }
            foreach (Books bb in b)
            {
                string transcriptionLocation = TranscriptionServices.getTranscriptionByID(bb.TranscriptionAddress_Id).TranscriptionLocation;
                DashUnfinishedMediaModel model = new DashUnfinishedMediaModel()
                {
                    Name = bb.Name,
                    IconKind = "Book",
                    Progress = TranscriptionServices.getMediaProgress(transcriptionLocation),
                    IconBackgroundColor = "#1c9e02"
                };
                _unfinishedMediaList.Add(model);
            }


        }

        private void createMoviesList()
        {
            List<FMovies> movies = MediaServices.getAllMovies();
            List<IDashMedia> _movies = new List<IDashMedia>();
            foreach (FMovies m in movies)
            {
                _movies.Add(new DashMovie() { Name = m.Name, IsFinished = false, LearnedTotalWords = 0 });
                _totalMediaCount += 1;
            }
            _dashStatisticsModel.Add(new DashStatisticsModel()
            {
                Name = "Movies",
                MediaCount = _movies.Count.ToString(),
                WordCount = MediaServices.getMovieWords().Count.ToString()
            });
        }

        private void createTVEpisodesList()
        {
            List<FTVEpisode> episodes = MediaServices.getTVEpisodes();
            List<IDashMedia> _tvEpisodes = new List<IDashMedia>();
            foreach (FTVEpisode e in episodes)
            {
                string transcriptionLocation = TranscriptionServices.getTranscriptionByID(e.TranscriptionAddress_Id).TranscriptionLocation;
                _tvEpisodes.Add(new DashTVEpisode() { 
                    Name = e.Name, 
                    IsFinished = e.IsFinished, 
                    LearnedTotalWords = 0,
                    MediaProgress = TranscriptionServices.getMediaProgress(transcriptionLocation)
                });
                _totalMediaCount += 1;
            }
            _dashStatisticsModel.Add(new DashStatisticsModel()
            {
                    Name = "TVEpisodes",
                    MediaCount = _tvEpisodes.Count.ToString(),
                    WordCount = MediaServices.getTVWords().Count.ToString()
            });
        }


        private void createYoutubeList()
        {
            List<FYoutube> series = MediaServices.getAllYoutubeVideos();
            List<IDashMedia> _youtubeVideos = new List<IDashMedia>();
            foreach (FYoutube y in series)
            {
                _youtubeVideos.Add(new DashTVSerie() {
                    Name = y.Name, 
                    IsFinished = y.IsFinished, 
                    LearnedTotalWords = 0,
                    MediaProgress = TranscriptionServices.getYoutubeMediaProgress(y.Link)
                });
                _totalMediaCount += 1;
            }
            _dashStatisticsModel.Add(new DashStatisticsModel()
            {
                    Name = "Youtube",
                    MediaCount = _youtubeVideos.Count.ToString(),
                    WordCount = MediaServices.getYoutubeWords().Count.ToString()
            });
        }

        private void createBooksList()
        {
            List<Books> books = MediaServices.getAllBooks();
            List<IDashMedia> _books = new List<IDashMedia>();
            foreach (Books b in books)
            {
                _books.Add(new DashMovie() { Name = b.Name, IsFinished = false, LearnedTotalWords = b.TotalLearnedWords });
                _totalMediaCount += 1;
            }
            _dashStatisticsModel.Add(new DashStatisticsModel()
            {
                Name = "Books",
                MediaCount = _books.Count.ToString(),
                WordCount = MediaServices.getBookWords().Count.ToString()
            });
        }

        public string getTotalWordCount()
        {
            int count = 0;
            foreach(DashStatisticsModel d in _dashStatisticsModel)
            {
                count += Int32.Parse(d.WordCount);
            }
            return count.ToString();
        }
        
    }

    public class DashTVSerie : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
        public int EpisodeCount { get; set; }
        public int MediaProgress { get; set; }
    }

    public class DashTVEpisode : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
        public int MediaProgress { get; set; }

    }
    public class DashMovie : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
        public int MediaProgress { get; set; }
    }
    public class DashYoutube : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
        public int MediaProgress { get; set; }
    }
    public class DashStatisticsModel
    {
        public string Name { get; set; }
        public string MediaCount { get; set; }
        public string WordCount { get; set; }
    }
    public class DashUnfinishedMediaModel
    {
        public string Name { get; set; }
        public string IconKind { get; set; }
        public string IconBackgroundColor { get; set; }
        public string LastReview { get; set; }
        public int Progress { get; set; }
    }



}
