using DAT154_Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string email = HttpContext.Current.Request["ctl00$MainContent$email"];
                string password = HttpContext.Current.Request["ctl00$MainContent$password"];

                User user = Data.getUserByEmail(email);

                /*
                 * THIS DOES NOT CHECK PASSWORD
                 * I AM TRYING TO FIGURE SOMETHING OUT AND WILL ADD IT BUT
                 * AM WRITING THIS IN CASE I FORGET
                 */

                Session["user"] = user;

                Response.Redirect("~/");
            }
        }
    }
}