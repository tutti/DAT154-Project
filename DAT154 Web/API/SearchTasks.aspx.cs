using DAT154_Libs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web.API {
    public partial class SearchTasks : System.Web.UI.Page {

        protected string json = "";
        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                //string token = HttpContext.Current.Request["token"];
                string room_id = HttpContext.Current.Request["room_id"];
                string _category = HttpContext.Current.Request["category"];
                string _status = HttpContext.Current.Request["status"];

                /*if (Application["api:token-" + token] == null) {
                    json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Invalid user token." });
                    return;
                }*/

                Room room = null;
                if (room_id != null && !room_id.Equals("")) room = Data.getRoomById(Convert.ToInt32(room_id));
                int? status = null;
                if (_status != null && !_status.Equals("")) status = Convert.ToInt32(_status);
                int? category = null;
                if (_category != null && !_category.Equals("")) category = Convert.ToInt32(_category);

                List<Task> tasks = Data.getTasks(room: room, status: status, category: category);

                json = JsonConvert.SerializeObject(new { status = "OK", tasks = tasks });

            }
            else {
                json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Request must be POST." });
            }
        }
    }
}