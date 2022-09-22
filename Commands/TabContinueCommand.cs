using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Models;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SubProgWPF.Commands
{
    public class TabContinueCommand : CommandBase
    {
        private TabContinueMediaViewModel _tabContinueViewModel;

        public TabContinueCommand(TabContinueMediaViewModel tabContinueViewModel)
        {
            _tabContinueViewModel = tabContinueViewModel;
        }

        public override void Execute(object parameter)
        {
            if (_tabContinueViewModel.SelectedMediaName.Length == 0) { return; }
            
            FTVEpisode fTVEpisode = getSelectedEpisode();

            launchNewWordsTab(fTVEpisode);
            
        }

        private void launchNewWordsTab(FTVEpisode fTVEpisode)
        {
            TranscriptionAddress tA = MediaServices.getTranscriptionByID(fTVEpisode.TranscriptionAddress_Id);
            TempMedia tM = TempServices.getTempMediaByTranscription(tA);
            //List<TempWord> allWords = LangUtils.GetAllWordObjects(tA.TranscriptionLocation, Int32.Parse(_tabContinueViewModel.MaxWordFreq), fTVEpisode.PlayHeadPosition);
            List<TempWord> allWords = TempServices.getTempWordsByTempMedia(tM);
            allWords = removePassedWords(tM, allWords);
            MembersModel model = new MembersModel(allWords);
            AddMediaModel addMediaModel = new AddMediaModel();
            addMediaModel.EpisodeIndex = fTVEpisode.EpisodeIndex.ToString();
            addMediaModel.SeasonIndex = fTVEpisode.Season.SeasonIndex.ToString();
            addMediaModel.MediaName = _tabContinueViewModel.SelectedMediaName.Split(",")[0];

            DataGridNewWordModel gridNewWordModel = new DataGridNewWordModel()
            {
                MembersModel = model,
                AddMediaModel = addMediaModel
            };
        

            _tabContinueViewModel.launchGridView(gridNewWordModel);
        }

        private List<TempWord> removePassedWords(TempMedia tM, List<TempWord> allWords)
        {
            string playHead = tM.PlayHeadPosition;
            if (playHead.Equals("00:00:00"))
            {
                return allWords;
            }
            for(int i = 0; i < allWords.Count; i++)
            {
                TempWord tW = allWords[i];
                if(tW.WordContext_Ids.Split(",").Length == 0)
                {
                    continue;
                }
                WordContext wC = WordServices.getWordContextByID(tW.WordContext_Ids[0]);
                if (wC.Address.SubLocation.Equals(tM.PlayHeadPosition))
                {
                    allWords.RemoveRange(0, i + 1);
                    return allWords;
                }
            }
            return allWords;
        }

        private FTVEpisode getSelectedEpisode()
        {
            for (int i = 0; i < _tabContinueViewModel.MediaNames.Length; i++)
            {
                if (_tabContinueViewModel.SelectedMediaName.Equals(_tabContinueViewModel.MediaNames[i]))
                {
                    return _tabContinueViewModel.Episodes[i];
                    
                }
            }
            return null;
        }
    }
}
