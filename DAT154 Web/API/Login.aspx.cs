using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAT154_Libs;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace DAT154_Web.API {
    public partial class Login : System.Web.UI.Page {
        protected string json = "";

        protected void Page_Load(object sender, EventArgs e) {
            if (HttpContext.Current.Request.HttpMethod == "POST") {
                string email = HttpContext.Current.Request["email"];
                string password = HttpContext.Current.Request["password"];

                User user = Data.getUserByEmail(email);

                if (user == null) {
                    json = JsonConvert.SerializeObject(new { status = "ERROR", error = "User not found." });
                    return;
                }

                if (!user.verifyPassword(password)) {
                    json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Wrong password." });
                    return;
                }

                /*string token;

                using (RandomNumberGenerator rng = new RNGCryptoServiceProvider()) {
                    byte[] tokenData = new byte[32];
                    rng.GetBytes(tokenData);

                    token = Convert.ToBase64String(tokenData);
                }*/

                json = JsonConvert.SerializeObject(new { status = "OK", user = user });

                //Application["api:token-" + token] = user;
            } else {
                json = JsonConvert.SerializeObject(new { status = "ERROR", error = "Request must be POST." });
            }
        }
    }
}