using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT154_Libs
{
    class Program
    {
        static void Main(string[] args)
        {
            Room room = Data.getRoomById(5);
            Console.WriteLine(room.room_number);
            Console.WriteLine("Moo.");
            Console.ReadKey();
        }
    }
}
