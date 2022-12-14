using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IEpisodeCreation : IMediaCreation
    {
        public bool createEpisode(IEpisode episode);
    }

}
