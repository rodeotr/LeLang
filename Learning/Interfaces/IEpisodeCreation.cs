using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Interfaces
{
    public interface IBookCreation : IMediaCreation
    {
        public bool createBook(IBook book);
    }

}
