using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SubProgWPF.Utils
{
    public class TranscriptionUtils
    {
        public static string getContextFromTimestamp(string wholeText, string timeStamp, string timePattern)
        {
            if (timeStamp.Length == 0)
            {
                return "";
            }

            int tLocation = wholeText.IndexOf(timeStamp) - 1;
            tLocation = tLocation < 0 ? 0 : tLocation;
            string precedingText = getPrecedingText(wholeText, timeStamp);
            //string precedingText = wholeText.Substring(0, tLocation);
            int indexOfColon = getLastIndexOfSemiColon(precedingText, timePattern);
            //int indexOfColon = precedingText.LastIndexOf(":");

            //wholeText = precedingText.Substring(indexOfColon + 14, precedingText.Length - indexOfColon - 19);
            wholeText = precedingText.Substring(indexOfColon + 13, precedingText.Length - indexOfColon - 16);
            int minusIndex;
            for (minusIndex = 1; minusIndex < wholeText.Length - 1; minusIndex++)
            {
                Boolean result = char.IsDigit(wholeText[wholeText.Length - minusIndex]);
                if (!result)
                {
                    break;
                }
            }

            try
            {
                wholeText = wholeText.Substring(0, wholeText.Length - minusIndex).Trim();

            }
            catch (Exception)
            {


            }
            if (wholeText.Length == 0)
            {
                Console.WriteLine("");
            }
            return wholeText;
        }

        public static string getPrecedingText(string wholeText, string timeStamp)
        {
            int matchCount = Regex.Matches(wholeText, timeStamp).Count;
            if (matchCount == 2)
            {
                int tLocation = wholeText.LastIndexOf(timeStamp) - 1;
                return wholeText.Substring(0, tLocation);
            }
            else if (matchCount == 1)
            {
                int tLocation = wholeText.IndexOf(timeStamp) - 1;
                return wholeText.Substring(0, tLocation);
            }
            return "";

        }

        

        public static string getRemainingText(string wholeText,string playHeadPos)
        {
            string remainingText = null;
            if (playHeadPos != null && !playHeadPos.Equals("00:00:00"))
            {
                int index = wholeText.IndexOf(playHeadPos);
                remainingText = wholeText.Substring(index);
            }

            return remainingText;
        }

        private static int getLastIndexOfSemiColon(string precedingText, string timePattern)
        {
            Regex rg = new Regex(timePattern);
            MatchCollection matchedAuthors = rg.Matches(precedingText);
            //matchedAuthors[matchedAuthors.Count - 1].Index;
            //int lastIndexOfSemiColon = precedingText.LastIndexOf(":");

            return matchedAuthors[matchedAuthors.Count - 1].Index; ;
        }

    }
}
