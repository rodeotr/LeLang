using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.AddMedia;
using SubProgWPF.Learning.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubProgWPF.Utils
{
    public class GetWordsFromTranscriptionFile
    {
        public string wordPattern = @"\b[^\d\W][\w'-]+|[I]\b";
        public string timePattern = @"\b[\d]{2}:[\d]{2}:[\d]{2},[\d]{3}";

        IMedia _media; 

        List<string> globalWordList = new List<string>();

        public GetWordsFromTranscriptionFile(IMedia media)
        {
            _media = media;
        }


        /// <summary>
        /// Reads Whole Text
        /// Gets Remaining Text
        /// Run regex for every individual words
        /// Check if the word is already in the list, if it is then continue
        /// Run Regex on individual word to see the occurrence count
        /// 
        /// </summary>
        /// <param name="playHeadPosition"></param>
        /// <returns></returns>
        //public List<TempWord> GetWords(string playHeadPosition, BackgroundWorker worker)
        //{
        //    List<TempWord> words = new List<TempWord>();

        //    _rawText = System.IO.File.ReadAllText(_path);
        //    string remainingText = TranscriptionUtils.getRemainingText(_rawText, playHeadPosition);

        //    Regex rg = new Regex(wordPattern);
        //    MatchCollection matchedText = rg.Matches(remainingText);

        //    for (int count = 0; count < matchedText.Count; count++)
        //    {
                
        //        string wordStr = matchedText[count].Value;
        //        if (!globalWordList.Contains(wordStr))
        //        {
        //            string wordMatchPattern = "\\b(" + wordStr + ")\\b";
        //            Regex wordRegex = new Regex(wordMatchPattern);

        //            MatchCollection mC = Regex.Matches(remainingText, "\\b" + wordStr + "\\b");
        //            int wordOccurenceInText = mC.Count;
        //            if (wordOccurenceInText > _maxWordFreq)
        //            {
        //                continue;
        //            }

        //            //if (count % 1 == 0)
        //            //{
                    
        //            worker.ReportProgress(0,
        //                new ReportClass()
        //                {
        //                    ReportProgress = (count * 100 / matchedText.Count).ToString(),
        //                    Text = wordStr
        //                });
        //            //}

        //            List<int> wContext_Ids = new List<int>();
        //            foreach (Match match in mC)
        //            {
        //                string time = TranscriptionUtils.getTimeValueOfTheMatch(match.Index, remainingText);
                        

        //                string contextStr = TranscriptionUtils.getContextFromTimestamp(remainingText, time);
        //                int contextId = createWordContext(contextStr,time);
                        
        //                wContext_Ids.Add(contextId);
        //            }
                    

        //            globalWordList.Add(wordStr);

        //            TempWord word = new TempWord()
        //            {
        //                Name = wordStr,
        //                InitDate = DateTime.Now,
        //                WordContext_Ids = String.Join(",", wContext_Ids.ConvertAll<string>(a => a.ToString()))
        //            };

        //            words.Add(word);

        //        }
        //    }
        //    return words;
        //}


        public List<TempWord> GetWords(BackgroundWorker worker)
        {
            List<TempWord> words = new List<TempWord>();
            ReportClass report = new ReportClass();

            string _rawText = System.IO.File.ReadAllText(_media.TranscriptionLocation);
            //string remainingText = TranscriptionUtils.getRemainingText(_rawText, playHeadPosition);
            string remainingText = _rawText;

            Regex rg = new Regex(wordPattern);
            MatchCollection matchedText = rg.Matches(remainingText);

            for (int count = 0; count < matchedText.Count; count++)
            {

                string wordStr = matchedText[count].Value;
                if (!globalWordList.Contains(wordStr))
                {
                    //int wordOccurenceInText = remainingText.Split(" "+wordStr+" ").Length - 1;
                    string wordMatchPattern = "\\b(" + wordStr + ")\\b";
                    Regex wordRegex = new Regex(wordMatchPattern,RegexOptions.IgnoreCase);

                    //MatchCollection mC = Regex.Matches(remainingText, "\\b" + wordStr + "\\b");
                    MatchCollection mC = wordRegex.Matches(remainingText);
                    int wordOccurenceInText = mC.Count;

                    //if(wordOccurenceInText == 2)
                    //{
                    //    Console.WriteLine("");
                    //}
                    if (wordOccurenceInText > Int32.Parse(_media.MaxWordFreq) || wordOccurenceInText == 0)
                    {
                        continue;
                    }
                    

                    report.ReportProgress = (count * 100 / matchedText.Count).ToString();
                    report.Text = wordStr;

                    worker.ReportProgress(0, report);

                  

                    List<int> wContext_Ids = new List<int>();
                    //List<int> wordIndexes = AllIndexesOf(remainingText, wordStr);
                    //foreach (int i in wordIndexes)
                    //{
                    //    string time = TranscriptionUtils.getTimeValueOfTheMatch(i, remainingText);


                    //    string contextStr = TranscriptionUtils.getContextFromTimestamp(remainingText, time);
                    //    int contextId = createWordContext(contextStr, time);

                    //    wContext_Ids.Add(contextId);
                    //}
                    foreach (Match match in mC)
                    {
                        string time = TranscriptionUtils.getTimeValueOfTheMatch(match.Index, remainingText);


                        string contextStr = TranscriptionUtils.getContextFromTimestamp(remainingText, time);
                        int contextId = createWordContext(contextStr, time);

                        wContext_Ids.Add(contextId);
                    }


                    globalWordList.Add(wordStr);

                    TempWord word = new TempWord()
                    {
                        Name = wordStr,
                        InitDate = DateTime.Now,
                        WordContext_Ids = String.Join(",", wContext_Ids.ConvertAll<string>(a => a.ToString())),
                        FirstTimeTranscriptionId = _media.TranscriptionId
                        
                    };

                    words.Add(word);

                }
            }
            return words;
        }

        private int createWordContext(string contextStr, string time)
        {
            int transcriptionId;
            TranscriptionAddress transcriptionAddress = getTranscriptionAddress();

            if (transcriptionAddress == null)
            {
                transcriptionAddress = new TranscriptionAddress();
                transcriptionAddress.TranscriptionLocation = _media.TranscriptionLocation;
                transcriptionId = TranscriptionServices.createTranscriptionAddress(transcriptionAddress);
            }
            else { transcriptionId = transcriptionAddress.Id; }

            Address address = new Address();
            address.TranscriptionAddress_Id = transcriptionId;
            address.SubLocation = time;

            WordContext context = new WordContext()
            {
                Address = address,
                Content = contextStr,
                Type = _media.Type


            };
            int wCId = WordServices.createWordContext(context);

            return wCId;
        }
        public static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        private TranscriptionAddress getTranscriptionAddress()
        {
            TranscriptionAddress transcriptionAddress = TranscriptionServices.GetTranscription(_media.TranscriptionLocation);
            return transcriptionAddress;
        }
    }
    }
    

