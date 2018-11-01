using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AndroidAnalyzer
{
    class Term
    {
        private String Word;
        private String Speech;
        
        public int Tfrequncy;
        public int Dfrequency;

        public double TF;
        public double IDF;

        public Term(String term, String speech)
        {
            SetWord(term);
            SetSpeech(speech);
        }
        public Term(String term, String speech, int Tfrequncy, int Dfrequency)
        {
            SetWord(term);
            SetSpeech(speech);

            this.Tfrequncy = Tfrequncy;
            this.Dfrequency = Dfrequency;
        }
        public Term(String term, String speech, int Tfrequncy, int Dfrequency, double TF, double IDF)
        {
            SetWord(term);
            SetSpeech(speech);

            this.Tfrequncy = Tfrequncy;
            this.Dfrequency = Dfrequency;

            this.TF = TF;
            this.IDF = IDF;
        }
        public void SetWord(String word)
        {
            Word = word;
        }
        public void SetSpeech(String speech)
        {
            Speech = speech;
        }
        public String GetWord()
        {
            return Word;
        }
        public String GetSpeech()
        {
            return Speech;
        }
        public int CompareTo(Term next)
        {
            if (this.Dfrequency > next.Dfrequency)
                return -1;
            if (this.Dfrequency == next.Dfrequency)
                return 0;
            if (this.Dfrequency < next.Dfrequency)
                return 1;
            else
                return 0;
        }
    }
}
