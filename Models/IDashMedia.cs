using System;
using System.Collections.Generic;
using System.Text;

namespace SubProgWPF.Models
{
    public interface IDashMedia
    {
        public string Name { get; set; }
        public bool IsFinished { get; set; }
        public int LearnedTotalWords { get; set; }
    }
}
