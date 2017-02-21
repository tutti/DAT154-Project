using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;

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

        public static List<Room> getRoomsByCriteria(
            int minBeds = -1,
            int maxBeds = -1,
            int minSize = -1,
            int maxSize = -1,
            int minQuality = -1,
            int maxQuality = -1
        ) {
            Table<Room> tbl = cont.GetTable<Room>();
            var query = from room in tbl select room;

            if (minBeds >= 0) query = from room in query where room.beds >= minBeds select room;
            if (maxBeds >= 0) query = from room in query where room.beds <= maxBeds select room;
            if (minSize >= 0) query = from room in query where room.room_size >= minSize select room;
            if (maxSize >= 0) query = from room in query where room.room_size <= maxSize select room;
            if (minQuality >= 0) query = from room in query where room.quality >= minQuality select room;
            if (maxQuality >= 0) query = from room in query where room.quality <= maxQuality select room;

            return query.ToList<Room>();
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

        public static List<User> getAllUsers() {
            Table<User> tbl = cont.GetTable<User>();

            var result = from user in tbl select user;
            return result.ToList<User>();
        }

        public static void saveUser(User user) {
            if (user.id == 0) {
                Table<User> tbl = cont.GetTable<User>();
                tbl.InsertOnSubmit(user);
            }
            cont.SubmitChanges();
        }
    }
}