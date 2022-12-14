using LangDataAccessLibrary.Models;
using LangDataAccessLibrary.Services;
using SubProgWPF.Learning.Interfaces;
using SubProgWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SubProgWPF.Learning.AddMedia
{
    public class BookCreationImplementation : BaseBookCreation
    {

        public override bool createBook(IBook book)
        {
            int success = BookCreator.CreateBook(
                new Books()
                {
                    Name = book.Name,
                    PagePerSection = book.PPS
                });

            return success == 1 ? true : false;
        }

       
    }
}
