using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Customer_Admin_ManageUsers : System.Web.UI.Page
{
    //Jack
    protected override void OnPreInit(EventArgs e)
    {
        aspnet_Users current = Brentwood.LookupCustomerByUsername(User.Identity.Name);
        if ((bool)current.Approved)
        {
            int? companyID = current.CompanyID;

            if (companyID == null)
                Response.Redirect("../Error.aspx?ErrorMsg=" + "A corporate account has not been configured for your account", true);
            else
                if (!(bool)current.IsAdmin)
                    Response.Redirect("../Error.aspx?ErrorMsg=" + "You do not have access to this form.", true);
                else
                    if (!(bool)current.CompanyReference.CreateSourceQuery().FirstOrDefault<Company>().Approved)
                        Response.Redirect("../Error.aspx?ErrorMsg=" + "Your corporate account has not yet been approved.", true);
        }
        else
            Response.Redirect("../Error.aspx?ErrorMsg=" + "Your account has not yet been approved. Access to corporate features is restricted.", true);

        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
            BindData();
    }

    private void BindData()
    {
        try
        {
            List<aspnet_Users> data = Brentwood.ListCustomersByCompany((int)Brentwood.LookupCustomerByUsername(User.Identity.Name).CompanyID);
            UsersGridView.DataSource = data;
            UsersGridView.DataBind();
            UsersGridView.Visible = true;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void UsersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Brentwood.ArchiveCustomer(Guid.Parse(UsersGridView.DataKeys[e.NewSelectedIndex].Value.ToString()));
            BindData();
            FormMessage.Text = "User successfully deactivated.";
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Blue;
        }
    }

    protected void UsersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UsersGridView.PageIndex = e.NewPageIndex;
        BindData();
    }
}