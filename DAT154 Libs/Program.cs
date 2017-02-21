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

            //List<Room> rooms = Data.getRoomsByCriteria(maxSize: 14);
            //foreach (Room room in rooms) {
            //    Console.WriteLine(room.room_number);
            //}

            /*
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

            List<Room> rooms = Data.getRoomsByCriteria(startDate: DateTime.Parse("2017-01-15"), endDate: DateTime.Parse("2017-01-20"));
            foreach (Room room in rooms) {
                Console.WriteLine(room.room_number);
            }
            
            User user = Data.getUserById(1);

            List<Booking> bookings = Data.getBookings(user: user);
            foreach (Booking booking in bookings) {
                Console.WriteLine(booking.start_date);
                Console.WriteLine(booking.end_date);
                Console.WriteLine();
            }

            User user = Data.getUserById(1);
            Room room = Data.getRoomById(1);
            Data.bookRoom(user, room, DateTime.Parse("2017-02-01"), DateTime.Parse("2017-02-28"));
            Console.WriteLine("Done");

            Task task1 = new Task();
            task1.room_id = 1;
            task1.category = Task.CATEGORY.MAINTENANCE | Task.CATEGORY.LEGAL;
            Data.insert(task1);
            Data.save();
            */

            List<Task> tasks = Data.getTasks(category: Task.CATEGORY.MAINTENANCE);
            foreach (Task task in tasks) {
                Console.WriteLine("{0} {1}", task.id, task.category);
            }

            Console.ReadKey();
        }
    }
}