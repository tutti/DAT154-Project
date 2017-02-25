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

        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                int room_id = Convert.ToInt32(HttpContext.Current.Request["ctl00$MainContent$room_id"]);
                
                
            } else {
                int room_id = Convert.ToInt32(HttpContext.Current.Request["r"]);

                foundRoom = Data.getRoomById(room_id);
            }
        }
    }
}