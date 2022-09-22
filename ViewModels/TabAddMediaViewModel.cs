using LangDataAccessLibrary;
using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class TabAddMediaViewModel : ViewModelBase
    {
        private AddMediaModel _addMediaModel;
        private TabLearnViewModel _learnViewModel;
        private readonly ICommand _tabAddCommand;
        private string[] _existingTVSeriesNames;
        private bool _tvVisibility = false;


        public TabAddMediaViewModel(TabLearnViewModel learnViewModel)
        {
            _learnViewModel = learnViewModel;
            _addMediaModel = new AddMediaModel();
            _tabAddCommand = new TabAddCommand(this);
        }

        /// <summary>
        /// Selected Media Type
        /// </summary>
        public string MediaType
        {
            get { return _addMediaModel.TypeStr; }
            set {_addMediaModel.TypeStr = value; 
                OnPropertyChanged(nameof(MediaType));
                if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.TVSeries.ToString()))
                {
                    TVVisibility = true;
                    ExistingTVSeriesNames = ExistingSeriesNames;
                    OnPropertyChanged(nameof(ExistingTVSeriesNames));
                }
                else { TVVisibility = false;
                    ExistingTVSeriesNames = null; 
                    OnPropertyChanged(nameof(ExistingTVSeriesNames)); }
            }
        }

        /// <summary>
        /// For Listing Existing Media Types
        /// </summary>
        public string[] MediaTypes { get { return System.Enum.GetNames(typeof(MediaTypes.TYPE));} }

        public string SeasonIndex { get => _addMediaModel.SeasonIndex; set => _addMediaModel.SeasonIndex = value; }
        public string EpisodeIndex { get => _addMediaModel.EpisodeIndex; set => _addMediaModel.EpisodeIndex = value; }

        public string[] ExistingTVSeriesNames
        {
            get { return _existingTVSeriesNames; }
            set { _existingTVSeriesNames = value; }
        }

        public string[] ExistingSeriesNames { get { return MediaServices.getAllTVSeriesNames(); } }
        public string TranscriptionLocation
        {
            get { return _addMediaModel.TranscriptionLocation; }
            set { _addMediaModel.TranscriptionLocation = value; OnPropertyChanged(nameof(TranscriptionLocation)); }
        }
        public string MaxWordFreq
        {
            get { return _addMediaModel.MaxWordFrequency; }
            set { _addMediaModel.MaxWordFrequency = value; }
        }
        public bool TVVisibility
        {
            get { return _tvVisibility; }
            set { _tvVisibility = value; OnPropertyChanged(nameof(TVVisibility)); }
        }
        public ICommand TabAddCommand => _tabAddCommand;
        public string SelectedMediaName { get => _addMediaModel.MediaName; set { _addMediaModel.MediaName = value;OnPropertyChanged(nameof(SelectedMediaName)); } }
        
        public void launchNewWordsTab(DataGridNewWordModel dataGridNewWordModel)
        {
            _learnViewModel.launchDataGrid(dataGridNewWordModel);
        }

        }



    }

