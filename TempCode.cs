using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF
{
    class TempCode
    {

        // About Media Player
        //Vlc.DotNet.Core.VlcMediaPlayer mediaPlayer;
        //mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(new DirectoryInfo(@"C:\\Program Files (x86)\\VideoLAN\\VLC"));
        //mediaPlayer.SetMedia(new Uri("C:\\Users\\Dean\\Desktop\\The.Big.Bang.Theory.S03.Season.3.Complete.720p.HDTV.x264-[maximersk]\\The.Big.Bang.Theory.S06E02.720p.HDTV.X264-MRSK.mkv"), new string[] { ":no-audio", ":start-time=7" });
        //mediaPlayer.Play();
        //IsPlaying = true;
        //vlcPlayer.BeginInit();
        //vlcPlayer.SourceProvider.CreatePlayer(new DirectoryInfo(@"C:\\Program Files (x86)\\VideoLAN\\VLC"), new string[] { ":no-audio", ":start-time=7" });
        //vlcPlayer.SourceProvider.MediaPlayer.Play("C:\\Users\\Dean\\Desktop\\The.Big.Bang.Theory.S03.Season.3.Complete.720p.HDTV.x264-[maximersk]\\The.Big.Bang.Theory.S05E01.720p.HDTV.ReEnc-Max.mkv");



        //private static async Task MakeHttpPostRequestAsync(string text)
        //{
        //    //string myJson = "{'Username': 'myusername','Password':'pass'}";

        //    //string myJson = "{'Username': " + "'" +  CreateString(1000000) + "'" + "}";
        //    using (var client = new HttpClient())
        //    {
        //        var response = await client.PostAsync(
        //            "http://localhost:51524/GGVFYhpidNY6HEPg46cm/",
        //             new StringContent(text, Encoding.UTF8, "application/json"));
        //    }
        //}


        //private static void MakeHttpGetRequest()
        //{
        //    string html = string.Empty;
        //    string url = @"http://localhost:51524/GGVFYhpidNY6HEPg46cm/";
        //    url += CreateString(100);

        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    request.AutomaticDecompression = DecompressionMethods.GZip;

        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    using (Stream stream = response.GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        html = reader.ReadToEnd();
        //    }

        //    Console.WriteLine(html);
        //}


        //using (WordContextDB dbContext = wordDbContextFactory.CreateDbContext())
        //{
        //    dbContext.Database.Migrate();
        //}


        //string path = "C:\\Users\\Dean\\Desktop\\PDF\\Bhagavad Gita.pdf";
        //LangUtils.GetAllWordObjectsFromPDF(path, 1, 10, 10);


        //IServiceCollection services = new ServiceCollection();
        //services.AddSingleton<NavigationStore>();


        //    public async void makeCurlRequest()
        //    {
        //        var client = new HttpClient();

        //        // Create the HttpContent for the form to be posted.
        //        var requestContent = new FormUrlEncodedContent(new[] {
        //new KeyValuePair<string, string>("text", "This is a block of text"),
        //    });


        //        // Get the response.
        //        HttpResponseMessage response = await client.PostAsync(
        //            "https://youtu.be/kFQUDCgMjRc",
        //            requestContent);

        //        // Get the response content.
        //        HttpContent responseContent = response.Content;

        //        // Get the stream of the content.
        //        string asd;
        //        using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
        //        {
        //            // Write the output.
        //            asd = await reader.ReadToEndAsync();
        //        }

        //        string path = @"C:\Users\Dean\source\repos\SubProgWPF\Content\Collections";

        //        File.WriteAllText(Path.Combine(path, "curl.txt"), asd);


        //    }



        //static string CreateString(int stringLength)
        //{
        //    Random rd = new Random();
        //    const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
        //    char[] chars = new char[stringLength];

        //    for (int i = 0; i < stringLength; i++)
        //    {
        //        chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        //    }

        //    return new string(chars);
        //}



        //Dynamic Animation
        //TranslateTransform myTranslateTransform = new TranslateTransform();
        //Duration duration = new Duration(new TimeSpan(0, 0, 0, 1, 0));
        //DoubleAnimation anim = new DoubleAnimation(300, duration);
        //anim.RepeatBehavior = RepeatBehavior.Forever;
        //anim.AutoReverse = true;
        //startButton.BeginAnimation(Border.WidthProperty, anim);


        //FOR MOUSE MOVE
        //protected override void OnSourceInitialized(EventArgs e)
        //{
        //    HwndSource hwndSource = (HwndSource)HwndSource.FromVisual(this);
        //    hwndSource.AddHook(WndProcHook);
        //    base.OnSourceInitialized(e);
        //}

        //private static IntPtr WndProcHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handeled)
        //{
        //    if (msg == 0x0084) // WM_NCHITTEST
        //    {
        //        handeled = true;
        //        return (IntPtr)2; // HTCAPTION
        //    }
        //    return IntPtr.Zero;
        //}


        //private void createFrames()
        //{
        //    //FFMpegCore.FFMpegOptions.Configure(new FFMpegCore.FFMpegOptions { RootDirectory = @"", TempDirectory = @"" });

        //    var mediaInfo = FFProbe.Analyse("C:\\Users\\Dean\\Desktop\\Musics\\programming music.mp4");
        //    //FFMpegCore.FFMpegArguments.FromFileInput("C:\\Users\\Dean\\Desktop\\Musics\\interstellar.mp4")
        //    FFMpeg.Snapshot(mediaInfo, new System.Drawing.Size(355, 200), TimeSpan.FromMinutes(1));
        //}

        //internal void openMedia(TestWordContextWithMedia testModelWithMediaAvailable)
        //{
        //    //var mediaPlayer = new Vlc.DotNet.Core.VlcMediaPlayer(new DirectoryInfo(@"C:\\Program Files (x86)\\VideoLAN\\VLC"));
        //    // time in seconds
        //    //string mediaLocation = "C:\\Users\\Dean\\Desktop\\The.Big.Bang.Theory.S03.Season.3.Complete.720p.HDTV.x264-[maximersk]\\The.Big.Bang.Theory.S05E01.720p.HDTV.ReEnc-Max.mkv";
        //    //string timeCopy = ":start-time=7";
        //    int time = convertTime(testModelWithMediaAvailable.Address.SubLocation);
        //    int seekBeforeTime = 15;
        //    time = time > seekBeforeTime ? time - seekBeforeTime : time;
        //    string timeStr = ":start-time=" + time.ToString();
        //    mediaPlayTimeOption = timeStr;

        //    mediaSubtitleOption = ":sub-file=" + testModelWithMediaAvailable.Address.TranscriptionAddress.TranscriptionLocation;
        //    //_mediaPlayer.SetMedia(new Uri(testModelWithMediaAvailable.MediaLocation), new string[] { ":no-audio", timeStr, ":sout-all", ":qt-pause-minimized" });

        //    //_mediaPlayer.SetMedia(new Uri(testModelWithMediaAvailable.MediaLocation), new string[] { timeStr, ":sout-all"});
        //    //_mediaPlayer.Play();
        //    _backgroundWorker.RunWorkerAsync(new Uri(testModelWithMediaAvailable.MediaLocation));

        //}
    }

}
