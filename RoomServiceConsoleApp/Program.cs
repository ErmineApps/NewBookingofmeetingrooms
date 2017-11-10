using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RoomService;

namespace RoomServiceConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceRoom = new ServiceRoomEntry();

            Console.WriteLine("Service is starting");
            serviceRoom.StartService();
            Console.WriteLine("Service is started");
            Console.WriteLine("Press ENTER for exit...");
            Console.ReadLine();
        }

        static string checkIp()
        {
            StreamReader reader;
            HttpWebRequest httpWebRequest;
            HttpWebResponse httpWebResponse;

            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://checkip.dyndns.org");
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                reader = new StreamReader(httpWebResponse.GetResponseStream());
                return System.Text.RegularExpressions.Regex.Match(reader.ReadToEnd(), @"(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})").Groups[1].Value;
            }
            catch
            {
                return "error";
            }
        }
    }
}
