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
public partial class Customer_AccountPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
               BindData(Brentwood.LookupCustomerByUsername(User.Identity.Name));
            }
            catch (Exception ex)
            {
                FormMessage.Text = ex.Message;
                FormMessage.ForeColor = Color.Red;
            }
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            BindData(Brentwood.LookupCustomerByUsername(User.Identity.Name));
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        aspnet_Users user = new aspnet_Users();
        user.UserName = User.Identity.Name;
        user.FirstName = FirstNameTextbox.Text.Trim();
        user.LastName = LastNameTextbox.Text.Trim();
        user.CustomerAddress = AddressTextbox.Text.Trim();
        user.City = CityTextbox.Text.Trim();
        user.Province = ProvinceTextbox.Text.Trim().ToUpper();
        user.PostalCode = PostalTextbox.Text.Trim().ToUpper();
        user.PhoneNumber = PhoneTextbox.Text.Trim();

        if (NewPassTextbox.Text != "")
        {
            MembershipUser muser = Membership.GetUser(User.Identity.Name);
            muser.ChangePassword(OldPassTextbox.Text, NewPassTextbox.Text);
        }

        try
        { 
            user.Approved = Brentwood.LookupCustomerByUsername(User.Identity.Name).Approved;
            Brentwood.UpdateCustomer(user, EmailTextbox.Text);
            FormMessage.Text = "Account successfully updated!";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    private void BindData(aspnet_Users user)
    {
        UsernameLabel.Text = user.UserName;
        FirstNameTextbox.Text = user.FirstName;
        LastNameTextbox.Text = user.LastName;
        AddressTextbox.Text = user.CustomerAddress;
        CityTextbox.Text = user.City;
        ProvinceTextbox.Text = user.Province;
        PostalTextbox.Text = user.PostalCode;
        PhoneTextbox.Text = user.PhoneNumber;
        EmailTextbox.Text = Membership.GetUser(User.Identity.Name).Email;
    }
}