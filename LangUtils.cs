using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Linq;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;

namespace SubProgWPF
{
    static class LangUtils
    {
        static List<string> globalWordList = new List<string>();
        static int counter = 0;
        public static int max_word_freq = 20;
        public static string pattern_every_word_subtitle = @"\b[^\d\W][\w'-]+|[I]\b";
        public static string pattern_every_word = @"\b[\w]+\b";
        public static string patter_time = @"\b[\d]{2}:[\d]{2}:[\d]{2},[\d]{3}";

        public static List<string> GetUniqueWords()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\Dean\Desktop\Silicon.Valley.S02.1080p.BluRay.x265.HEVC.6CH-MRN\S01 SUBS\Silicon.Valley.S01E02.HDTV.x264 - 2HD.srt");

            Regex rg = new Regex(pattern_every_word_subtitle);
            MatchCollection matchedAuthors = rg.Matches(text);

            for (int count = 0; count < matchedAuthors.Count; count++)

                if (!globalWordList.Contains(matchedAuthors[count].Value))
                {
                    globalWordList.Add(matchedAuthors[count].Value);
                }

            return globalWordList;
        }

        public static void GetAllWordsFromDir(string path)
        {
            List<string> file_names = GetFileNamesFromDir(path);
            foreach (string str in file_names)
            {
                string text = System.IO.File.ReadAllText(str);
                
                //GetUniqueWords(text);
            }
        }
        public static List<WordContext> getAllMatchesOfWordContextsFromDir(string[] word_array)
        {
            List<WordContext> wordContexts = new List<WordContext>();
            //List<string> file_names = GetFileNamesFromDir("C:\\Users\\Dean\\Desktop\\Subtitles\\Silicon Valley\\Season 1");
            string[] file_names = Directory.GetFiles("C:\\Users\\Dean\\Desktop\\Subtitles", "*.srt", SearchOption.AllDirectories);

            foreach (string str in file_names)
            {
                string text = System.IO.File.ReadAllText(str);
                for (int i = 0; i < word_array.Length; i++)
                {
                    string s = word_array[i];
                    foreach (Match match in Regex.Matches(text, "\\b"+s+"\\b"))
                    {
                        string time = getTimeValueOfTheMatch(match.Index, text);
                        string context_str = getContextFromLoc(text, time);

                        TranscriptionAddress transcription = WordServices.GetTranscription(str);
                        if(transcription == null)
                        {
                            transcription = new TranscriptionAddress();
                            transcription.TranscriptionLocation = str;
                        }
                        
                        
                        Address address = new Address() { TranscriptionAddress_Id = transcription.Id, SubLocation = time};
                        WordContext context = new WordContext() {
                            Address = address ,
                            Type = LangDataAccessLibrary.MediaTypes.TYPE.TVSeries,
                            Content = context_str
                        };

                        wordContexts.Add(context);
                    }
                }
            }

            return wordContexts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="max_word_freq"></param>
        /// <returns></returns>
        public static List<TempWord> GetAllWordObjects(string path, int max_word_freq, string playHeadPos)
        {
            string text = System.IO.File.ReadAllText(path);
            text = getRemainingText(playHeadPos, text);

            List<TempWord> words = new List<TempWord>();
            Regex rg = new Regex(pattern_every_word_subtitle);
            MatchCollection matchedText = rg.Matches(text);

            for (int count = 0; count < matchedText.Count; count++)

                if (!globalWordList.Contains(matchedText[count].Value, StringComparer.CurrentCultureIgnoreCase))
                {
                    string word_str = matchedText[count].Value;
                    string wordMatchPattern = "\\b(" + word_str + ")\\b";
                    Regex wordRegex = new Regex(wordMatchPattern);

                    
                    if (Regex.Matches(text, "\\b"+word_str+"\\b").Count > max_word_freq)
                    {
                        continue;
                    }
                    

                    counter += 1;

                    if(count % 50 == 0)
                    {
                        Console.WriteLine("We're processing the " + count + "th words out of " + matchedText.Count + " words." + "  " + (float)count/matchedText.Count);
                    }
                    
                    List<int> wContext_Ids = new List<int>();
                    bool doesWordExists = false;
                    foreach (Match match in Regex.Matches(text, "\\b" + word_str + "\\b"))
                    {
                        if (word_str.Equals("may"))
                        {
                            Console.WriteLine("");
                        }
                        string time = getTimeValueOfTheMatch(match.Index, text);
                        if (time.Equals(playHeadPos))
                        {
                            doesWordExists = true;
                            break;
                        }
                        
                        string context_str = getContextFromLoc(text, time);
                        
                        Address address = new Address();
                        bool transcriptionExists = true;
                        TranscriptionAddress transcriptionAddress = WordServices.GetTranscription(path);
                        if(transcriptionAddress == null)
                        {
                            transcriptionExists = false;
                            transcriptionAddress = new TranscriptionAddress();
                            transcriptionAddress.TranscriptionLocation = path;
                            MediaServices.createTranscriptionAddress(transcriptionAddress);
                        }
                        
                        //address.MediaAddress = path;
                        //address.TranscriptionAddress = null;
                        address.TranscriptionAddress_Id = transcriptionExists ? transcriptionAddress.Id : 0;
                        address.SubLocation = time;
                        WordContext context = new WordContext()
                        {
                            Address = address,
                            Content = context_str,
                            Type = LangDataAccessLibrary.MediaTypes.TYPE.TVSeries
                            

                        };
                        int wCId = WordServices.createWordContext(context);
                        //MediaServices.createAddress(address);
                        wContext_Ids.Add(wCId);
                    }
                    if (doesWordExists)
                    {
                        continue;
                    }
                    if(wContext_Ids.Count == 0)
                    {
                        Console.WriteLine("ss");
                    }
                    globalWordList.Add(word_str);
                    TempWord word = new TempWord()
                    {
                        Name = word_str,
                        InitDate = DateTime.Now,
                        WordContext_Ids = String.Join(",", wContext_Ids.ConvertAll<string>(a => a.ToString())) 
                    };

                    words.Add(word);

                }
            return words;
        }


        public static List<Word> GetAllWordObjectsFromPDF(string path, int max_word_freq, int startingPage, int pageCount)
        {

            string text = "";
            using (var stream = File.OpenRead(path))
            using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(stream))
            {
                string ss = "";
                for (int i = startingPage; i < startingPage + pageCount; i++)
                {
                    var page = document.GetPage(i);
                    text = text + " " + string.Join(" ", page.GetWords());
                }
                //return string.Join(" ", page.GetWords());
            }

            

            List<Word> words = new List<Word>();
            Regex rg = new Regex(pattern_every_word);
            MatchCollection matchedText = rg.Matches(text);

            Console.WriteLine(matchedText.Count);
            for (int count = 0; count < matchedText.Count; count++)

                if (!globalWordList.Contains(matchedText[count].Value, StringComparer.CurrentCultureIgnoreCase))
                {
                    string word_str = matchedText[count].Value;
                    if (Regex.Matches(text, word_str).Count > max_word_freq)
                    {
                        continue;
                    }

                    counter += 1;

                    globalWordList.Add(word_str);
                    Console.WriteLine(counter);
                    Console.WriteLine(word_str);
                    
                    List<WordContext> wContexts = new List<WordContext>();

                    //foreach (Match match in Regex.Matches(text, word_str))
                    //{
                    //    string time = getTimeValueOfTheMatch(match.Index, text);

                    //    Address address = new Address();
                    //    address.MediaAddress = path;
                    //    address.SubAddress = time;
                    //    WordContext context = new WordContext()
                    //    {
                    //        Address = address,
                    //        Content = getContextFromLoc(text, time),
                    //        Type = LangDataAccessLibrary.MediaTypes.TYPE.Book


                    //    };
                    //    wContexts.Add(context);
                    //}

                    Word word = new Word()
                    {
                        Name = word_str,
                        //WordContexts = wContexts
                    };

                    words.Add(word);

                }
            return words;
        }


