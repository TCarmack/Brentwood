using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Drawing;
using BrentwoodPrinting.Data;
//Jack
public partial class Admin_Accounts_Employee_EmployeeAccounts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FormMessage.Text = "";
            FormMessage.ForeColor = Color.Black;

            try
            {
                List<GetEmployees_Result> data = Brentwood.ListEmployees();
                EmployeesGridView.DataSource = data;
                EmployeesGridView.DataBind();
            }
            catch (Exception ex)
            {
                FormMessage.ForeColor = Color.Red;
                FormMessage.Text = ex.Message;
            }
        }
    }

    protected void EmployeesGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Guid id = Guid.Parse(EmployeesGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
        Context.Items.Add("UserId", id);
        Server.Transfer("EmployeePage.aspx");
    }

    protected void EmployeesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string username = EmployeesGridView.Rows[e.RowIndex].Cells[2].Text.Trim();
            MembershipUser user = Membership.GetUser(username);
            user.IsApproved = false;
            Membership.UpdateUser(user);
            EmployeesGridView.DataSource = Brentwood.ListEmployees();
            EmployeesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void EmployeesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            EmployeesGridView.PageIndex = e.NewPageIndex;
            EmployeesGridView.DataSource = Brentwood.ListEmployees();
            EmployeesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}