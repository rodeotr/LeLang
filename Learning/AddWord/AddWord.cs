﻿using LangDataAccessLibrary;
using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using LangDataAccessLibrary.Services.WordCreators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SubProgWPF.Learning.AddWord
{
    public class AddWord
    {

        public static void AddWordToDB(TempWord word, string[] alternativeNames, MediaTypes.TYPE type)
        {
            createTheSelectedWord(word, alternativeNames, type);

            //TempServices.UpdatePlayHead(word, wordIndex);
            //TempServices.IncrementTotalWordCount(word);



            //TempServices.deleteTempWordFromDB(word);

            
        }

        private static void createTheSelectedWord(TempWord tempWord, string[] alternativeNames, MediaTypes.TYPE type)
        {
            Word word = new Word()
            {
                Name = tempWord.Name,
                InitDate = DateTime.Now,
                Repetition = new List<Repetition>(),
                WordContext_Ids = tempWord.WordContext_Ids,
                TypeOfLearnedMedium = type.ToString()
            };
            

            if (alternativeNames.Length > 0)
            {
                if(alternativeNames[0].Length != 0)
                {
                    word = addInclinationWords(word, alternativeNames);
                }
            }
            
            int result = WordCreator.CreateWord(word);
            if (result == -1)
            {
                MessageBox.Show("The word already exists.");
            }
        }
        private static Word addInclinationWords(Word word, string[] alternativeNames)
        {
            
            List<string> altNames = new List<string>(alternativeNames);
            altNames = trimList(altNames);
            word.Name = altNames[0];
            List<WordData> InflectedWords = new List<WordData>();
            for (int i = 1; i < altNames.Count; i++)
            {
                WordData wData = new WordData();
                wData.Word = word;
                wData.Name = altNames[i];
                InflectedWords.Add(wData);
            }
            word.WordInflections = InflectedWords;
            return word;
        }

        private static List<string> trimList(List<string> altNames)
        {
            for (int i = 0; i < altNames.Count; i++)
            {
                altNames[i] = altNames[i].Trim();
            }
            return altNames;
        }
    }
}
