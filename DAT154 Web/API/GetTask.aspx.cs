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
                string category = HttpContext.Current.Request["category"];
                
                List<Task> task = Data.getTasks(null,1,Convert.ToInt32(category));
                List<Task> task2 = Data.getTasks(null, 2, Convert.ToInt32(category));

                json = JsonConvert.SerializeObject(new { status = "OK", task = task });
                
            } else {
                json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Request must be POST." });
            }
        }
    }
}