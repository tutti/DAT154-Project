using DAT154_Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DAT154_Web
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string email = HttpContext.Current.Request["ctl00$MainContent$email"];
                string password1 = HttpContext.Current.Request["ctl00$MainContent$password1"];
                string password2 = HttpContext.Current.Request["ctl00$MainContent$password2"];
                string name = HttpContext.Current.Request["ctl00$MainContent$name"];

                User existing = Data.getUserByEmail(email);

                if (existing != null) {
                    errors.Add("That e-mail is already in use.");
                }
                else if (!password1.Equals(password2)) {
                    errors.Add("The passwords don't match.");
                }
                else if (email.Equals("") || password1.Equals("") || name.Equals("")) {
                    errors.Add("You must enter values into all fields.");
                }

                if (errors.Count() == 0) {
                    User user = new DAT154_Libs.User();
                    user.email = email;
                    user.password = password1;
                    user.name = name;
                    user.type = DAT154_Libs.User.TYPE.GUEST;
                    Data.insert(user);
                    Data.save();
                    Session["user"] = user;
                    Response.Redirect("~/");
                }
            }
            
            if (HttpContext.Current.Request["email"] != null) {
                email.Text = HttpContext.Current.Request["email"];
            }

            if (HttpContext.Current.Request["name"] != null) {
                name.Text = HttpContext.Current.Request["name"];
            }

            errorList.DataSource = errors;
            errorList.DataBind();
        }
    }
}