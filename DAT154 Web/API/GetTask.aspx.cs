﻿using DAT154_Libs;
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
            string task_id = HttpContext.Current.Request["task_id"];

            Task task = Data.getTaskById(Convert.ToInt32(task_id));

            json = JsonConvert.SerializeObject(new { status = "OK", task = task });
        }
    }
}