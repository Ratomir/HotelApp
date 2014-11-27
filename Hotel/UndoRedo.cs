using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    class UndoRedo
    {
        private bool tip;

        public bool Tip
        {
            get { return tip; }
            set { tip = value; }
        }

        private string tekst;

        public string Tekst
        {
            get { return tekst; }
            set { tekst = value; }
        }

        public UndoRedo()
        {
            Tip = true;
            Tekst = "";
        }

        public UndoRedo(bool tip, string tekst)
        {
            Tip = tip;
            Tekst = tekst;
        }

    }
}
