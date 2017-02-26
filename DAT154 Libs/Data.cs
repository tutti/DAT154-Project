using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System;

namespace DAT154_Libs {
    public class Data {
        private static string host = "localhost";
        private static string user = "hotel";
        private static string pass = "hotel";
        private static string db = "hotel";

        private static DataContext cont = new DataContext(
            @"Data Source=" + host +
            ";Initial Catalog= " + db +
            ";User ID=" + user +
            ";Password=" + pass);

        /*
         * Saving data to database
         */

        public static void save() {
            cont.SubmitChanges();
        }

        public static void insert(User user) {
            Table<User> tbl = cont.GetTable<User>();
            tbl.InsertOnSubmit(user);
        }

        public static void insert(Task task) {
            Table<Task> tbl = cont.GetTable<Task>();
            tbl.InsertOnSubmit(task);
        }

        public static void insert(Room room) {
            Table<Room> tbl = cont.GetTable<Room>();
            tbl.InsertOnSubmit(room);
        }

        /*
         * Room functions
         */

        public static Room getRoomById(int id) {
            Table<Room> tbl = cont.GetTable<Room>();

            //var result = tbl.Where(room => room.id == id);
            var result = from room in tbl where room.id == id select room;

            if (result.Count() == 0) {
                return null;
            }
            return result.First<Room>();
        }

        public static Room getRoomByRoomNumber(int room_number) {
            Table<Room> tbl = cont.GetTable<Room>();

            var result = from room in tbl where room.room_number == room_number select room;

            if (result.Count<Room>() == 0) {
                return null;
            }
            return result.First<Room>();
        }

        public static List<Room> getRooms(
            int minBeds = -1,
            int maxBeds = -1,
            int minSize = -1,
            int maxSize = -1,
            int minQuality = -1,
            int maxQuality = -1,
            DateTime? startDate = null,
            DateTime? endDate = null
        ) {
            Table<Room> tbl = cont.GetTable<Room>();
            var query = from room in tbl select room;

            if (minBeds >= 0) query = from room in query where room.beds >= minBeds select room;
            if (maxBeds >= 0) query = from room in query where room.beds <= maxBeds select room;
            if (minSize >= 0) query = from room in query where room.room_size >= minSize select room;
            if (maxSize >= 0) query = from room in query where room.room_size <= maxSize select room;
            if (minQuality >= 0) query = from room in query where room.quality >= minQuality select room;
            if (maxQuality >= 0) query = from room in query where room.quality <= maxQuality select room;

            List<Room> rooms = query.ToList<Room>();

            if (startDate != null && endDate != null) {
                Table<Booking> bookingTbl = cont.GetTable<Booking>();
                var bookingQuery =
                    from booking in bookingTbl
                    where booking.start_date < endDate
                    where booking.end_date > startDate
                    where booking.booking_status != Booking.STATUS.CANCELED
                    select booking.room_id;

                List<int> bookedRooms = bookingQuery.ToList<int>();

                rooms = rooms.Where(room => !bookedRooms.Contains(room.id)).ToList<Room>();
            }

            return rooms;
        }

        /*
         * User functions
         */

        public static User getUserById(int id) {
            Table<User> tbl = cont.GetTable<User>();

            var result = from user in tbl where user.id == id select user;

            if (result.Count<User>() == 0) {
                return null;
            }
            return result.First<User>();
        }

        public static User getUserByEmail(string email) {
            Table<User> tbl = cont.GetTable<User>();

            var result = from user in tbl where user.email == email select user;

            if (result.Count<User>() == 0) {
                return null;
            }
            return result.First<User>();
        }

        public static List<User> getUsersByName(string name) {
            Table<User> tbl = cont.GetTable<User>();

            var result = from user in tbl where user.name == name select user;

            return result.ToList<User>();
        }

        public static List<User> getAllUsers() {
            Table<User> tbl = cont.GetTable<User>();

            var result = from user in tbl select user;
            return result.ToList<User>();
        }

        /*
         * Booking functions
         */

        public static Booking getBookingById(int id) {
            Table<Booking> tbl = cont.GetTable<Booking>();

            var result = from booking in tbl where booking.id == id select booking;

            if (result.Count<Booking>() == 0) {
                return null;
            }
            return result.First<Booking>();
        }

        public static List<Booking> getBookings(
            User user = null,
            Room room = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            bool includeCanceled = true
        ) {
            Table<Booking> tbl = cont.GetTable<Booking>();

            var query = from booking in tbl select booking;

            if (user != null) query = from booking in query where booking.user_id == user.id select booking;
            if (room != null) query = from booking in query where booking.room_id == room.id select booking;
            if (startDate != null && endDate != null) query = from booking in query where booking.start_date < endDate where booking.end_date > startDate select booking;
            if (!includeCanceled) query = from booking in query where booking.booking_status != Booking.STATUS.CANCELED select booking;

            return query.ToList<Booking>();
        }

        public static Booking bookRoom(User user, Room room, DateTime startDate, DateTime endDate) {
            Booking booking = new Booking();
            booking.user_id = user.id;
            booking.room_id = room.id;
            booking.start_date = startDate;
            booking.end_date = endDate;
            Table<Booking> tbl = cont.GetTable<Booking>();
            tbl.InsertOnSubmit(booking);
            cont.SubmitChanges();

            return booking;
        }

        /*
         * Task functions
         */

        public static Task getTaskById(int id) {
            Table<Task> tbl = cont.GetTable<Task>();

            var result = from task in tbl where task.id == id select task;

            if (result.Count<Task>() == 0) {
                return null;
            }
            return result.First<Task>();
        }

        public static List<Task> getTasks(Room room = null, int? status = null, int? category = null) {
            Table<Task> tbl = cont.GetTable<Task>();

            var query = from task in tbl select task;

            if (room != null) query = from task in query where task.room_id == room.id select task;
            //if (status != null) query = from task in query where task.status == status select task;
            if (status != null && status != 0) {
                query = query.Where(task => (task.status & status) > 0);
            }
            if (category != null && category != 0) {
                query = query.Where(task => (task.category & category) == category);
            }

            return query.ToList<Task>();
        }
    }
}