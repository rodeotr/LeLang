using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learning
{
    public class TabContinueMediaViewModel : ViewModelBase
    {
        public BackgroundWorker _worker;
        private AddMediaModel _addMediaModel;
        private TabLearnViewModel _learnViewModel;
        private List<FTVEpisode> _episodes;
        private List<FYoutube> _youtubeVideos;
        private List<Books> _books;
        private List<string> _episodeStringList;
        private List<string> _youtubeStringList;
        private List<string> _booksStringList;
        private ICommand _tabContinueCommand;
        private string[] _mediaNames;
        private bool _isLoading;
        public int _transcriptionId;
        public ListWordsModel _dataGridNewWordModel;



        public TabContinueMediaViewModel(TabLearnViewModel learnViewModel)
        {
            _worker = new BackgroundWorker();
            _learnViewModel = learnViewModel;
            _tabContinueCommand = new TabContinueCommand(this);
            _addMediaModel = new AddMediaModel();

            _isLoading = false;
            setWorkerProperties();
            setMediaNames();
        }

        public ICommand TabContinueCommand => _tabContinueCommand;

        public string[] MediaTypes { get { return System.Enum.GetNames(typeof(MediaTypes.TYPE)); } }
        public string SelectedMediaName { get => _addMediaModel.MediaName; set { _addMediaModel.MediaName = value; OnPropertyChanged(nameof(SelectedMediaName)); } }

        public string MaxWordFreq { get => _addMediaModel.MaxWordFrequency; set { _addMediaModel.MaxWordFrequency = value; OnPropertyChanged(nameof(MaxWordFreq)); } }

        public string MediaType
        {
            get { return _addMediaModel.TypeStr; }
            set
            {
                _addMediaModel.TypeStr = value;
                MediaNames = null;

                if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.TVSeries.ToString()))
                {
                    MediaNames = _episodeStringList.ToArray();
                }else if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.Youtube.ToString()))
                {
                    MediaNames = _youtubeStringList.ToArray();
                }
                else if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.Book.ToString()))
                {
                    MediaNames = _booksStringList.ToArray();
                }
                OnPropertyChanged(nameof(MediaType));

            }
        }

        public string[] MediaNames { get => _mediaNames; set{ _mediaNames = value; OnPropertyChanged(nameof(MediaNames)); }  }

        public List<FTVEpisode> Episodes { get => _episodes; set => _episodes = value; }
        public List<FYoutube> YoutubeVideos { get => _youtubeVideos; set => _youtubeVideos = value; }
        public List<Books> Books { get => _books; set => _books = value; }
        public AddMediaModel AddMediaModel { get => _addMediaModel; set => _addMediaModel = value; }
        public bool IsLoading { get => _isLoading; set { 
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading)); } }

        private void setMediaNames()
        {
            _episodes = MediaServices.getUnfinishedTVSeries();
            _youtubeVideos = MediaServices.getUnfinishedYoutubeVideos();
            _books = MediaServices.getUnfinishedBooks();

            _episodeStringList = new List<string>();
            _youtubeStringList = new List<string>();
            _booksStringList = new List<string>();

            foreach (FTVEpisode e in _episodes)
            {
                string s = e.Name + ", " + "Season : " + e.Season.SeasonIndex + ", Episode : " + e.EpisodeIndex;
                _episodeStringList.Add(s);
            }
            foreach (FYoutube e in _youtubeVideos)
            {
                string s = e.Name;
                _youtubeStringList.Add(s);
            }
            foreach (Books e in _books)
            {
                string s = e.Name;
                _booksStringList.Add(s);
            }
        }

        public void launchGridView(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {

            _learnViewModel.launchNewWordGridContinue(dataGridNewWordModel, transcriptionId);
            _isLoading = false;
        }

        private void setWorkerProperties()
        {
            _worker.DoWork += worker_DoWork;
            _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsLoading = false;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            launchGridView(_dataGridNewWordModel, _transcriptionId);
        }

        public override void updateTheFields()
        {
            setMediaNames();
        }
    }


}
