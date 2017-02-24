using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAT154_Libs;

namespace DAT154_Desktop {
    class DataAccess {
        public static void submitTask(DAT154_Libs.Task task) {
            Data.insert(task);
            save();
        }

        public static void bookRoom(DAT154_Libs.User user, DAT154_Libs.Room room, DateTime startDate, DateTime endDate) {
            Data.bookRoom(user, room, startDate, endDate);
            save();
        }

        public static void changeBookingStatus(DAT154_Libs.Booking booking, int status) {
            booking.booking_status = status;
            save();
        }

        public static Room getRoomById(int id) {
            return Data.getRoomById(id);
        }

        public static User getUserById(int id) {
            return Data.getUserById(id);
        }

        private static void save() {
            Data.save();
        }

        public static Room getRoomByRoomNumber(int room_number) {
            return Data.getRoomByRoomNumber(room_number);
        }

        public static List<Room> getRooms(
            int minBeds = -1,
            int maxBeds = -1,
            int minSize = -1,
            int maxSize = -1,
            int minQuality = -1,
            int maxQuality = -1,
            DateTime? startDate = null,
            DateTime? endDate = null) {
            return Data.getRooms(minBeds, maxBeds, minSize, maxSize, minQuality, maxQuality, startDate, endDate);
        }

        public static List<DAT154_Libs.Task> getTasks(Room room = null, int? status = null, int? category = null) {
            return Data.getTasks(room, status, category);
        }

        public static User getUserByEmail(string email) {
            return Data.getUserByEmail(email);
        }

        public static List<User> getUsersByName(string name) {
            return Data.getUsersByName(name);
        }

        public static List<User> getAllUsers() {
            return Data.getAllUsers();
        }

        public static List<DAT154_Libs.Booking> getBookings(
            User user = null,
            Room room = null,
            DateTime? startDate = null,
            DateTime? endDate = null
        ) {
            return Data.getBookings(user, room, startDate, endDate);
        }

        public static void populateDB() {
            User user = new User();
            user.email = "test@test.com";
            user.password = "abcdefg";
            user.name = "Testy Testerson";
            user.type = 0;
            Data.insert(user);

            for (int i = 1; i <=5; i++) {
                for (int j = 1; j<=25;j++) {
                    Room room = new Room();
                    room.beds = 1 + (j % 7);
                    room.quality = (i + j) % 3;
                    room.room_size = j % 3;
                    room.room_number = 100 * i + j;
                    Data.insert(room);
                }
            }

            save();
        }
    }
}
