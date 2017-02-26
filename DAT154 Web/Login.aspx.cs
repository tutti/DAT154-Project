using DAT154_Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DAT154_Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string email = HttpContext.Current.Request["ctl00$MainContent$email"];
                string password = HttpContext.Current.Request["ctl00$MainContent$password"];

                User user = Data.getUserByEmail(email);

                if (user == null) {
                    errors.Add("Could not find user.");
                } else if (!user.verifyPassword(password)) {
                    errors.Add("Wrong password.");
                }

                if (errors.Count() == 0) {
                    Session["user"] = user;

                    if (Session["reserved_room_id"] != null) {
                        Response.Redirect("/SearchResults");
                    } else {
                        Response.Redirect("/");
                    }
                }
            }

            if (HttpContext.Current.Request["email"] != null) {
                email.Text = HttpContext.Current.Request["email"];
            }

            errorList.DataSource = errors;
            errorList.DataBind();
        }
    }
}