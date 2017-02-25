using DAT154_Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web {
    public partial class Bookings : System.Web.UI.Page {

        protected Dictionary<int, int> room_numbers;
        protected readonly Dictionary<int, string> status = new Dictionary<int, string>() {
            { Booking.STATUS.CANCELED, "Canceled" },
            { Booking.STATUS.BOOKED, "Booked" },
            { Booking.STATUS.PAID, "Paid" },
            { Booking.STATUS.CHECKEDIN, "Checked in" },
            { Booking.STATUS.COMPLETE, "Complete" }
        };

        protected void Page_Load(object sender, EventArgs e) {

            if (Session["user"] == null) {
                Response.Redirect("/Login");
                return;
            }

            User user = (User)Session["user"];

            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string s_cancel_id = HttpContext.Current.Request["cancel_id"];
                if (s_cancel_id != null && !s_cancel_id.Equals("")) {
                    Booking booking = Data.getBookingById(Convert.ToInt32(s_cancel_id));
                    if (booking.user_id == user.id && booking.booking_status == Booking.STATUS.BOOKED) {
                        booking.booking_status = Booking.STATUS.CANCELED;
                        Data.save();
                    }
                }
            }

            List<Booking> bookings = Data.getBookings(user: user);
            room_numbers = bookings.Select(booking => Data.getRoomById(booking.room_id)).Distinct().ToDictionary(room => room.id, room => room.room_number);

            bookingList.DataSource = bookings;
            bookingList.DataBind();

        }
    }
}