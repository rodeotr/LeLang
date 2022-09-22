using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class TabMediaViewModel : ViewModelBase
    {
        ObservableCollection<MediaMember> _episodesCollection;

        private readonly ICommand _tabMediaCommand;
        private string _mediaLocation;
        private string _stringIndex;
        public ICommand TabMediaCommand => _tabMediaCommand;
        public string MediaLocation { 
            get { return _mediaLocation; } 
            set { _mediaLocation = value;
                MediaServices.setMediaLocation(SelectedIndex, MediaLocation);
                
                //Episodes = MediaModel.GetMediaMembers();
                OnPropertyChanged(nameof(MediaLocation));
                


            }
        }
        public string SelectedIndex { get { return _stringIndex; } set {
                _stringIndex = value;
                _episodesCollection[Int32.Parse(_stringIndex) - 1].MediaLinkedIcon = "CheckboxOutline";

            } }
        public ObservableCollection<MediaMember> Episodes { get { return _episodesCollection; } set { Episodes = value; OnPropertyChanged(nameof(Episodes)); } }

        public TabMediaViewModel(NavigationStore navigationStore)
        {
            _tabMediaCommand = new TabMediaCommand(this);
            _episodesCollection = MediaModel.GetMediaMembers();
        }


    }
}
