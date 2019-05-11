using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH
{
    class Broker
    {
        static Random rnd = new Random();
        static int globalID = 0;

        public double Penz { get; set; }
        public List<Reszveny> Reszvenyek { get; set; }
        public int ID { get; set; }

        public Broker()
        {
            Penz = rnd.Next(5000, 15000);
            Reszvenyek = new List<Reszveny>();
            ID = globalID++;
        }

        public void Work()
        {
            lock (Bank._lock)
            {
                var legolcsobbReszveny = (from e in Bank.ReszvenyErtekek
                                          orderby e.Value descending
                                          select e).First();

                if (Penz >= legolcsobbReszveny.Value)
                {
                    var broker = (from e in Bank.Mindenki
                                 where (from f in e.Reszvenyek
                                        where f.Nev == legolcsobbReszveny.Key && f.Eleado
                                        select f) != null && e != this
                                 select e).FirstOrDefault();

                    if (broker != null)
                    {
                        var reszveny = from e in (broker as Broker).Reszvenyek
                                       where e.Nev == legolcsobbReszveny.Key
                                       select e;

                        Broker b = broker as Broker;
                        b.Reszvenyek.Remove(reszveny as Reszveny);
                        b.Penz += legolcsobbReszveny.Value;
                    }

                    Penz -= legolcsobbReszveny.Value;
                    Reszvenyek.Add(new Reszveny()
                    {
                        Nev = legolcsobbReszveny.Key,
                        Ertek = legolcsobbReszveny.Value,
                        Tulajdonos = this,
                        Eleado = false
                    });
                }

                foreach (var item in Reszvenyek)
                {
                    var q = (from e in Bank.ReszvenyErtekek
                             where e.Key == item.Nev
                             select e.Value).FirstOrDefault();

                    if (item.Ertek / q > 1.05)
                    {
                        item.Eleado = true;
                    }
                    else
                    {
                        item.Eleado = false;
                    }
                }
            }
        }

        public override string ToString()
        {
            string o = $"Broker#{ID}: {Penz}";

            lock (Bank._lock)
            {
                foreach (var item in Reszvenyek)
                {
                    double q = (from e in Bank.ReszvenyErtekek
                                where e.Key == item.Nev
                                select e.Value).First();

                    o += $"\n\t{item} {(item.Ertek / q) * 100}";
                }
            }

            return o;
        }
    }
}
