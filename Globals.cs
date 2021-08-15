using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Leaf.xNet;

namespace Spirit_Airlines
{
    public static class Globals
    {
        public static readonly ConcurrentBag<string> Queue = new ConcurrentBag<string>();
        public static readonly List<string> Proxies = new List<string>();

        public static int Threads = 500;
        public static int OutputBad = 2;
        public static int OutputCustom = 2;
        public const int Timeout = 500;
        public static ProxyType EProxyType = ProxyType.Socks4;

        public static readonly string ResultsPath = $@"Results\{DateTime.Now:yyyy-MM-dd-HH-mm-ss}";
    }
}