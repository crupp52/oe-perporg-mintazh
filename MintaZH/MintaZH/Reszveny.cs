using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH
{
    class Reszveny
    {
        public string Nev { get; set; }
        public double Ertek { get; set; }
        public Broker Tulajdonos { get; set; }
        public bool Eleado { get; set; }

        public override string ToString()
        {
            return $"{Nev} : {Ertek} ({Eleado})";
        }
    }
}
