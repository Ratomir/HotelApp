using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class Soba
    {
        private int brojSobe;

        public int BrojSobe
        {
            get { return brojSobe; }
            set { brojSobe = value; }
        }

        private string stanje;

        public string Stanje
        {
            get { return stanje; }
            set { stanje = value; }
        }

        private string tipSobe;

        public string TipSobe
        {
            get { return tipSobe; }
            set { tipSobe = value; }
        }

        private string datumPrijave;

        public string DatumPrijave
        {
            get { return datumPrijave; }
            set { datumPrijave = value; }
        }

        private string datumOdjave;

        public string DatumOdjave
        {
            get { return datumOdjave; }
            set { datumOdjave = value; }
        }

        public Soba(int broj, string stanje, string tip, string datumPrijave, string datumOdjave)
        {
            BrojSobe = broj;
            Stanje = stanje;
            TipSobe = tip;
            DatumPrijave = datumPrijave;
            DatumOdjave = datumOdjave;
        }
    }
}
