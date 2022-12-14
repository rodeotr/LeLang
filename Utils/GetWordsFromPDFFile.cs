using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.AddMedia;
using SubProgWPF.Learning.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubProgWPF.Utils
{
    public class GetWordsFromPDFFile
    {
        public string wordPattern = @"\b[^\d\W][\w'-]+|[I]\b";
        public string sentencePattern = @"\s+[^.!?]*[.!?]";

        IBook _book;
        List<string> globalWordList = new List<string>();
        List<string> sentencesList = new List<string>();

        public GetWordsFromPDFFile(IBook book)
        {
            _book = book;
        }
        public List<TempWord> GetAllWordObjectsFromPDF(int startingPage, BackgroundWorker worker)
        {

            string text = "";
            using (var stream = File.OpenRead(_book.TranscriptionLocation))
            using (UglyToad.PdfPig.PdfDocument document = UglyToad.PdfPig.PdfDocument.Open(stream))
            {
                string ss = "";
                for (int i = startingPage; i < startingPage + _book.PPS; i++)
                {
                    var page = document.GetPage(i);
                    text = text + " " + string.Join(" ", page.GetWords());
                }
                //return string.Join(" ", page.GetWords());
            }

            Regex rSentence = new Regex(sentencePattern);
            MatchCollection matchedSentences = rSentence.Matches(text);

            foreach(Match match in matchedSentences)
            {
                sentencesList.Add(match.Value);
            }


            List<TempWord> words = new List<TempWord>();
            ReportClass report = new ReportClass();
            Regex rg = new Regex(wordPattern);
            MatchCollection matchedText = rg.Matches(text);

            Console.WriteLine(matchedText.Count);
            for (int count = 0; count < matchedText.Count; count++)

                if (!globalWordList.Contains(matchedText[count].Value, StringComparer.CurrentCultureIgnoreCase))
                {
                    string word_str = matchedText[count].Value;
                    if (Regex.Matches(text, word_str).Count > Int32.Parse(_book.MaxWordFreq))
                    {
                        continue;
                    }

                    report.ReportProgress = (count * 100 / matchedText.Count).ToString();
                    report.Text = word_str;

                    worker.ReportProgress(0, report);
                    globalWordList.Add(word_str);
                   

                    List<WordContext> wContexts = new List<WordContext>();

                    List<int> wContext_Ids = new List<int>();
                    foreach (string w in sentencesList)
                    {
                        if (w.Contains(word_str))
                        {
                            int contextId = createWordContext(w, "");
                            wContext_Ids.Add(contextId);
                        }
                    }

                    TempWord word = new TempWord()
                    {
                        Name = word_str,
                        FirstTimeTranscriptionId = _book.TranscriptionId,
                        InitDate = DateTime.Now,
                        WordContext_Ids = String.Join(",", wContext_Ids.ConvertAll<string>(a => a.ToString()))
                    };

                    words.Add(word);

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
                transcriptionAddress.TranscriptionLocation = _book.TranscriptionLocation;
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
                Type = MediaTypes.TYPE.Book


            };
            int wCId = WordServices.createWordContext(context);

            return wCId;
        }
        private TranscriptionAddress getTranscriptionAddress()
        {
            TranscriptionAddress transcriptionAddress = TranscriptionServices.GetTranscription(_book.TranscriptionLocation);
            return transcriptionAddress;
        }


    }
}
    

