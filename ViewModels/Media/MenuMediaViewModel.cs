using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Commands;
using SubProgWPF.Commands.Media;
using SubProgWPF.Models;
using SubProgWPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace SubProgWPF.ViewModels.Media
{
    public class MenuMediaViewModel : ViewModelBase
    {
        ObservableCollection<MediaMember> _mediaCollection;
        

        private readonly ICommand _tabMediaCommand;
        private bool _mediaVisibility;
        private string _mediaLocation;
        private string _stringIndex;
        private string _pageNum = "1";
        public ICommand TabMediaCommand => _tabMediaCommand;
        public ObservableCollection<MediaMember> Episodes { get { return _mediaCollection; } set { Episodes = value; OnPropertyChanged(nameof(Episodes)); } }
        public string PageNum { get => _pageNum; set => _pageNum = value; }
        public bool MediaVisibility { get => _mediaVisibility; set => _mediaVisibility = value; }

        public string MediaLocation { 
            get { return _mediaLocation; } 
            set { _mediaLocation = value;
                MediaServices.setMediaLocation(SelectedIndex, MediaLocation);
                
                OnPropertyChanged(nameof(MediaLocation));
            }
        }
        public string SelectedIndex { get { return _stringIndex; } set {
                _stringIndex = value;
                _mediaCollection[Int32.Parse(_stringIndex) - 1].MediaLinkedIcon = "CheckboxOutline";

            } }
       
        public MenuMediaViewModel(NavigationStore navigationStore)
        {
            _tabMediaCommand = new TabMediaCommand(this);
            _mediaCollection = MediaModel.GetMediaMembers();
            _mediaVisibility = _mediaCollection.Count == 0 ? false : true;
        }

        public override void updateTheFields()
        {
            _mediaCollection = MediaModel.GetMediaMembers();
            
        }
    }
}
