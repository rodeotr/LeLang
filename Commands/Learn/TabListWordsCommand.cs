using LangDataAccessLibrary;
using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using Microsoft.EntityFrameworkCore;
using SubProgWPF.Learning.AddWord;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace SubProgWPF.Commands
{
    public class TabListWordsCommand : CommandBase
    {
        private readonly DataGridNewWordsViewModel _dataGridNewWordsViewModel;

        public TabListWordsCommand(DataGridNewWordsViewModel dataGridNewWordsViewModel)
        {
            _dataGridNewWordsViewModel = dataGridNewWordsViewModel;
           
        }

        public override void Execute(object parameter)
        {
            if(parameter is StorageContext)
            {
                _dataGridNewWordsViewModel.launchContextWindow((StorageContext)parameter);
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
                        TranscriptionServices.finishMedia(_dataGridNewWordsViewModel.AddMediaModel.Type.ToString(),
                            _dataGridNewWordsViewModel.AddMediaModel.TranscriptionLocation);
                        _dataGridNewWordsViewModel.finishSession();
                        return;

                }

                if (IsNumeric(paramString))
                {
                    _dataGridNewWordsViewModel.IsLoading = true;
                    int index = Int32.Parse(parameter.ToString()) - 1;

                    addWordToDB(index);
                    updateViewModel(index);
                    incrementScore();
                }
                else
                {
                    getMeaning(parameter.ToString());
                }
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
                TranscriptionServices.updateTranscriptionPlayHead(_dataGridNewWordsViewModel.TabLearnViewModel.SelectedTranscriptionId, index);
                MediaServices.updateMediaWordCount(_dataGridNewWordsViewModel.AddMediaModel.TranscriptionLocation,_dataGridNewWordsViewModel.AddMediaModel.Type.ToString());
                _dataGridNewWordsViewModel.LastLearnedWordIndex = index + 1;
                _dataGridNewWordsViewModel.LastLearnedText = "Last Learned Word Index : " + (index + 1).ToString();
            }
            removeWordFromMembers(index);
            _dataGridNewWordsViewModel.AlternativeName = "";
            _dataGridNewWordsViewModel.TabLearnViewModel.notifyTheMainViewModelForUpdate();
        }

        private void addWordToDB(int index)
        {
            TempWord word = _dataGridNewWordsViewModel.MembersModel.TempWordList[index];
            AddWord.AddWordToDB(word, _dataGridNewWordsViewModel.AlternativeName.Split(","),
                _dataGridNewWordsViewModel.AddMediaModel.Type);
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
            _dataGridNewWordsViewModel.MembersModel.updateGrid(v);
            _dataGridNewWordsViewModel.Members = _dataGridNewWordsViewModel.MembersModel.CurrentMembers;
            _dataGridNewWordsViewModel.PageNumString = "Page: " + _dataGridNewWordsViewModel.MembersModel.Current_page.ToString();
            _dataGridNewWordsViewModel.checkIfLastPage();
        }

        private void removeWordFromMembers(int index)
        {
            _dataGridNewWordsViewModel.RemoveItemFromCurrent(index);
            ObservableCollection<Models.MemberNewWord> members = _dataGridNewWordsViewModel.MembersModel.CurrentMembers;
            ObservableCollection<Models.MemberNewWord> members_copy = _dataGridNewWordsViewModel.MembersModel.AllMembers;
            members.Remove(members_copy[index]);
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
