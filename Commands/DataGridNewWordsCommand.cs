using LangDataAccessLibrary.DataAccess;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using Microsoft.EntityFrameworkCore;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace SubProgWPF.Commands
{
    public class DataGridNewWordsCommand : CommandBase
    {
        private readonly DataGridNewWordsViewModel _dataGridNewWordsViewModel;
        private string _tvSeriesName;
        private string _seasonIndex;
        private string _episodeIndex;

        public string TvSeriesName_ { get => _tvSeriesName; set => _tvSeriesName = value; }
        public string SeasonIndex_ { get => _seasonIndex; set => _seasonIndex = value; }
        public string EpisodeIndex_ { get => _episodeIndex; set => _episodeIndex = value; }

        public DataGridNewWordsCommand(DataGridNewWordsViewModel dataGridNewWordsViewModel)
        {
            _dataGridNewWordsViewModel = dataGridNewWordsViewModel;
            TvSeriesName_ = dataGridNewWordsViewModel.DataGridNewWordModel.AddMediaModel.MediaName;
            SeasonIndex_ = dataGridNewWordsViewModel.DataGridNewWordModel.AddMediaModel.SeasonIndex;
            EpisodeIndex_ = dataGridNewWordsViewModel.DataGridNewWordModel.AddMediaModel.EpisodeIndex;
        }

        public override void Execute(object parameter)
        {
            //var values = (object[])parameter;
            //var num = values[0];
            //var op = values[1];
            


            if(IsNumeric(parameter.ToString())){
                int index = Int32.Parse(parameter.ToString()) - 1;
                List<TempWord> words = _dataGridNewWordsViewModel.MembersModel.getWordsCopy();
                Word word = new Word() {
                    Name = words[index].Name,
                    InitDate = DateTime.Now,
                    Repetition = new List<Repetition>(),
                    WordContext_Ids = words[index].WordContext_Ids
                    //WordContexts = words[index].WordContexts
                };

                //for (int i = 0; i < words[index].WordContext_Ids.Count; i++)
                //{
                //    //WordContext wC = words[index].WordContexts[i];
                //    WordContext wC = WordServices.getWordContextByID(words[index].WordContext_Ids[i]);
                //    word.WordContexts.Add(new WordContext()
                //    {
                //        Address = new Address() { SubLocation = wC.Address.SubLocation,
                //            TranscriptionAddress_Id = wC.Address.TranscriptionAddress_Id
                //        }, Content = wC.Content, Type = wC.Type

                //    });
                    
                //}

                if(_dataGridNewWordsViewModel.AlternativeName.Length > 0)
                {
                    List<string> altNames = _dataGridNewWordsViewModel.AlternativeName.Split(",").ToList();
                    altNames = trimList(altNames);
                    word.Name = altNames[0];
                    List<WordData> InflectedWords = new List<WordData>();
                    for(int i = 1; i < altNames.Count; i++)
                    {
                        WordData wData = new WordData();
                        wData.Word = word;
                        wData.Name = altNames[i];
                        InflectedWords.Add(wData);
                    }
                    word.WordInflections = InflectedWords;
                }
                _dataGridNewWordsViewModel.AlternativeName = "";
                DatabaseWordCreator dWC = new DatabaseWordCreator();
                dWC.CreateWord(word);

                //MediaServices.updateEpisodePlayHead(
                //    _dataGridNewWordsViewModel.DataGridNewWordModel.AddMediaModel.EpisodeIndex, 
                //    TvSeriesName_, 
                //    SeasonIndex_, 
                //    words[index].WordContexts[0].Address.SubLocation);
                TempServices.UpdatePlayHead(words[index]);
                TempServices.deleteTempWord(words[index]);

                //WordEntryCreator wordEntry = new WordEntryCreator();
                //wordEntry.CreateWordEntry(words[index]);

                //TVSeriesCreator tV = new TVSeriesCreator();
                //FTVEpisode fTVEpisode = new FTVEpisode();
                //fTVEpisode.EpisodeIndex = Int32.Parse(EpisodeIndex_);
                //fTVEpisode.SubLocation = _dataGridNewWordsViewModel.ScanSourceLocation;
                //int IsSuccess = tV.CreateEpisode(fTVEpisode, TvSeriesName_, SeasonIndex_);
                //if(IsSuccess != -1)
                //{

                _dataGridNewWordsViewModel.RemoveItemFromCurrent(index);
                ObservableCollection<Models.MemberNewWord> members = _dataGridNewWordsViewModel.MembersModel.GetCurrentGridItems();
                ObservableCollection<Models.MemberNewWord> members_copy = _dataGridNewWordsViewModel.MembersModel.GetAllMembers();
                members.Remove(members_copy[index]);







                //}

                //FTVSeason
                //tV.CreateEpisode()
                //tV.CreateTVSeries(TvSeriesName_, Int32.Parse(SeasonIndex_), Int32.Parse(EpisodeIndex_), _dataGridNewWordsViewModel.ScanSourceLocation);



                //FTVEpisode fTV = wordEntry.getEpisode(TvSeriesName_, Int32.Parse(SeasonIndex_), Int32.Parse(EpisodeIndex_));



                //if (!MediaExistenceValidators.checkIfTVSeriesExists(TvSeriesName_))
                //{
                //    tV.CreateTVSeries(TvSeriesName_, Int32.Parse(SeasonIndex_), Int32.Parse(EpisodeIndex_), _dataGridNewWordsViewModel.ScanSourceLocation);
                //}


                //createDatabaseEntry(words[index]);

            }
            else
            {
                _dataGridNewWordsViewModel.MembersModel.updateGrid(parameter.ToString());
                _dataGridNewWordsViewModel.Members = _dataGridNewWordsViewModel.MembersModel.GetCurrentGridItems();
                //_dataGridNewWordsViewModel.members = _dataGridNewWordsViewModel.MembersModel.GetCurrentGridItems();
                _dataGridNewWordsViewModel.PageNum = _dataGridNewWordsViewModel.MembersModel.Current_page.ToString();
            }
            
        }

        private List<string> trimList(List<string> altNames)
        {
            for(int i = 0; i < altNames.Count; i++)
            {
                altNames[i] = altNames[i].Trim();
            }
            return altNames;
        }

        private bool checkIfSeasonExists(string seasonIndex)
        {
            DataContextFactory contextFactory = new DataContextFactory();
            using (WordContextDB wordContextDB = contextFactory.CreateDbContext(new string[] { "" }))
            {
                return wordContextDB.TVSeriesSeasons.Any(s => s.SeasonIndex == Int32.Parse(SeasonIndex_) && s.Series.Name.Equals(TvSeriesName_));
            }
        }

        

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
