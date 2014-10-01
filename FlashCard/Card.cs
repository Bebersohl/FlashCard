using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard
{
    public class Card
    {
        private int _row;
        private string _englishWord;
        private string _greekWord;

        public Card(int r, string e, string g)
        {
            _row = r;
            _englishWord = e;
            _greekWord = g;
        }

        public int row
        {
            get { return _row; }
            set { _row = value; }
        }
        public string englishWord
        {
            get { return _englishWord; }
            set { _englishWord = value; }
        }
        public string greekWord
        {
            get { return _greekWord; }
            set { _greekWord = value; }
        }

        public string toString()
        {
            return row + " " + _englishWord + " " + _greekWord;
        }
    }
}
