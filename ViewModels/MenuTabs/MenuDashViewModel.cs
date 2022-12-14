using SubProgWPF.Commands;
using SubProgWPF.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SubProgWPF.ViewModels
{
    public class MenuDashViewModel : ViewModelBase
    {
        DashModel _dashModel;
        private readonly ICommand _tabDashCommand;



        public ICommand TabDashCommand => _tabDashCommand;
        public ObservableCollection<DashUnfinishedMediaModel> UnfinishedMedia { get => _dashModel.UnfinishedMediaList; }

        private string _date;
        public string TotalTVEpisodes { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("TVEpisodes")).MediaCount; }
        public string TotalTVWords { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("TVEpisodes")).WordCount; }
        public string TotalMovies { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("Movies")).MediaCount; }
        public string TotalMovieWords { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("Movies")).WordCount; }
        public string TotalYoutubeVideos { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("Youtube")).MediaCount; }
        public string TotalYoutubeWords { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("Youtube")).WordCount; }
        public string Date { get => _date; set => _date = value; }
        public string TotalMediaCount { get => _dashModel.TotalMediaCount.ToString(); }
        public string TotalMediaWords { get => _dashModel.getTotalWordCount(); }
        public string TotalBooks { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("Books")).MediaCount; }
        public string TotalBookWords { get => _dashModel.DashMediaOverviews.FirstOrDefault(a => a.Name.Equals("Books")).WordCount; }

        public MenuDashViewModel()
        {
            _dashModel = new DashModel();
            _tabDashCommand = new TabDashCommand(this);

            setDashItemProperties();

            
        }

        private void setDashItemProperties()
        {

            _date = DateTime.Now.DayOfWeek.ToString();
            _date = "Dashboard";
        }

        public override void updateTheFields()
        {
            setDashItemProperties();
            _dashModel.createUnfinishedMediaList();
            OnPropertyChanged(nameof(UnfinishedMedia));
        }
        
        
    }
}