        private static string getRemainingText(string playHeadPos, string text)
        {
            if (playHeadPos != null && !playHeadPos.Equals("00:00:00"))
            {
                int index = text.IndexOf(playHeadPos);
                text = text.Substring(index);
            }

            return text;
        }

        /// <summary>
        /// returns the timestamp that immediately comes  after the word
        /// </summary>
        /// <param name="v"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string getTimeValueOfTheMatch(int v, string text)
        {
            text = text.Substring(v, text.Length - v > 200 ? 200 : text.Length - v - 1);
            Regex rg = new Regex(patter_time);
            MatchCollection matchedAuthors = rg.Matches(text);
            try
            {
                return matchedAuthors[0].Value;
            }
            catch
            {
                Console.WriteLine("timestamp couldn't be found");
                return "";
            }
        }
        public static string getContextFromLoc(string wholeText, string timeStamp)
        {
            if (timeStamp.Length == 0)
            {
                return "";
            }
            if (timeStamp.Equals("00:00:46,710"))
            {
                Console.WriteLine("");
            }
            int tLocation = wholeText.IndexOf(timeStamp) - 1;
            tLocation = tLocation < 0 ? 0 : tLocation;
            string precedingText = getPrecedingText(wholeText, timeStamp);
            //string precedingText = wholeText.Substring(0, tLocation);
            int indexOfColon = getLastIndexOfSemiColon(precedingText);
            //int indexOfColon = precedingText.LastIndexOf(":");

            wholeText = precedingText.Substring(indexOfColon + 14, precedingText.Length - indexOfColon - 19);
            int minusIndex;
            for(minusIndex = 1; minusIndex < wholeText.Length - 1; minusIndex++)
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
            if(wholeText.Length == 0)
            {
                Console.WriteLine("");
            }
            return wholeText;
        }

        private static int getLastIndexOfSemiColon(string precedingText)
        {
            Regex rg = new Regex(patter_time);
            MatchCollection matchedAuthors = rg.Matches(precedingText);
            //matchedAuthors[matchedAuthors.Count - 1].Index;
            //int lastIndexOfSemiColon = precedingText.LastIndexOf(":");

            return matchedAuthors[matchedAuthors.Count - 1].Index; ;
        }

        private static string getPrecedingText(string wholeText, string timeStamp)
        {
            int matchCount = Regex.Matches(wholeText, timeStamp).Count;
            if (matchCount == 2)
            {
                int tLocation = wholeText.LastIndexOf(timeStamp) - 1;
                return wholeText.Substring(0, tLocation);
            }
            else if(matchCount == 1)
            {
                int tLocation = wholeText.IndexOf(timeStamp) - 1;
                return wholeText.Substring(0, tLocation);
            }
            return "";
            
        }

        private static List<string> GetFileNamesFromDir(string path)
        {
            List<string> file_names = new List<string>();
            var files = from file in
            Directory.EnumerateFiles(path)
                        select file;
            foreach (var file in files)
            {
                file_names.Add(file);
                //Console.WriteLine("{0}", file);
            }
            return file_names;
        }

        public static void orderWordObjectList()
        {

        }



    }
}
