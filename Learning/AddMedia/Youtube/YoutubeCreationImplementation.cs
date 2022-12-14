using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SubProgWPF.Learning.AddMedia
{
    public class YoutubeCreationImplementation : BaseYoutubeCreation
    {
        public override bool createYoutube(IYoutube youtube)
        {
            int success = YoutubeCreator.CreateYoutube(
                new FYoutube()
                {
                    Name = youtube.Name,
                    Link = youtube.Link
                }
            );
            return success == 1 ? true : false;
        }
    }
}
