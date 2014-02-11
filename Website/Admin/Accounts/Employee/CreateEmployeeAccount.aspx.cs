using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BrentwoodPrinting.Data;
//Jack
public partial class Admin_CreateEmployeeAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormMessage.Text = "";
        FormMessage.ForeColor = Color.Black;

        if (!Page.IsPostBack)
        {
        }
    }

    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        FirstNameTextbox.Text = "";
        LastNameTextbox.Text = "";
        EmailTextbox.Text = "";
        UsernameTextbox.Text = "";
        AdminCheckbox.Checked = false;
        EmployeeStatus.SelectedValue = null;
        EmployeeStatus.Enabled = true;
        FormMessage.Text = "";
    }

    protected void ConfirmButton_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.CreateUser(UsernameTextbox.Text, PasswordTextbox.Text, EmailTextbox.Text);
            List<string> roles = new List<string>();

            if (AdminCheckbox.Checked)
                Roles.AddUserToRole(UsernameTextbox.Text, "Admin");
            else
                Roles.AddUserToRole(UsernameTextbox.Text, "Employee");

            Brentwood.UpdateEmployee(user.Email, FirstNameTextbox.Text, LastNameTextbox.Text, user.UserName);

            if (!AdminCheckbox.Checked)
                foreach (ListItem c in EmployeeStatus.Items)
                    if (c.Selected)
                        roles.Add(c.Text);

            if (roles.Count > 0)
                Brentwood.AddEmployeeToRoles(user.UserName, roles);

            FormMessage.Text = "Employee account successfully created!";
        }
        catch (Exception ex)
        {
            if (ex.Message == "The password supplied is invalid.  Passwords must conform to the password strength requirements configured for the default provider.")
                FormMessage.Text = "Password must be at least 6 alphanumeric characters long";
            else
                FormMessage.Text = ex.Message;

            FormMessage.ForeColor = Color.Red;
        }
    }
}