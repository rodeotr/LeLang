using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace SubProgWPF.Models
{
    public class MediaModel
    {
        static ObservableCollection<MediaMember> mediaMembers;
        public static string[] colors = new string[] { 
            "#00AD02", "#00A4AD", "#AD009B","#AD0029","#00AD58", "#69AD00" };
        public static ObservableCollection<MediaMember> GetMediaMembers()
        {
            mediaMembers = new ObservableCollection<MediaMember>();
            List<FTVEpisode> _episodes = MediaServices.getTVEpisodes();
            List<FYoutube> _youtube = MediaServices.getAllYoutubeVideos();
            List<Books> _books = MediaServices.getAllBooks();
            addTVMembers(_episodes);
            addYoutubeMembers(_youtube);
            addBookMembers(_books);
            
            return mediaMembers;
        }

        private static void addBookMembers(List<Books> books)
        {
            Random r = new Random();
            for (int i = 0; i < books.Count; i++)
            {
                Books e = books[i];
                MediaMember m = new MediaMember
                {
                    Number = (mediaMembers.Count + 1).ToString(),
                    Name = e.Name,
                    MediaLinkedIcon = "CheckboxOutline",
                    MediaIcon = "Book",
                    Visibility = false,
                    TotalLearnedWords = "8",
                    FirstLetter = e.Name.Substring(0, 1),
                    FirstLetterBackgroundColor = colors[r.Next(colors.Length)],
                    TextDecoration = e.IsFinished ? "StrikeThrough" : "None"
                };
                mediaMembers.Add(m);
            }
        }

        private static void addYoutubeMembers(List<FYoutube> _youtube)
        {
            Random r = new Random();
            for (int i = 0; i < _youtube.Count; i++)
            {
                FYoutube e = _youtube[i];
                MediaMember m = new MediaMember
                {
                    Number = (mediaMembers.Count + 1).ToString(),
                    Name = e.Name,
                    MediaLinkedIcon = "CheckboxOutline",
                    MediaIcon = "Youtube",
                    Visibility = false,
                    TotalLearnedWords = e.TotalLearnedWords,
                    FirstLetter = e.Name.Substring(0, 1),
                    FirstLetterBackgroundColor = colors[r.Next(colors.Length)],
                    TextDecoration = e.IsFinished ? "StrikeThrough" : "None"

                };
                mediaMembers.Add(m);
            }
        }

        private static void addTVMembers(List<FTVEpisode> _episodes)
        {
            Random r = new Random();
            for (int i = 0; i < _episodes.Count; i++)
            {
                FTVEpisode e = _episodes[i];
                TranscriptionAddress tA = TranscriptionServices.getTranscriptionByID(e.TranscriptionAddress_Id);
                MediaMember m = new MediaMember
                {
                    Number = (mediaMembers.Count + 1).ToString(),
                    Name = e.Season.Series.Name + ", Season: " + e.Season.SeasonIndex + ", Episode: " + e.EpisodeIndex,
                    MediaLinkedIcon = tA.MediaLocation == null ? "Close" : "CheckboxOutline",
                    MediaIcon = "DesktopMac",
                    Visibility = tA.MediaLocation == null ? true : false,
                    TotalLearnedWords = e.TotalLearnedWords.ToString(),
                    FirstLetter = e.Season.Series.Name.Substring(0, 1),
                    FirstLetterBackgroundColor = colors[r.Next(colors.Length)],
                    TextDecoration = e.IsFinished ? "StrikeThrough" : "None"

                };
                mediaMembers.Add(m);
            }
        }
    }


    public class MediaMember : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _mediaLinkedIcon;
        public string Number { get; set; }
        public string Name { get; set; }
        public string TotalLearnedWords { get; set; }
        public string FirstLetter { get; set; }
        public string FirstLetterBackgroundColor { get; set; }
        public bool Visibility { get; set; }
        public string MediaIcon { get; set; }
        public string TextDecoration { get; set; }
        public string MediaLinkedIcon { get { return _mediaLinkedIcon; }
            set { _mediaLinkedIcon = value; OnPropertyChanged(nameof(MediaLinkedIcon)); }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
