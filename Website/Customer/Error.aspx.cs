using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Customer_Error : System.Web.UI.Page
{
    public string ErrorMsg { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ErrorMsg"] == null)
            Response.Redirect("CustomerDashboard.aspx");
        else
            ErrorMsg = Request.QueryString["ErrorMsg"].ToString();

        ErrorLabel.Text = ErrorMsg;
        ErrorLabel.ForeColor = System.Drawing.Color.Red;
    }
}