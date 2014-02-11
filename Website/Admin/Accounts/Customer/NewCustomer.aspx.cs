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
public partial class Admin_Accounts_Customer_NewCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.CreateUser(UsernameTextbox.Text, PasswordTextbox.Text, EmailAddressTextbox.Text);

            if (ApprovedCheckbox.Checked)
            {           
                Roles.AddUsersToRole(new string[] { UsernameTextbox.Text }, "Customer");
                Roles.AddUsersToRole(new string[] { UsernameTextbox.Text }, "Approved Customer");
            }
            else
                Roles.AddUsersToRole(new string[] { UsernameTextbox.Text }, "Customer");

            aspnet_Users newUser = new aspnet_Users();
            newUser.UserName = user.UserName;
            newUser.FirstName = FirstNameTextbox.Text;
            newUser.LastName = LastNameTextbox.Text;
            newUser.CustomerAddress = AddressTextbox.Text;
            newUser.City = CityTextbox.Text;
            newUser.Province = ProvinceTextbox.Text;
            newUser.PostalCode = PostalCodeTextbox.Text;
            newUser.PhoneNumber = PhoneNumberTextbox.Text;

            if (CompanyDropdown.SelectedValue != "-1")
                newUser.CompanyID = int.Parse(CompanyDropdown.SelectedValue);

            newUser.Approved = ApprovedCheckbox.Checked;

            Brentwood.UpdateCustomer(newUser, EmailAddressTextbox.Text);

            FormMessage.Text = "Customer account successfully created!";
            FormMessage.ForeColor = Color.Blue;
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

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        UsernameTextbox.Text = "";
        PasswordTextbox.Text = "";
        ConfirmPasswordTextbox.Text = "";
        EmailAddressTextbox.Text = "";
        FirstNameTextbox.Text = "";
        LastNameTextbox.Text = "";
        AddressTextbox.Text = "";
        CityTextbox.Text = "";
        ProvinceTextbox.Text = "";
        PostalCodeTextbox.Text = "";
        PhoneNumberTextbox.Text = "";
        CompanyDropdown.SelectedIndex = 0;
        ApprovedCheckbox.Checked = false;
    }
}