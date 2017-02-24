using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT154_Desktop {
    class NotFakeData{
        public static List<DAT154_Libs.Task> getTasks() {
            List<DAT154_Libs.Task> taskList = new List<DAT154_Libs.Task>();
            taskList.Add(new DAT154_Libs.Task() {
                id = 0,
                room_id = 0,
                status = 0,
                category = 1,
                notes = "This is a task"
            });
            taskList.Add(new DAT154_Libs.Task() {
                id = 1,
                room_id = 1,
                status = 1,
                category = 5,
                notes = "This is a different task"
            });
            taskList.Add(new DAT154_Libs.Task() {
                id = 2,
                room_id = 2,
                status = 2,
                category = 16,
                notes = "This is a third task"
            });

            return taskList;
        }

        public static List<DAT154_Libs.Room> getRooms() {
            List<DAT154_Libs.Room> roomList = new List<DAT154_Libs.Room>();

            roomList.Add(new DAT154_Libs.Room() {
                id = 0,
                beds = 1,
                room_number = 101,
                quality = 1,
                room_size = 2
            });
            roomList.Add(new DAT154_Libs.Room() {
                id = 1,
                beds = 2,
                room_number = 102,
                quality = 2,
                room_size = 4
            });
            roomList.Add(new DAT154_Libs.Room() {
                id = 2,
                beds = 1,
                room_number = 103,
                quality = 3,
                room_size = 1
            });

            return roomList;
        }

        public static List<DAT154_Libs.Booking> getBookings() {
            List<DAT154_Libs.Booking> bookingList = new List<DAT154_Libs.Booking>();

            bookingList.Add(new DAT154_Libs.Booking() {
                id = 0,
                room_id = 0,
                user_id = 0,
                booking_status = 2,
                start_date = new DateTime(),
                end_date = new DateTime()
            });
            bookingList.Add(new DAT154_Libs.Booking() {
                id = 1,
                room_id = 2,
                user_id = 1,
                booking_status = 1,
                start_date = new DateTime(),
                end_date = new DateTime()
            });
            bookingList.Add(new DAT154_Libs.Booking() {
                id = 1,
                room_id = 1,
                user_id = 2,
                booking_status = 3,
                start_date = new DateTime(),
                end_date = new DateTime()
            });

            return bookingList;
        }
    }
}
