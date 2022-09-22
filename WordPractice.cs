using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF
{
    public class WordPractice
    {
        private string time;
        private string isSuccess;

        public WordPractice(string time, string isSuccess)
        {
            this.time = time;
            this.isSuccess = isSuccess;
        }

        public string IsSuccess { get => isSuccess; set => isSuccess = value; }
        public string Time { get => time; set => time = value; }
    }
}
