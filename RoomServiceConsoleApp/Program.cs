using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
