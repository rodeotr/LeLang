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

namespace SubProgWPF.ViewModels
{
    public class TabContinueMediaViewModel : ViewModelBase
    {
        private AddMediaModel _addMediaModel;
        private TabLearnViewModel _learnViewModel;
        private List<FTVEpisode> _episodes;
        private ICommand _tabContinueCommand;
        private string[] _mediaNames;


        public TabContinueMediaViewModel(TabLearnViewModel learnViewModel)
        {
            _learnViewModel = learnViewModel;
            _tabContinueCommand = new TabContinueCommand(this);
            _addMediaModel = new AddMediaModel();
            _episodes = MediaServices.getUnfinishedTVSeries();
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
                OnPropertyChanged(nameof(MediaType));
                if (value.Equals(LangDataAccessLibrary.MediaTypes.TYPE.TVSeries.ToString()))
                {
                }
            }
        }

        public string[] MediaNames { get => _mediaNames; set{ _mediaNames = value; OnPropertyChanged(nameof(MediaNames)); }  }

        public List<FTVEpisode> Episodes { get => _episodes; set => _episodes = value; }
        public AddMediaModel AddMediaModel { get => _addMediaModel; set => _addMediaModel = value; }


        private void setMediaNames()
        {
            List<string> list = new List<string>();
            foreach (FTVEpisode e in _episodes)
            {
                string s = e.Name + ", " + "Season : " + e.Season.SeasonIndex + ", Episode : " + e.EpisodeIndex;
                list.Add(s);
            }
            MediaNames = list.ToArray();
        }

        public void launchGridView(DataGridNewWordModel dataGridNewWordModel)
        {
            _learnViewModel.launchDataGrid(dataGridNewWordModel);
            
        }

    }


}
