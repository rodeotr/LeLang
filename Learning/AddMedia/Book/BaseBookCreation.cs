using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.Utils;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SubProgWPF.Learning.AddMedia
{
    public abstract class BaseBookCreation : BaseMediaCreation, IBookCreation
    {

       

        // returns true if operation is successful, false when not.
        public abstract bool createBook(IBook book);
        public override void saveTheWords(IMedia iMedia, BackgroundWorker worker) { }

        public void saveTheWords(IBook book, BackgroundWorker worker) {
            GetWordsFromPDFFile getWordsFromPDF = new GetWordsFromPDFFile(book);
            List<TempWord> tempWords = getWordsFromPDF.GetAllWordObjectsFromPDF(1, worker);

            TempServices.addWordsToTempWords(tempWords);
        }



    }
}
