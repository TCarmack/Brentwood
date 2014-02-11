using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BrentwoodPrinting.Data;
using System.Web.Security;
//Jack
public partial class Admin_Accounts_Employee_EmployeePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Context.Items["UserId"] == null)
                Response.Redirect("EmployeeAccounts.aspx");
            else
            {
                try
                {
                    List<GetEmployees_Result> data = Brentwood.ListEmployees();
                    EmployeeView.DataSource = data;
                    EmployeeView.DataBind();
                    Guid userid = Guid.Parse(Context.Items["UserId"].ToString());
                    int itemIndex = data.FindIndex(item => item.UserId == userid);
                    EmployeeView.PageIndex = itemIndex;
                    EmployeeView.DataSource = data;
                    EmployeeView.DataBind();
                }
                catch (Exception ex)
                {
                    FormMessage.Text = ex.Message;
                    FormMessage.ForeColor = Color.Red;
                }
            }

            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = "";
        }

        if (AdminCheckbox.Checked)
            RolesList.Visible = false;
        else
            RolesList.Visible = true;
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        List<GetEmployees_Result> data = Brentwood.ListEmployees();
        Guid userid = Guid.Parse(EmployeeView.DataKey.Value.ToString());
        int itemIndex = data.FindIndex(item => item.UserId == userid);
        EmployeeView.PageIndex = itemIndex;
        EmployeeView.DataSource = data;
        EmployeeView.DataBind();
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            string email = "", lastname = "", firstname = "", username = "";
            username = EmployeeView.Rows[0].Cells[1].Text.Trim();
            email = (EmployeeView.Rows[2].Cells[1].FindControl("EmailTextbox") as TextBox).Text.Trim();
            firstname = (EmployeeView.Rows[3].Cells[1].FindControl("FirstNameTextbox") as TextBox).Text.Trim();
            lastname = (EmployeeView.Rows[4].Cells[1].FindControl("LastNameTextbox") as TextBox).Text.Trim();

            Brentwood.UpdateEmployee(email, firstname, lastname, username);

            if (!String.IsNullOrEmpty((EmployeeView.Rows[6].Cells[1].FindControl("NewPassTextbox") as TextBox).Text.Trim()))
                Membership.GetUser(username).ChangePassword((EmployeeView.Rows[5].Cells[1].FindControl("OldPassTextbox") as TextBox).Text.Trim(), (EmployeeView.Rows[6].Cells[1].FindControl("NewPassTextbox") as TextBox).Text.Trim());

            List<string> roles = new List<string>();

            if (!AdminCheckbox.Checked)
            {
                foreach (ListItem l in RolesList.Items)
                    if (l.Selected)
                        roles.Add(l.Text);

                if (roles.Count > 0)
                {
                    if (Roles.IsUserInRole(username, "Admin"))
                        Roles.RemoveUserFromRole(username, "Admin");

                    if (Roles.IsUserInRole(username, "Employee"))
                        Roles.RemoveUserFromRole(username, "Employee");

                    foreach (string item in roles)
                    {
                        if (Brentwood.IsEmployeeInRole(username, item))
                            Brentwood.RemoveEmployeeFromRoles(Brentwood.GetUser(username).UserId, new List<string>(){ item });
                    }

                    Roles.AddUserToRole(username, "Employee");
                    Brentwood.AddEmployeeToRoles(username, roles);
                }
            }
            else
            {
                if (Roles.IsUserInRole(username, "Admin"))
                    Roles.RemoveUserFromRole(username, "Admin");

                if (Roles.IsUserInRole(username, "Employee"))
                    Roles.RemoveUserFromRole(username, "Employee");

                Roles.AddUserToRole(username, "Admin");
            }

            FormMessage.Text = "Account successfully updated!";
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void ArchiveButton_Click(object sender, EventArgs e)
    {
        try
        {
            string username = EmployeeView.Rows[0].Cells[1].Text.Trim();
            MembershipUser user = Membership.GetUser(username);
            user.IsApproved = false;
            Membership.UpdateUser(user);
            EmployeeView.DataSource = Brentwood.ListEmployees();
            EmployeeView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void EmployeeView_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {
        try
        {
            EmployeeView.PageIndex = e.NewPageIndex;
            EmployeeView.DataSource = Brentwood.ListEmployees();
            EmployeeView.DataBind();

            if (AdminCheckbox.Checked)
                RolesList.Visible = false;
            else
                RolesList.Visible = true;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void EmployeeView_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (Brentwood.IsEmployeeAdmin(Guid.Parse(EmployeeView.DataKey.Value.ToString())))
                AdminCheckbox.Checked = true;
            else
                AdminCheckbox.Checked = false;

            List<EmployeeRole> role = Brentwood.ListEmployeeRoles();
            RolesList.DataSource = role;
            RolesList.DataBind();

            List<string> roles = Brentwood.GetRolesByEmployeeID(Guid.Parse(EmployeeView.DataKey.Value.ToString()));

            foreach (string s in roles)
                foreach (ListItem i in RolesList.Items)
                    if (s == i.Text)
                        i.Selected = true;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void AdminCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        if (AdminCheckbox.Checked)
            RolesList.Visible = false;
        else
            RolesList.Visible = true;
    }
}