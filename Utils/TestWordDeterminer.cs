using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Utils
{
    public class TestWordDeterminer
    {
        public static int[] EbbingHausIntervalsInMinutes =
        {
            0,
            20,
            60,
            60*24,
            60*24*3,
            60*24*7,
            60*24*14,
            60*24*30,
            60*24*120,
            60*24*180
        };

        public static bool determineIfPracticeNeeded(Word w)
        {
            if(w.Repetition.Count == 0) { return true; }
            Repetition lastRep = w.Repetition[w.Repetition.Count - 1];
            
            if((DateTime.Now - lastRep.Time).TotalMinutes > EbbingHausIntervalsInMinutes[w.EbbinghausForgettingIndex])
            {
                return true;
            }
            return false;

        }
        

        //private bool isSecondStagePassed(Word w)
        //{
        //    Repetition repetition1 = w.Repetition[w.Repetition.Count - 1];
        //    if(w.Repetition.Count > 1)
        //    {
        //        Repetition repetition2 = w.Repetition[w.Repetition.Count - 2];
        //    }
        //    else if ((DateTime.Now - repetition1.Time).TotalMinutes > EbbingHausIntervalsInMinutes[1])
        //    {


        //    }

        //}

        //private bool isFirstStagePassed(Word w)
        //{
        //    Repetition repetition1 = w.Repetition[w.Repetition.Count - 1];
        //    if (!repetition1.Success) { return false; } else { return true; }
        //}
    }
}
