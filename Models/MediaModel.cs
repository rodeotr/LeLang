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
        public static ObservableCollection<MediaMember> GetMediaMembers()
        {
            List<FTVEpisode> _episodes = MediaServices.getTVEpisodes();
            ObservableCollection<MediaMember> mC = new ObservableCollection<MediaMember>();
            for (int i = 0; i < _episodes.Count; i++)
            {
                FTVEpisode e = _episodes[i];
                TranscriptionAddress tA = MediaServices.getTranscriptionByID(e.TranscriptionAddress_Id);
                MediaMember m = new MediaMember { Number = (i + 1).ToString(),
                    Name = e.Season.Series.Name + ", Season: " + e.Season.SeasonIndex + ", Episode: " + e.EpisodeIndex,
                    MediaLinkedIcon = tA.MediaLocation == null ? "Close" : "CheckboxOutline"
                };
                mC.Add(m);
            }
            return mC;
        }
    }


    public class MediaMember : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _mediaLinkedIcon;
        public string Number { get; set; }
        public string Name { get; set; }
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
