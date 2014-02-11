using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Jack
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Page.User.Identity.IsAuthenticated)
        {
            if (Page.User.IsInRole("Admin"))
                Response.Redirect("Admin/AdminDashboard.aspx");
            else if (Page.User.IsInRole("Employee"))
                Response.Redirect("Employee/EmployeeDashboard.aspx");
            else if (Page.User.IsInRole("Customer") || Page.User.IsInRole("Approved Customer"))
                Response.Redirect("Customer/CustomerDashboard.aspx");
        }
        else
            Response.Redirect("Account/Login.aspx");
    }
}