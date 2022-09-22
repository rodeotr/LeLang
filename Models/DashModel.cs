using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubProgWPF.Models
{
    public class DashModel
    {
        List<IDashMedia> _tvSeries;
        List<IDashMedia> _tvEpisodes;
        List<IDashMedia> _movies;
        List<IDashMedia> _podcastAndVideos;

        public DashModel()
        {
            _tvSeries = new List<IDashMedia>();
            _tvEpisodes = new List<IDashMedia>();
            _movies = new List<IDashMedia>();
            _podcastAndVideos = new List<IDashMedia>();

            createTVSeriesList();
            createTVEpisodesList();
            createMoviesList();
        }

        public List<IDashMedia> TvSeries { get => _tvSeries; set => _tvSeries = value; }
        public List<IDashMedia> TvEpisodes { get => _tvEpisodes; set => _tvEpisodes = value; }
        public List<IDashMedia> Movies { get => _movies; set => _movies = value; }
        public List<IDashMedia> PodcastAndVideos { get => _podcastAndVideos; set => _podcastAndVideos = value; }

        private void createMoviesList()
        {
            //List<FMovies> movies = MediaServices.getAllMovies();
            //foreach (FMovies m in movies)
            //{
            //    _movies.Add(new DashMovie() { Name = m.Name, IsFinished = false, LearnedTotalWords = 0 });
            //}
        }

        private void createTVEpisodesList()
        {
            List<FTVEpisode> episodes = MediaServices.getAllTVEpisodes();
            foreach (FTVEpisode e in episodes)
            {
                _tvEpisodes.Add(new DashTVEpisode() { Name = e.Name, IsFinished = e.IsFinished, LearnedTotalWords = 0 });
            }
        }

        private void createTVSeriesList()
        {
            List<FTVSeries> series = MediaServices.getAllTVSeries();
            foreach(FTVSeries s in series)
            {
                _tvSeries.Add(new DashTVSerie() { Name = s.Name, EpisodeCount = 0, IsFinished = s.IsFinished, LearnedTotalWords = 0 });
            }
        }
    }

    public class DashTVSerie : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
        public int EpisodeCount { get; set; }
    }
    public class DashTVEpisode : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
    }
    public class DashMovie : IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }

    }

    
}
