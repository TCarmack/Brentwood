using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;

public partial class Customer_Approved_Customer_NewOrder : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        if (!(bool)Brentwood.LookupCustomerByUsername(User.Identity.Name).Approved)
            Response.Redirect("../NotApproved.aspx", true);

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Context.Items.Add("JobTypeID", TypesGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
        Server.Transfer("OrderPage.aspx");
    }
}