using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Learning.Continue
{
    public class ContinueMedia
    {
        public static List<TempWord> getNewWordsToBeLearned(int transcriptionId)
        {
            TranscriptionAddress tA = TranscriptionServices.getTranscriptionByID(transcriptionId);

            List<TempWord> allWords = TempServices.getTempWordsByTranscriptionLocation(tA.TranscriptionLocation);
            //int tempMediaTotalLearnedWords = TempServices.getTempMediaTotalLearnedWords(tM);
            //allWords.RemoveRange(0, Int32.Parse(tM.PlayHeadPosition));
            allWords.RemoveRange(0, 0);

            return allWords;
        }
    }
}
