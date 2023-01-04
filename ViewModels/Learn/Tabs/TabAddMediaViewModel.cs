using LangDataAccessLibrary;
using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Learn;
using SubProgWPF.Models;
using SubProgWPF.ViewModels.Learn.Tabs.AddMediaOptions;
using SubProgWPF.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Learn.Tabs
{
    public class TabAddMediaViewModel : ViewModelBase
    {
        private readonly AddMediaModel _addMediaModel;
        private readonly MenuLearnViewModel _learnViewModel;
        private ViewModelBase _currentViewModel;
        private List<ChildViewModel> _childViewModels;
        private ChildViewModel _currentChildViewModel;
        private readonly ICommand _tabAddCommand;
        private LoadingWindow _loadingWindow;
        private Progress progress;



        public ICommand TabAddCommand => _tabAddCommand;
        public ViewModelBase CurrentViewModel { get => _currentViewModel; set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); } }
        public AddMediaModel AddMediaModel => _addMediaModel;
        public ChildViewModel CurrentChildViewModel { get => _currentChildViewModel; set { _currentChildViewModel = value; OnPropertyChanged(nameof(CurrentChildViewModel)); } }
        public Progress Progress { get => progress; set { progress = value; OnPropertyChanged(nameof(Progress)); } }
        public string[] MediaTypes { get { return System.Enum.GetNames(typeof(MediaTypes.TYPE)); } }
        public string SelectedMediaName { get => _addMediaModel.MediaName; set { _addMediaModel.MediaName = value; OnPropertyChanged(nameof(SelectedMediaName)); } }
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


        public TabAddMediaViewModel(MenuLearnViewModel learnViewModel)
        {
            progress = new Progress() {};
            _learnViewModel = learnViewModel;
            _addMediaModel = new AddMediaModel();
            _tabAddCommand = new TabAddCommand(this);

            setMediaViewModels();
            
            

        }
        private void setMediaViewModels()
        {
            _childViewModels = new List<ChildViewModel>();
            _childViewModels.Add(new ChildViewModel()
            {
                Type = LangDataAccessLibrary.MediaTypes.TYPE.TVSeries,
                ViewModel = new TabAddMediaTVSeriesViewModel(),
                ExistingMedia = MediaServices.getAllTVSeriesNames()
            });
            _childViewModels.Add(new ChildViewModel()
            {
                Type = LangDataAccessLibrary.MediaTypes.TYPE.Youtube,
                ViewModel = new TabAddMediaYoutubeViewModel()
            });
            _childViewModels.Add(new ChildViewModel()
            {
                Type = LangDataAccessLibrary.MediaTypes.TYPE.Book,
                ViewModel = new TabAddMediaBookViewModel()
            });
            _childViewModels.Add(new ChildViewModel()
            {
                Type = LangDataAccessLibrary.MediaTypes.TYPE.Text,
                ViewModel = new TabAddMediaTextViewModel()
            });
            _childViewModels.Add(new ChildViewModel()
            {
                Type = LangDataAccessLibrary.MediaTypes.TYPE.Movie,
                ViewModel = null
            });

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
            //ExistingMediaNames = null;
            CurrentViewModel = null;
            ChildViewModel viewModel = _childViewModels.FirstOrDefault(a => a.Type.ToString().Equals(value));
            if(viewModel != null)
            {
                CurrentChildViewModel = viewModel;
                CurrentViewModel = viewModel.ViewModel;
            }
            
        }

   
      
        public void switchToNewWordsTab(ListWordsModel dataGridNewWordModel, int transcriptionId)
        {
            _learnViewModel.switchToNewWordsTab(dataGridNewWordModel, transcriptionId);
        }
        public void launchProgresBar()
        {
            _loadingWindow = new LoadingWindow();
            _loadingWindow.DataContext = Progress;

            _loadingWindow.Show();
        }

        public void closeProgresBar()
        {
            _loadingWindow.Close();
        }

        public override void updateTheFields()
        {
            ChildViewModel viewModel = _childViewModels.FirstOrDefault(a => a.Type == LangDataAccessLibrary.MediaTypes.TYPE.TVSeries);
            viewModel.ExistingMedia = MediaServices.getAllTVSeriesNames();
        }
    }
    public class ChildViewModel
    {
        public MediaTypes.TYPE Type { get; set; }
        public ViewModelBase ViewModel { get; set; }
        public string[] ExistingMedia { get; set; }
    }
    public class Progress
    {
        public string CurrentWord { get; set; }
        public string CurrentTime { get; set; }
        public string CurrentProgress { get; set; }
    }

    }

