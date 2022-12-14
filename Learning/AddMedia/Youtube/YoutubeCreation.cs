using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Add
{
    public class YoutubeCreation
    {
        private string _mediaName;
        private string _link;

        public YoutubeCreation(string mediaName, string link)
        {
            _mediaName = mediaName;
            _link = link;
        }

        public int createYoutube()
        {
            return YoutubeCreator.CreateYoutube(
                new FYoutube()
                {
                    Name = _mediaName,
                    Link = _link
                }
            );
        }
        
    }
}
