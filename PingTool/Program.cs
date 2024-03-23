

using System.Net.NetworkInformation;

namespace PingTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool online = false; //是否在线
            Ping ping = new Ping();
            PingReply pingReply = ping.Send("192.168.1.251");
            List<string> addrs = new List<string>();
            for (int i = 0; i < 255; i++)
            {
                addrs.Add($"192.168.1.{i}");
            }
            MultPing(addrs);
        }

        static void MultPing(List<string> list)
        {
            var replys=list.Select(t =>
            {
                Ping p = new Ping();
                return p.SendPingAsync(t);
            }).ToArray();

            Task.WaitAll(replys);

            for (int i = 0;i < 255;i++)
            {
                PingReply pingReply = replys[i].Result;
                if (pingReply.Status == IPStatus.Success)
                {
                    Console.ForegroundColor = pingReply.Status == IPStatus.Success ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine($"{list[i]},{pingReply.Status}，{pingReply.RoundtripTime}ms");
                }

            }
        }
    }
}
