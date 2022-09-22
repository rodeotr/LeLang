using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SubProgWPF
{
    class TVSeriesEpisode
    {
        private string episodeName;
        private string file_path;
        private string episodeFinishDate;
        private string episodeLength;
        private string totalLearnedWords;
        private string token;

        public TVSeriesEpisode(string episodeName, string file_path, string episodeFinishDate, string episodeLength, string totalLearnedWords)
        {
            this.episodeName = episodeName;
            this.episodeFinishDate = episodeFinishDate;
            this.episodeLength = episodeLength;
            this.totalLearnedWords = totalLearnedWords;
            this.file_path = file_path;
            this.token = generateToken();
        }

        public string EpisodeName { get => episodeName; set => episodeName = value; }
        public string EpisodeFinishDate { get => episodeFinishDate; set => episodeFinishDate = value; }
        public string EpisodeLength { get => episodeLength; set => episodeLength = value; }
        public string File_path { get => file_path; set => file_path = value; }
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
