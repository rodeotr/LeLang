using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.AddMedia;
using SubProgWPF.Learning.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Linq;

namespace SubProgWPF.Utils
{
    public class GetWordsFromTranscriptionFile
    {
        IMedia _media;
        BackgroundWorker _worker;
        List<TempWord> words = new List<TempWord>();
        ReportClass report = new ReportClass();
        ResourceDictionary dict;
        RegexTypes _regexTypes;
        TranscriptionAddress transcriptionAddress;

        static string _rawText;
        List<string> globalWordList = new List<string>();
        List<TempWordContext> wordContextHolder;
        static List<string> timeStampList;
        static List<Word> _knownWords;
        static List<int> timeStampIndexList;
        static MatchCollection matchedTimestamps;
        static private string word;

        public GetWordsFromTranscriptionFile(IMedia media, BackgroundWorker worker, RegexTypes regexTypes)
        {

            _media = media;
            _worker = worker;
            _regexTypes = regexTypes;
            _rawText = ReadAllText();
        }

        public void setText(string text)
        {
            _rawText = text;
        }

        public List<TempWord> GetWords(List<Word> knownWords)
        {
            _knownWords = knownWords;

            int transcriptionId;
            transcriptionAddress = getTranscriptionAddress();

            if (transcriptionAddress == null)
            {
                transcriptionAddress = new TranscriptionAddress();
                transcriptionAddress.TranscriptionLocation = _media.TranscriptionLocation;
                transcriptionId = TranscriptionServices.createTranscriptionAddress(transcriptionAddress);
            }
            else { transcriptionId = transcriptionAddress.Id; }


            MatchCollection matchedText = getMatchCollectionOfWords();
            matchedTimestamps = getMatchCollectionAllTimestamps();

            //Console.WriteLine("getMatchCollectionOfWords took " + watchGlobal.ElapsedMilliseconds);
            
            for (int count = 0; count < matchedText.Count; count++)
            {
                wordContextHolder = new List<TempWordContext>();
                string progressPercentage = (count * 100 / matchedText.Count).ToString();
                word = matchedText[count].Value;
                bool isWordKnown = checkIfWordIsKnown(word);
                if (isWordKnown && !WordAlreadyAdded(word))
                {
                    addTheContextToDB(word);
                    globalWordList.Add(word);
                    continue;
                }
                if (!WordAlreadyAdded(word))
                {
                    MatchCollection mC = GetSpecificWordMatches(word);
                    if (IsWordCountTooMany(mC.Count))
                    {
                        continue;
                    }
                    
                    
                    createWordContexts(mC);

                    TempWord wordTemp = CreateTempWordObj(word);
                    globalWordList.Add(word);
                    words.Add(wordTemp);
                    //watchReportProgress.Start();
                    ReportProgress(progressPercentage, word);
                    //watchReportProgress.Stop();
                }
            }
            //watchGlobal.Stop();
            //Console.WriteLine("total time passed global" + watchGlobal.ElapsedMilliseconds);
            //Console.WriteLine("total time passed wordAlreadyAdded" + (watchWordAlreadyAdded.ElapsedMilliseconds / watchGlobal.ElapsedMilliseconds)*100);
            //Console.WriteLine("total time passed createContexts" + (watchCreateContexts.ElapsedMilliseconds / watchGlobal.ElapsedMilliseconds)*100);
            return words;
        }

        private void addTheContextToDB(string _word)
        {
            MatchCollection mC = GetSpecificWordMatches(_word);
            createWordContexts(mC);
            Word w = WordServices.getWordByName(_word);
            WordServices.addContextListToWord(w.Id, wordContextHolder);

        }

        private bool checkIfWordIsKnown(string word)
        {
            foreach(Word w in _knownWords)
            {
                if (w.Name.ToLower().Equals(word))
                {
                    return true;
                }
                if(w.WordInflections != null)
                {
                    foreach (WordData wordInflection in w.WordInflections)
                    {
                        if (wordInflection.Name.ToLower().Equals(word))
                        {
                            return true;
                        }
                    }
                }
                
            }
            return false;
        }

        private MatchCollection getMatchCollectionAllTimestamps()
        {
            Regex rg = new Regex(_regexTypes.TimePattern);
            MatchCollection matchedText = rg.Matches(_rawText);
            timeStampList = matchedText.Select(a => a.Value).ToList();
            timeStampIndexList = matchedText.Select(a => a.Index).ToList();
            return matchedText;
        }

