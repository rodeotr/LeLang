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
    public abstract class BaseEpisodeCreation : BaseMediaCreation, IEpisodeCreation
    {


        // returns true if operation is successful, false when not.
        public abstract bool createEpisode(IEpisode episode);

       
    }
}
