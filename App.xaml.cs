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
using LangDataAccessLibrary.Services.IServices;
using Microsoft.Extensions.Hosting;

namespace SubProgWPF
{
    /// <summary>
    /// App Launch Logic
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton(new DataContextFactory());
                services.AddSingleton(this);
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<IAppServices, AppServices>();
                services.AddSingleton((s) => new MainViewModel(s.GetRequiredService<NavigationStore>()));
                services.AddSingleton<MainWindow>();


            }).Build();
            _navigationStore = new NavigationStore();
            App.Current.Properties["AppHost"] = _host;
            
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
            bool isAppRunningFirstTime = appServices.isAppRunningFirstTime();
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
            //MainWindow window = new MainWindow() { DataContext = new MainViewModel(_navigationStore) };
            MainWindow window = _host.Services.GetRequiredService<MainWindow>();
            window.DataContext = _host.Services.GetRequiredService<MainViewModel>();
            window.Show();
        }

        private void firstTimeRunningSequence()
        {
            AppServices appServices = (AppServices)_host.Services.GetRequiredService<IAppServices>();
            //appServices.addAllLanguagesToDB();
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
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _host.Dispose();
            IHost mainHost = (IHost)App.Current.Properties["MainViewModelHost"];
            if(mainHost != null)
            {
                mainHost.Dispose();
            }
            
        }
    }
}
