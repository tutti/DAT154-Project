using DAT154_Libs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web.API {
    public partial class SaveTask : System.Web.UI.Page {

        protected string json;
        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                int task_id = Convert.ToInt32(HttpContext.Current.Request["task_id"]);
                int room_id = Convert.ToInt32(HttpContext.Current.Request["room_id"]);
                int status = Convert.ToInt32(HttpContext.Current.Request["status"]);
                int category = Convert.ToInt32(HttpContext.Current.Request["category"]);
                string notes = HttpContext.Current.Request["notes"];

                Task task;

                if (task_id == 0) {
                    task = new Task();
                    Data.insert(task);
                } else {
                    task = Data.getTaskById(task_id);
                    if (task == null) {
                        json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Task doesn't exist." });
                        return;
                    }
                }

                task.room_id = room_id;
                task.status = status;
                task.category = category;
                task.notes = notes;

                Data.save();

                json = JsonConvert.SerializeObject(new { status = "OK", id = task.id });
            }
            else {
                json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Request must be POST." });
            }
        }
    }
}