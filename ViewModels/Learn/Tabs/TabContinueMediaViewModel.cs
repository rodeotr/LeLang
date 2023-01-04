using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Learn.Tabs;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn.Tabs
{
    public class TabContinueMediaViewModel : ViewModelBase
    {
        public BackgroundWorker _worker;
        private AddMediaModel _addMediaModel;
        private MenuLearnViewModel _learnViewModel;
        private List<object> _unfinishedMedia;
        private List<ContinueMediaItem> _allItemList;
        //private List<FTVEpisode> _episodes;
        //private List<FYoutube> _youtubeVideos;
        //private List<Books> _books;
        //private List<string> _episodeStringList;
        //private List<string> _youtubeStringList;
        //private List<string> _booksStringList;
        private ICommand _tabContinueCommand;
        private string[] _mediaNames;
        private bool _isLoading;
        public int _transcriptionId;
        public ListWordsModel _dataGridNewWordModel;



        public TabContinueMediaViewModel(MenuLearnViewModel learnViewModel)
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
        public string SelectedMediaName { get => _addMediaModel.MediaName; set { _addMediaModel.MediaName = value; 
                OnPropertyChanged(nameof(SelectedMediaName)); } }

        public string MediaType
        {
            get { return _addMediaModel.TypeStr; }
            set
            {
                _addMediaModel.TypeStr = value;
                MediaTypes.TYPE Type;
                Enum.TryParse(_addMediaModel.TypeStr, out Type);
                List<ContinueMediaItem> items = _allItemList.Where(a => a.Type == Type).ToList();
                MediaNames = items.Select(a=>a.Name).ToArray();
                OnPropertyChanged(nameof(MediaType));

            }
        }

        public string[] MediaNames { get => _mediaNames; set{ _mediaNames = value; OnPropertyChanged(nameof(MediaNames)); }  }

        public AddMediaModel AddMediaModel { get => _addMediaModel; set { 
                _addMediaModel = value;
                OnPropertyChanged(nameof(AddMediaModel));
            } }
        public bool IsLoading { get => _isLoading; set { 
                _isLoading = value; 
                OnPropertyChanged(nameof(IsLoading)); } }

        public List<ContinueMediaItem> AllItemList { get => _allItemList; set => _allItemList = value; }
        public List<object> UnfinishedMedia { get => _unfinishedMedia; set => _unfinishedMedia = value; }

        private void setMediaNames()
        {
            _allItemList = new List<ContinueMediaItem>();
            _unfinishedMedia = new List<object>();

            List<FTVEpisode> _episodes = MediaServices.getUnfinishedTVSeries();
            List<FYoutube> _youtubeVideos = MediaServices.getUnfinishedYoutubeVideos();
            List<Books> _books = MediaServices.getUnfinishedBooks();

            _unfinishedMedia.Add(MediaServices.getUnfinishedTVSeries());
            _unfinishedMedia.Add(MediaServices.getUnfinishedYoutubeVideos());
            _unfinishedMedia.Add(MediaServices.getUnfinishedBooks());


            foreach (FTVEpisode e in _episodes)
            {
                _allItemList.Add(new ContinueMediaItem()
                {
                     Name = e.Name + ", " + "Season : " + e.Season.SeasonIndex + ", Episode : " + e.EpisodeIndex,
                     Type = LangDataAccessLibrary.MediaTypes.TYPE.TVSeries,
                     Media = e,
                     TranscriptionId = e.TranscriptionAddress.Id

                    
                });
            }
            foreach (FYoutube y in _youtubeVideos)
            {
                _allItemList.Add(new ContinueMediaItem()
                {
                    Name = y.Name,
                    Type = LangDataAccessLibrary.MediaTypes.TYPE.Youtube,
                    Media = y
                    
                });
            }
            foreach (Books b in _books)
            {
                _allItemList.Add(new ContinueMediaItem()
                {
                    Name = b.Name,
                    Type = LangDataAccessLibrary.MediaTypes.TYPE.Book,
                    Media = b,
                    TranscriptionId = b.TranscriptionAddress.Id
                });
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
    public class ContinueMediaItem
    {
        public string Name { get; set; }
        public MediaTypes.TYPE Type { get; set; }
        public object Media { get; set; }
        public int TranscriptionId { get; set; }
    }


}
