using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IYoutubeCreation : IMediaCreation
    {
        public bool createYoutube(IYoutube youtube);
    }

}
