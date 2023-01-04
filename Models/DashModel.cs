using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Interfaces.Dashboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SubProgWPF.Models
{
    public class DashModel
    {

        private DashboardCardItem _episodeCard;
        private DashboardCardItem _movieCard;
        private DashboardCardItem _bookCard;
        private DashboardCardItem _youtubeCard;
        private ObservableCollection<DashUnfinishedMediaModel> _unfinishedMediaList;

        public ObservableCollection<DashUnfinishedMediaModel> UnfinishedMediaList { get => _unfinishedMediaList; set => _unfinishedMediaList = value; }
        public DashboardCardItem YoutubeCard { get => _youtubeCard; set => _youtubeCard = value; }
        public DashboardCardItem TVEpisodeCard { get => _episodeCard; set => _episodeCard = value; }
        public DashboardCardItem MovieCard { get => _movieCard; set => _movieCard = value; }
        public DashboardCardItem BookCard { get => _bookCard; set => _bookCard = value; }

        public DashModel()
        {
            createUnfinishedMediaList();
        }

        public void createUnfinishedMediaList()
        {
            _unfinishedMediaList = new ObservableCollection<DashUnfinishedMediaModel>();

            List<FTVEpisode> e = MediaServices.getUnfinishedTVSeries();
            List<FYoutube> y = MediaServices.getUnfinishedYoutubeVideos();
            List<Books> b = MediaServices.getUnfinishedBooks();
            List<Books> m = MediaServices.getUnfinishedBooks();




            foreach (FTVEpisode ee in e)
            {
                string transcriptionLocation = ee.TranscriptionAddress.TranscriptionLocation;
                string name = ee.Name + " S" + ee.Season.SeasonIndex.ToString().PadLeft(2, '0') +
                    "E" + ee.EpisodeIndex.ToString().PadLeft(2, '0');

                DashUnfinishedMediaModel model = new DashUnfinishedMediaModel()
                {
                    Type = MediaTypes.TYPE.TVSeries,
                    MediaIdInDB = ee.Id,
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
                    Type = MediaTypes.TYPE.Youtube,
                    MediaIdInDB = yy.Id,
                    Name = yy.Name,
                    IconKind = "Youtube",
                    Progress = TranscriptionServices.getYoutubeMediaProgress(yy.Link),
                    IconBackgroundColor = "#c4061f"


                };
                _unfinishedMediaList.Add(model);
            }
            foreach (Books bb in b)
            {
                string transcriptionLocation = bb.TranscriptionAddress.TranscriptionLocation;
                DashUnfinishedMediaModel model = new DashUnfinishedMediaModel()
                {
                    Type = MediaTypes.TYPE.Book,
                    MediaIdInDB = bb.Id,
                    Name = bb.Name,
                    IconKind = "Book",
                    Progress = TranscriptionServices.getMediaProgress(transcriptionLocation),
                    IconBackgroundColor = "#1c9e02"
                };
                _unfinishedMediaList.Add(model);
            }


        }


        public DashboardCardItem createYoutubeCard(int mediaCount, int wordCount)
        {
            return new DashboardCardItem()
            {
                MediaKind = MediaTypes.TYPE.Youtube.ToString(),
                MediaCount = mediaCount.ToString(),
                WordCount = wordCount.ToString(),
                IconKind = "Youtube",
                BackgroundImagePath = "/Assets/Images/despacito.jpg"
            };
        }

        public DashboardCardItem createMoviesCard(int mediaCount, int wordCount)
        {
            List<FMovies> movies = MediaServices.getAllMovies();
            return new DashboardCardItem()
            {
                MediaKind = MediaTypes.TYPE.TVSeries.ToString(),
                MediaCount = movies.Count.ToString(),
                WordCount = MediaServices.getMovieWords().Count.ToString(),
                IconKind = "FilmStrip",
                BackgroundImagePath = "/Assets/Images/harry.jpg"
            };
        }

        public DashboardCardItem createTVEpisodeCard(int mediaCount, int wordCount)
        {
            List<FTVEpisode> episodeList = MediaServices.getTVEpisodes();
            return new DashboardCardItem()
            {
                MediaKind = MediaTypes.TYPE.TVSeries.ToString(),
                MediaCount = episodeList.Count.ToString(),
                WordCount = MediaServices.getTVWords().Count.ToString(),
                IconKind = "DesktopMac",
                BackgroundImagePath = "/Assets/Images/got.jpg"
            };
        }
        
        public DashboardCardItem createBooksCard(int mediaCount, int wordCount)
        {
            List<Books> books = MediaServices.getAllBooks();
            return new DashboardCardItem()
            {
                MediaKind = MediaTypes.TYPE.Youtube.ToString(),
                MediaCount = books.Count.ToString(),
                WordCount = MediaServices.getBookWords().Count.ToString(),
                IconKind = "Book",
                BackgroundImagePath = "/Assets/Images/book1.png"
            };
        }

    }

    
    public class DashUnfinishedMediaModel
    {
        public MediaTypes.TYPE Type { get; set; }
        public int MediaIdInDB { get; set; }
        public string Name { get; set; }
        public string IconKind { get; set; }
        public string IconBackgroundColor { get; set; }
        public string LastReview { get; set; }
        public int Progress { get; set; }
    }



}
