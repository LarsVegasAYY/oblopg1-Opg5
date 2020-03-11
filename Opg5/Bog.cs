using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opg5
{
    public class Bog
    {
        private string titel;
        private string forfatter;
        private int sidetal;
        private string isbn13;

        public string Titel
        {
            get { return titel; }
            set { titel = value; }
        }
        public string Forfatter
        {
            get { return forfatter; }
            set
            {
                if (value.Length >= 2) forfatter = value;
                else throw (new ForfatterException("Forfatter skal minimum bestå af 2 tegn"));
            }
        }
        public int Sidetal
        {
            get { return sidetal; }
            set
            {
                if (value >= 4 && value <= 1000) sidetal = value;
                else throw (new SideAntalException("Sidetal Skal være mellem 4 og 1000"));
            }
        }
        public string Isbn13
        {
            get { return isbn13; }
            set
            {
                if (value.Length == 13) isbn13 = value;
                else throw (new Isbn13Exception("Isbn13 skal bestå af præcis 13 karakterer."));
            }
        }

        public Bog(string titel, string forfatter, int sidetal, string isbn13)
        {
            Titel = titel;
            Forfatter = forfatter;
            Sidetal = sidetal;
            Isbn13 = isbn13;
        }
    }
}
