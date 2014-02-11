using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BrentwoodPrinting.Data;
using System.Drawing;
//Jack
public partial class Account_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            MembershipUser user = Membership.CreateUser(UsernameTextbox.Text, PasswordTextbox.Text, EmailAddressTextbox.Text);

            aspnet_Users newUser = new aspnet_Users();
            newUser.UserName = user.UserName;
            newUser.FirstName = FirstNameTextbox.Text;
            newUser.LastName = LastNameTextbox.Text;
            newUser.CustomerAddress = AddressTextbox.Text;
            newUser.City = CityTextbox.Text;
            newUser.Province = ProvinceTextbox.Text;
            newUser.PostalCode = PostalCodeTextbox.Text;
            newUser.PhoneNumber = PhoneNumberTextbox.Text;

            newUser.Approved = false;

            Brentwood.UpdateCustomer(newUser, EmailAddressTextbox.Text);

            FormMessage.Text = "Your account has been successfully created! You may now login to review or modify your information.";
            FormMessage.ForeColor = Color.Blue;

            Roles.AddUserToRole(newUser.UserName, "Customer");
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
    }
}