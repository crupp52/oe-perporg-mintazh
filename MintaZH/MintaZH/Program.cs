using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MintaZH
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            Task.Run(() =>
            {
                while (true)
                {
                    Bank.Update();
                    Thread.Sleep(3000);
                }
            });

            Bank.ReszvenyErtekek.TryAdd("ABC", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("BBC", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("CBS", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("DNN", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("EEE", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("FTC", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("GOT", rnd.Next(50, 151));
            Bank.ReszvenyErtekek.TryAdd("HBO", rnd.Next(50, 151));

            for (int i = 0; i < Bank.Mindenki.Count; i++)
            {
                int j = i;
                Task.Run(() =>
                {
                    while (true)
                    {
                        Bank.Mindenki[j].Work();
                        Thread.Sleep(500);
                    }
                });
            }

            Task log = new Task(() =>
            {
                while (true)
                {
                    Logger();
                    Thread.Sleep(500);
                }
            });

            log.Start();
            Task.WaitAll(log);
        }

        static void Logger()
        {
            Console.Clear();
            foreach (var item in Bank.ReszvenyErtekek)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }
            foreach (var item in Bank.Mindenki)
            {
                Console.WriteLine(item);
            }
        }
    }
}
