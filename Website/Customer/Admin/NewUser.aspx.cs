using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Customer_Admin_NewUser : System.Web.UI.Page
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
            newUser.Approved = true;

            if (AdminCheckbox.Checked)
                Brentwood.MakeCustomerAdmin(Guid.Parse(user.ProviderUserKey.ToString()));

            Brentwood.UpdateCustomer(newUser, EmailAddressTextbox.Text);
            Brentwood.AddUserToCompany(newUser.UserName, (Brentwood.GetCompanyByCustomerId(Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()))).CompanyID);

            FormMessage.Text = "Your account has been successfully created!";
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
}