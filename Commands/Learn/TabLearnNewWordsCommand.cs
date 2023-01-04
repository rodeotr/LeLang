using LangDataAccessLibrary;
using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using Microsoft.EntityFrameworkCore;
using SubProgWPF.Learning.AddWord;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using SubProgWPF.ViewModels.Learn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace SubProgWPF.Commands
{
    public class TabLearnNewWordsCommand : CommandBase
    {
        private readonly TabLearnNewWordsViewModel _dataGridNewWordsViewModel;

        public TabLearnNewWordsCommand(TabLearnNewWordsViewModel dataGridNewWordsViewModel)
        {
            _dataGridNewWordsViewModel = dataGridNewWordsViewModel;
           
        }

        public override void Execute(object parameter)
        {
            if(parameter is StorageContext)
            {
                _dataGridNewWordsViewModel.launchContextWindow((StorageContext)parameter);
            }else if(parameter is Tuple<string, object>)
            {
                Tuple<string, object> tuple = (Tuple<string, object>)parameter;
                MemberNewWord member = (MemberNewWord)tuple.Item2;
                addWordToDB(member.WordObj);
                TempServices.deleteTempWordFromDB(member.WordObj);
                _dataGridNewWordsViewModel.Members.Remove(member);
                _dataGridNewWordsViewModel.OnMembersChanged();
                updateViewModel(Int32.Parse(member.Number));
                incrementScore();
            }
            else
            {
                string paramString = parameter.ToString();

                switch (paramString)
                {
                    case "next":
                    case "prev":
                        switchPage(parameter.ToString());
                        return;
                    case "finish":
                        TranscriptionServices.finishMedia(_dataGridNewWordsViewModel.MediaModel.Type.ToString(),
                            _dataGridNewWordsViewModel.MediaModel.TranscriptionLocation);
                        _dataGridNewWordsViewModel.finishSession();
                        return;

                }

                //if (IsNumeric(paramString))
                //{
                //    //_dataGridNewWordsViewModel.IsLoading = true;
                //    int index = Int32.Parse(parameter.ToString()) - 1;

                //    addWordToDB(index);
                //    TempServices.deleteTempWordFromDB(_dataGridNewWordsViewModel.MembersModel.TempWordList[index]);
                //    updateViewModel(index);
                //    incrementScore();
                //}
                //else
                //{
                //    getMeaning(parameter.ToString());
                //}
            }
            
            
        }

        private void incrementScore()
        {
            ScoreServices.IncrementScoreWordAdding();
        }

        private void updateViewModel(int index)
        {
            if (_dataGridNewWordsViewModel.LastLearnedWordIndex < index + 1)
            {
                TranscriptionServices.updateTranscriptionPlayHead(_dataGridNewWordsViewModel.TranscriptionId, index);
                MediaServices.updateMediaWordCount(_dataGridNewWordsViewModel.MediaModel.TranscriptionLocation,_dataGridNewWordsViewModel.MediaModel.Type.ToString());
                _dataGridNewWordsViewModel.LastLearnedWordIndex = index + 1;
                _dataGridNewWordsViewModel.LastLearnedText = "Last Learned Word Index : " + (index + 1).ToString();
            }
            _dataGridNewWordsViewModel.TabLearnViewModel.notifyTheMainViewModelForUpdate();
        }

        private void addWordToDB(TempWord word)
        {
            AddWord.AddWordToDB(word, _dataGridNewWordsViewModel.MediaModel.Type);
        }
        
        private void getMeaning(string name)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = "https://www.google.com/search?q=" + name + " meaning",
                UseShellExecute = true
            });
        }
        private void switchPage(string v)
        {
            if(_dataGridNewWordsViewModel.MembersModel.IsLastPage && v.Equals("next"))
            {
                return;
            }
            _dataGridNewWordsViewModel.MembersModel.updateGrid(v);
            _dataGridNewWordsViewModel.Members = _dataGridNewWordsViewModel.MembersModel.CurrentMembers;
            _dataGridNewWordsViewModel.PageNumString = "Page: " + _dataGridNewWordsViewModel.MembersModel.Current_page.ToString();
            _dataGridNewWordsViewModel.checkIfLastPage();
        }



        private List<string> trimList(List<string> altNames)
        {
            for(int i = 0; i < altNames.Count; i++)
            {
                altNames[i] = altNames[i].Trim();
            }
            return altNames;
        }
        //private bool checkIfSeasonExists(string seasonIndex)
        //{
        //    DataContextFactory contextFactory = new DataContextFactory();
        //    using (WordContextDB wordContextDB = contextFactory.CreateDbContext(new string[] { "" }))
        //    {
        //        return wordContextDB.TVSeriesSeasons.Any(s => s.SeasonIndex == Int32.Parse(SeasonIndex_) && s.Series.Name.Equals(TvSeriesName_));
        //    }
        //}
        private void createDatabaseEntry(Word word)
        {
            word.InitDate = DateTime.Now;

            DataContextFactory contextFactory = new DataContextFactory();
            using (WordContextDB wordContextDB = contextFactory.CreateDbContext(new string[] { "" }))
            {
                wordContextDB.Words.Add(word);
                wordContextDB.SaveChanges();
            }
        }

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
    }
}
