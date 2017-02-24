using DAT154_Libs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web.API {
    public partial class GetRoom : System.Web.UI.Page {
        
        protected string json = "";

        protected void Page_Load(object sender, EventArgs e) {
            string room_id = HttpContext.Current.Request["room_id"];

            Room room = Data.getRoomById(Convert.ToInt32(room_id));

            json = JsonConvert.SerializeObject(new { status = "OK", room = room });
        }
    }
}