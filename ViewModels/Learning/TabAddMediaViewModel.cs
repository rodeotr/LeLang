using LangDataAccessLibrary;
using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.ViewModels.Learning.AddMediaOptions;
using SubProgWPF.Windows;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learning
{
    public class TabAddMediaViewModel : ViewModelBase
    {
        private readonly AddMediaModel _addMediaModel;
        private readonly TabLearnViewModel _learnViewModel;
        private ViewModelBase _currentViewModel;
        private ViewModelBase _tvSeriesViewModel;
        private ViewModelBase _youtubeViewModel;
        private ViewModelBase _bookViewModel;
        private ViewModelBase _textViewModel;

        private readonly ICommand _tabAddCommand;
        private LoadingWindow _loadingWindow;

        private string _progressText = "";
        private string _progressValue = "0";
        private string[] _existingMediaNames;
        private string[] _existingTVSeriesNames;


        public ICommand TabAddCommand => _tabAddCommand;
        public ViewModelBase CurrentViewModel { get => _currentViewModel; set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); } }
        public AddMediaModel AddMediaModel => _addMediaModel;
        public string[] MediaTypes { get { return System.Enum.GetNames(typeof(MediaTypes.TYPE)); } }
        public string[] ExistingMediaNames { get { return _existingMediaNames; } set { _existingMediaNames = value; OnPropertyChanged(nameof(ExistingMediaNames)); } }
        public string SelectedMediaName { get => _addMediaModel.MediaName; set { _addMediaModel.MediaName = value; OnPropertyChanged(nameof(SelectedMediaName)); } }
        public string ProgressText { get => _progressText; set { _progressText = value; OnPropertyChanged(nameof(ProgressText)); } }
        public string ProgressValue { get => _progressValue; set { _progressValue = value; OnPropertyChanged(nameof(ProgressValue)); } }
        public string TranscriptionLocation
        {
            get { return _addMediaModel.TranscriptionLocation; }
            set
            {
                _addMediaModel.TranscriptionLocation = value;
                OnPropertyChanged(nameof(TranscriptionLocation));
            }
        }
        public string MaxWordFreq
        {
            get { return _addMediaModel.MaxWordFrequency; }
            set { _addMediaModel.MaxWordFrequency = value; }
        }


        public TabAddMediaViewModel(TabLearnViewModel learnViewModel)
        {
            _learnViewModel = learnViewModel;
            _addMediaModel = new AddMediaModel();
            _tabAddCommand = new TabAddCommand(this);

            _existingTVSeriesNames = MediaServices.getAllTVSeriesNames();
            setMediaViewModels();
            

        }
        private void setMediaViewModels()
        {
            _tvSeriesViewModel = new TabAddMediaTVSeriesViewModel();
            _youtubeViewModel = new TabAddMediaYoutubeViewModel();
            _bookViewModel = new TabAddMediaBookViewModel();
            _textViewModel = new TabAddMediaTextViewModel();
        }

        public string MediaType
        {
            get { return _addMediaModel.TypeStr; }
            set {_addMediaModel.TypeStr = value; 
                OnPropertyChanged(nameof(MediaType));

                setCurrentViewModel(value);
                
               
                

            }
        }


        private void setCurrentViewModel(string value)
        {
            ExistingMediaNames = null;
            CurrentViewModel = null;
            if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.TVSeries.ToString()))
            {
                CurrentViewModel = _tvSeriesViewModel;
                ExistingMediaNames = _existingTVSeriesNames;
            }
            else if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.Youtube.ToString()))
            {
                CurrentViewModel = _youtubeViewModel;
            }
            else if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.Book.ToString()))
            {
                CurrentViewModel = _bookViewModel;
            }
            else if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.Text.ToString()))
            {
                CurrentViewModel = _textViewModel;
            }
        }

   
      
        public void switchToNewWordsTab(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {
            _learnViewModel.switchToNewWordsTab(dataGridNewWordModel, transcriptionId);
        }
        public void launchProgresBar()
        {
            _loadingWindow = new LoadingWindow();
            _loadingWindow.DataContext = this;

            _loadingWindow.Show();
        }

        public void closeProgresBar()
        {
            _loadingWindow.Close();
        }

        public override void updateTheFields()
        {
            _existingTVSeriesNames = MediaServices.getAllTVSeriesNames();
        }
    }

    }

