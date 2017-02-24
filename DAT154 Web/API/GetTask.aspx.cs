using DAT154_Libs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web.API {
    public partial class GetTask : System.Web.UI.Page {

        protected string json = "";
        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string token = HttpContext.Current.Request["token"];
                string task_id = HttpContext.Current.Request["task_id"];

                if (Application["api:token-" + token] == null) {
                    json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Invalid user token." });
                    return;
                }

                Task task = Data.getTaskById(Convert.ToInt32(task_id));

                json = JsonConvert.SerializeObject(new { status = "OK", task = task });
                
            } else {
                json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Request must be POST." });
            }
        }
    }
}