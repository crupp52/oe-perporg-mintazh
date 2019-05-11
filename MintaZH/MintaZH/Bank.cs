using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintaZH
{
    static class Bank
    {
        static Random rnd = new Random();

        public static object _lock = new object();
        static int eltolas = 0;

        public static List<Broker> mindenki = new List<Broker>()
        {
            new Broker(),
            new Broker(),
            new Broker(),
            new Broker(),
            new Broker(),
            new Broker()
        };

        public static List<Broker> Mindenki
        {
            get
            {
                lock (_lock)
                {
                    return mindenki;
                }
            }
        }

        private static ConcurrentDictionary<string, double> reszvenyErtekek = new ConcurrentDictionary<string, double>();

        public static ConcurrentDictionary<string, double> ReszvenyErtekek
        {
            get
            {
                lock (_lock)
                {
                    return reszvenyErtekek;
                }
            }
        }

        public static void Update()
        {
            //eltolas = rnd.Next(-3, 4) + (int)(eltolas * 0.5);

            lock (Bank._lock)
            {
                foreach (var item in ReszvenyErtekek.Keys)
                {
                    //double asd = (100 + eltolas + rnd.Next(-5, 6)) / 100;
                    ReszvenyErtekek[item] += rnd.Next(-30, 31);
                }
            }
        }
    }
}
