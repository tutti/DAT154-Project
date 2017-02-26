using DAT154_Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web {
    public partial class SearchResults : System.Web.UI.Page {

        protected Room foundRoom;
        protected string startDate;
        protected string endDate;
        protected bool error = false;

        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string s_room_id = HttpContext.Current.Request["ctl00$MainContent$room_id"];
                startDate = HttpContext.Current.Request["ctl00$MainContent$start_date"];
                endDate = HttpContext.Current.Request["ctl00$MainContent$end_date"];

                int room_id;
                if (s_room_id == null || s_room_id.Equals("")) {
                    Response.Redirect("/Search");
                    return;
                } else {
                    room_id = Convert.ToInt32(s_room_id);
                }
                
                if (Session["user"] == null) {
                    Session["reserved_room_id"] = room_id;
                    Session["reserved_start_date"] = startDate;
                    Session["reserved_end_date"] = endDate;
                    Response.Redirect("/Login");
                    return;
                }

                foundRoom = Data.getRoomById(room_id);

                // Check bookings just in case
                DateTime _startDate = DateTime.Parse(startDate);
                DateTime _endDate = DateTime.Parse(endDate);
                List<Booking> bookings = Data.getBookings(room: foundRoom, startDate: _startDate, endDate: _endDate, includeCanceled: false);

                if (bookings.Count() > 0) {
                    error = true;
                    return;
                }

                Data.bookRoom((User)Session["user"], foundRoom, _startDate, _endDate);

                Response.Redirect("/Bookings");
            } else {
                int _room_id;
                if (Session["reserved_room_id"] != null) {
                    _room_id = (int)Session["reserved_room_id"];
                    startDate = (string)Session["reserved_start_date"];
                    endDate = (string)Session["reserved_end_date"];
                } else {
                    _room_id = Convert.ToInt32(HttpContext.Current.Request["r"]);
                    startDate = HttpContext.Current.Request["s"];
                    endDate = HttpContext.Current.Request["e"];
                }
                

                foundRoom = Data.getRoomById(_room_id);

                room_id.Value = "" + _room_id;
                start_date.Value = startDate;
                end_date.Value = endDate;
            }
        }
    }
}