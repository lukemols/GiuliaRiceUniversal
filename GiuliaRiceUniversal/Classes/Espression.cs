using System;
using System.Collections.Generic;
using System.Text;

namespace GiuliaRiceUniversal.Classes
{
    class Espression
    {
        string eng;
        string ita;

        public string Eng { get { return eng; } }
        public string Ita { get { return ita; } }
        public Espression(string e, string i)
        {
            eng = e;
            ita = i;
        }
    }
}
