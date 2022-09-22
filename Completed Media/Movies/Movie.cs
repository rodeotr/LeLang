using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SubProgWPF.Completed_Media.Movies
{
    class Movie
    {
        private string name;
        private string finishDate;
        private string file_name;
        private string length;      // format is 00:00:00
        private string totalLearnedWords;
        private string token;

        public Movie(string name, string file_name, string finishDate, string length, string totalLearnedWords)
        {
            this.name = name;
            this.finishDate = finishDate;
            this.length = length;
            this.totalLearnedWords = totalLearnedWords;
            this.file_name = file_name;
            this.token = generateToken();
        }

        public string Name { get => name; set => name = value; }
        public string FinishDate { get => finishDate; set => finishDate = value; }
        public string File_name { get => file_name; set => file_name = value; }
        public string Length { get => length; set => length = value; }
        public string TotalLearnedWords { get => totalLearnedWords; set => totalLearnedWords = value; }
        public string Token { get => token; set => token = value; }

        private string generateToken()
        {
            byte[] saltByte = new byte[9];

            using (var random = new RNGCryptoServiceProvider())
            {

                random.GetBytes(saltByte);
                return Convert.ToBase64String(saltByte);
            }


        }
    }


}
