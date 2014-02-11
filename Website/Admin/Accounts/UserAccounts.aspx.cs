using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BrentwoodPrinting.Data;
//Jack
public partial class Admin_Accounts_UserAccounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormMessage.Text = "";
        FormMessage.ForeColor = Color.Black;

        try
        {
            UsersView.DataSource = Brentwood.ListAllUsers();
            UsersView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void UsersView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        UsersView.PageIndex = e.NewPageIndex;
        try
        {
            UsersView.DataSource = Brentwood.ListAllUsers();
            UsersView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void UsersView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid userid = Guid.Parse(UsersView.DataKeys[e.NewSelectedIndex].Value.ToString());
        string role = UsersView.Rows[e.NewSelectedIndex].Cells[5].Text.Trim();
        Context.Items.Add("UserId", userid);

        if (role == "Admin" || role == "Employee")
            Server.Transfer("Employee/EmployeePage.aspx");
        else if (role == "Approved Customer" || role == "Customer")
            Server.Transfer("Customer/CustomerPage.aspx");
    }
}