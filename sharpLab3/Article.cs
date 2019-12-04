using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace sharpLab3
{
    [Serializable]
    class Article
    {
        public string ArName { get; set; }
        public int ArSymbols { get; set; }
        public DateTime ArDate { get; set; }
        public Article(string ArName, int ArSymbols, DateTime ArDate)
        {
            this.ArName = ArName;
            this.ArSymbols = ArSymbols;
            this.ArDate = ArDate;
        }
        public Article()
        {
            ArName = "Unknown";
            ArSymbols = 0;
            ArDate = new DateTime();
        }
        public override string ToString()
        {
            string s = "Name of the article: " + ArName + ". Amount of symbals: " + ArSymbols + ". " + "Date: " + ArDate + ".";
            return s;
        }
    }
}
