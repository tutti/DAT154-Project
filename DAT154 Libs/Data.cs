using System.Linq;
using System.Data.Linq;

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

        public static Room getRoomById(int id) {
            Table<Room> tblRoom = cont.GetTable<Room>();

            //var result = tblRoom.Where((Room room, int idx) => (room.id == id));
            var result = from room in tblRoom where room.id == id select room;

            if (result.Count<Room>() == 0) {
                return null;
            }
            return result.First<Room>();
        }

        public static Room getRoomByRoomNumber(int room_number) {
            Table<Room> tblRoom = cont.GetTable<Room>();

            var result = from room in tblRoom where room.room_number == room_number select room;

            if (result.Count<Room>() == 0) {
                return null;
            }
            return result.First<Room>();
        }

        
    }
}