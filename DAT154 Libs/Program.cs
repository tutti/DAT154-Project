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
            //Room room = Data.getRoomById(1);
            //Console.WriteLine(room.room_number);
            //Console.ReadKey();

            //List<Room> rooms = Data.getRoomsByCriteria(maxSize: 14);
            //foreach (Room room in rooms) {
            //    Console.WriteLine(room.room_number);
            //}
            //Console.ReadKey();

            User user = Data.getUserById(1);
            user.password = "abc";
            Console.WriteLine(user.passhash);
            Console.WriteLine(user.verifyPassword("abc"));
            Console.WriteLine(user.id);
            Data.saveUser(user);

            User user2 = new User();
            user2.name = "Magnar";
            user2.type = 1;
            user2.email = "magnar@gya.no";
            user2.password = "magnar";
            Data.saveUser(user2);

            Console.WriteLine(user2.id);
            Console.WriteLine("Done.");

            Console.ReadKey();
        }
    }
}