        private TempWord CreateTempWordObj(string wordStr)
        {
            return new TempWord()
            {
                Name = wordStr,
                InitDate = DateTime.Now,
                Contexts = wordContextHolder,
                //WordContext_Ids = String.Join(",", wordContextHolder.ConvertAll<string>(a => a.ToString())),
                FirstTimeTranscription = transcriptionAddress
            };
        }


        private void createWordContexts(MatchCollection mC)
        {
            foreach (Match match in mC)
            {
                Context c = getContextFromIndex(match.Index);
                //string contextStr = TranscriptionUtils.getContextFromTimestamp(_rawText, c.TimeStamp, _timePattern);
                createWordContext(c);
            }

        }
        public Context getContextFromIndex(int wordStartIndex)
        {
            int index = ClosestTo(wordStartIndex);
            int closestIndex = timeStampIndexList.IndexOf(index) > 0 ? timeStampIndexList.IndexOf(index) - 1 : 0;
            int charStartIndex = timeStampIndexList[closestIndex] + 29;
            int deduction = _media.Type == MediaTypes.TYPE.Youtube ? charStartIndex + 6 : charStartIndex + 8;
            string context = _rawText.Substring(charStartIndex, timeStampIndexList[closestIndex + 1] - deduction).Trim();
            
            if (!context.Contains(word))
            {
                
                closestIndex += 1;
                charStartIndex = timeStampIndexList[closestIndex] + 29;
                int length;
                if (closestIndex + 1 == timeStampIndexList.Count)
                {
                    length = timeStampIndexList[closestIndex];
                }
                else
                {
                    length = timeStampIndexList[closestIndex + 1];
                }
                length = _media.Type == MediaTypes.TYPE.Youtube ? length - charStartIndex - 7 :
                    length - charStartIndex - 8;
                //length -= charStartIndex + 8;
                length = length > 0 ? length : 0;
                context = _rawText.Substring(charStartIndex, length).Trim();
            }
            return new Context() { TimeStamp = timeStampList[closestIndex].Substring(0, 12), Index = closestIndex, Text = context};
        }

        private void ReportProgress(string progressPercentage, string wordStr)
        {
            try
            {
                report.ReportProgress = progressPercentage;
                report.Text = wordStr;

                _worker.ReportProgress(0, report);
            }
            catch (Exception)
            {
            }
            
        }

        private bool IsWordCountTooMany(int count)
        {
            if (count > Int32.Parse(_media.MaxWordFreq) || count == 0)
            {
                return true;
            }
            return false;
        }

        private MatchCollection GetSpecificWordMatches(string wordStr)
        {
            string wordMatchPattern = "\\b(" + wordStr + ")\\b";
            Regex wordRegex = new Regex(wordMatchPattern, RegexOptions.IgnoreCase);

            //MatchCollection mC = Regex.Matches(remainingText, "\\b" + wordStr + "\\b");
            MatchCollection mC = wordRegex.Matches(_rawText);
            return mC;
        }

        private bool WordAlreadyAdded(string wordStr)
        {
            //watchWordAlreadyAdded.Start();
            bool contains = globalWordList.Contains(wordStr);
            //watchWordAlreadyAdded.Stop();

            return contains;
        }

        private MatchCollection getMatchCollectionOfWords()
        {
            Regex rg = new Regex(_regexTypes.WordPattern);
            MatchCollection matchedText = rg.Matches(_rawText);
            return matchedText;
        }

        private string ReadAllText()
        {
            string text = null;
            try
            {
               text = System.IO.File.ReadAllText(_media.TranscriptionLocation);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            return text;

        }

        private void createWordContext(Context c)
        {
            
            Address address = new Address();
            //address.TranscriptionAddress_Id = transcriptionId;
            address.TranscriptionAddress = transcriptionAddress;
            address.SubLocation = c.TimeStamp;

            TempWordContext context = new TempWordContext()
            {
                Address = address,
                Content = c.Text,
                Type = _media.Type
            };
            //watchCreateContexts.Start();
            //int wCId = WordServices.createWordContext(context);
            //watchCreateContexts.Stop();
            wordContextHolder.Add(context);
            //return wCId;
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

        public static int ClosestTo(int target)
        {
            // NB Method will return int.MaxValue for a sequence containing no elements.
            // Apply any defensive coding here as necessary.
            var closest = int.MaxValue;
            var minDifference = int.MaxValue;
            foreach (var element in timeStampIndexList)
            {
                var difference = Math.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (int)difference;
                    closest = element;
                }
            }

            return closest;
        }
    }

    public class RegexTypes
    {
        public string WordPattern { get; set; }
        public string TimePattern { get; set; }
    }
    public class Context
    {
        public string TimeStamp { get; set; }
        public int Index { get; set; }
        public string Text { get; set; }
    }


}
    

