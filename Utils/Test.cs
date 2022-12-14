using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Utils
{
    public class Test
    {
        public static void updateEbbingIndexOfWord(int wordId, bool IsSuccess)
        {
            TestServices.updateEbbinghausIndex(wordId, IsSuccess);
          
        }
    }
}
