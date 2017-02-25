using DAT154_Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web {
    public partial class Search : System.Web.UI.Page {

        protected List<string> errors = new List<string>();
        protected void Page_Load(object sender, EventArgs e) {
            errorList.DataSource = errors;
            errorList.DataBind();

            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string s_minBeds = HttpContext.Current.Request["ctl00$MainContent$minBeds"];
                string s_maxBeds = HttpContext.Current.Request["ctl00$MainContent$maxBeds"];
                string s_minSize = HttpContext.Current.Request["ctl00$MainContent$minSize"];
                string s_maxSize = HttpContext.Current.Request["ctl00$MainContent$maxSize"];
                string s_minQuality = HttpContext.Current.Request["ctl00$MainContent$minQuality"];
                string s_maxQuality = HttpContext.Current.Request["ctl00$MainContent$maxQuality"];
                string s_startDate = HttpContext.Current.Request["ctl00$MainContent$startDate"];
                string s_endDate = HttpContext.Current.Request["ctl00$MainContent$endDate"];

                if (s_startDate == null || s_startDate.Equals("")
                    || s_endDate == null || s_endDate.Equals("")) {
                    errors.Add("You must date! Or no hotel!");
                    return;
                }

                int minBeds = -1;
                int maxBeds = -1;
                int minSize = -1;
                int maxSize = -1;
                int minQuality = -1;
                int maxQuality = -1;

                if (s_minBeds != null && !s_minBeds.Equals("")) minBeds = Convert.ToInt32(s_minBeds);
                if (s_maxBeds != null && !s_maxBeds.Equals("")) maxBeds = Convert.ToInt32(s_maxBeds);
                if (s_minSize != null && !s_minSize.Equals("")) minSize = Convert.ToInt32(s_minSize);
                if (s_maxSize != null && !s_maxSize.Equals("")) maxSize = Convert.ToInt32(s_maxSize);
                if (s_minQuality != null && !s_minQuality.Equals("")) minQuality = Convert.ToInt32(s_minQuality);
                if (s_maxQuality != null && !s_maxQuality.Equals("")) maxQuality = Convert.ToInt32(s_maxQuality);
                DateTime startDate = DateTime.Parse(s_startDate);
                DateTime endDate = DateTime.Parse(s_endDate);

                List<Room> rooms = Data.getRooms(
                    minBeds: minBeds,
                    maxBeds: maxBeds,
                    minSize: minSize,
                    maxSize: maxSize,
                    minQuality: minQuality,
                    maxQuality: maxQuality,
                    startDate: startDate,
                    endDate: endDate
                );

                if (rooms.Count() == 0) {
                    errors.Add("Apologise we, not available rooms. Search other please.");
                } else {
                    Response.Redirect(
                        "/SearchResults?r=" + rooms[0].id + "&s=" + s_startDate + "&e=" + s_endDate
                    );
                }
            }

            if (HttpContext.Current.Request["ctl00$MainContent$minBeds"] != null) {
                errors.Add("Apologise we, not available rooms. Search other please.");
            }
        }
    }
}