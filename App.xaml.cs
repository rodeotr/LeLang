using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Models;
using SubProgWPF.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SubProgWPF.Stores;

using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Vlc.DotNet.Wpf;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.ComponentModel;
using LangDataAccessLibrary.Services;

namespace SubProgWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly NavigationStore _navigationStore;
        Vlc.DotNet.Core.VlcMediaPlayer mediaPlayer;
        BackgroundWorker worker;

        public App()
        {
            //IServiceCollection services = new ServiceCollection();
            //services.AddSingleton<NavigationStore>();
            _navigationStore = new NavigationStore();
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = false;
            worker.RunWorkerAsync();

        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("");
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            //mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(new DirectoryInfo(@"C:\\Program Files (x86)\\VideoLAN\\VLC"));
            mediaPlayer = null;
        }

        protected override void OnStartup(StartupEventArgs e)
        {



            //using (WordContextDB dbContext = wordDbContextFactory.CreateDbContext())
            //{
            //    dbContext.Database.Migrate();
            //}


            //string path = "C:\\Users\\Dean\\Desktop\\PDF\\Bhagavad Gita.pdf";
            //LangUtils.GetAllWordObjectsFromPDF(path, 1, 10, 3);





            //Vlc.DotNet.Core.VlcMediaPlayer mediaPlayer = null;

            bool IsPlaying = false;
            // time in seconds

            //mediaPlayer.SetMedia(new Uri("C:\\Users\\Dean\\Desktop\\The.Big.Bang.Theory.S03.Season.3.Complete.720p.HDTV.x264-[maximersk]\\The.Big.Bang.Theory.S06E02.720p.HDTV.X264-MRSK.mkv"), new string[] { ":no-audio", ":start-time=7" });
            //mediaPlayer.Play();
            //IsPlaying = true;









            //MediaServices.getTotalLearnedWordsOfAnEpisode(1);

            _navigationStore.CurrentViewModel = CreateDashBoardViewModel();
            MainWindow window = new MainWindow(){ DataContext = new MainViewModel(_navigationStore, mediaPlayer, IsPlaying)};
            window.Show();

            base.OnStartup(e);
        }

      

        




        private ViewModelBase CreateDashBoardViewModel()
        {
            return new TabDashViewModel(_navigationStore);
        }

        
    }
}
