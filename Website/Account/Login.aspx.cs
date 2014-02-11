using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
//Jack
public partial class Account_Login : System.Web.UI.Page
{
    public struct aspnetUser
    {
        string name;
        string pass;

        public aspnetUser(string uname, string upass)
        {
            name = uname;
            pass = upass;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }
}
