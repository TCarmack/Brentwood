using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BrentwoodPrinting.Data;
using System.Web.Security;

public partial class Customer_Admin_ManageUsers : System.Web.UI.Page
{
    //Jack
    protected void Page_Load(object sender, EventArgs e)
    {
        aspnet_Users current = Brentwood.LookupCustomerByUsername(User.Identity.Name);
        int? companyID = current.CompanyID;

        if (companyID != null)
        {
            ControlsPanel.Visible = false;
            FormMessage.Text = "A corporate account has already been configured for your account";
            FormMessage.ForeColor = Color.Red;
        }
        else
            ControlsPanel.Visible = true;
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string name = NameTextbox.Text.Trim();
        string address = AddressTextbox.Text.Trim();
        string city = CityTextbox.Text.Trim();
        string province = ProvinceTextbox.Text.Trim();
        string postalcode = PostalCodeTextbox.Text.Trim();
        string phone = PhoneNumberTextbox.Text.Trim();
        string website = WebsiteTextbox.Text.Trim();

        try
        {
            int companyID = Brentwood.AddCompany(name, address, city, province, postalcode, phone, website);
            Brentwood.AddUserToCompany(User.Identity.Name, companyID);
            Brentwood.MakeCustomerAdmin(User.Identity.Name);

            FormMessage.Text = "Company successfully created.";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}