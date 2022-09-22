using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using SubProgWPF.ViewModels;

namespace SubProgWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            //vlcPlayer.BeginInit();
            //vlcPlayer.SourceProvider.CreatePlayer(new DirectoryInfo(@"C:\\Program Files (x86)\\VideoLAN\\VLC"), new string[] { ":no-audio", ":start-time=7" });
            //vlcPlayer.SourceProvider.MediaPlayer.Play("C:\\Users\\Dean\\Desktop\\The.Big.Bang.Theory.S03.Season.3.Complete.720p.HDTV.x264-[maximersk]\\The.Big.Bang.Theory.S05E01.720p.HDTV.ReEnc-Max.mkv");


        }




        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    var vm = (MainViewModel)this.DataContext;
                    vm.MediaPlayer.Stop();
                    Console.WriteLine("Escape pressed");
                    break;
            }
        }


        public class Member
        {
            public string Character { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string Position { get; set; }
            public string Context { get; set; }
            public string Phone { get; set; }
            public Brush BGColor { get; set; }

        }


    }
}
