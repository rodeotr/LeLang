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
using SubProgWPF.Windows;
using WebSocketSharp;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using SubProgWPF.Models;

namespace SubProgWPF
{
    /// <summary>
    /// App Launch Logic
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isAppRunningFirstTime = AppServices.isAppRunningFirstTime();
            if (isAppRunningFirstTime)
            {
                firstTimeRunningSequence();
            }
            else
            {
                launchMainWindow();
            }
            base.OnStartup(e);
        }

        public void launchMainWindow()
        {
            SetLanguageDictionary();
            MainWindow window = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };
            window.Show();
        }

        private void firstTimeRunningSequence()
        {
            AppServices.initializeLanguages();
            FirstTimeSignUpWindow firstTimeSignUp = new FirstTimeSignUpWindow(this);
            firstTimeSignUp.Show();
            

        }
        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "en-US":
                    dict.Source = new Uri("..\\ResourceDictionary\\LangEnglish.xaml", UriKind.Relative);
                    break;
            }
            
            this.Resources.MergedDictionaries.Add(dict);
        }


    }
}
