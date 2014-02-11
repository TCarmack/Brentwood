using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Customer_Admin_CorporateAccount : System.Web.UI.Page
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
        if (!Page.IsPostBack)
            BindData();
    }

    private void BindData()
    {
        int? companyID = Brentwood.LookupCustomerByUsername(User.Identity.Name).CompanyID;

        if (companyID == null)
        {
            FormMessage.Text = "There is no corporate account associate with your account.";
            FormMessage.ForeColor = Color.Red;
        }
        else
        {
            Company item = Brentwood.GetCompany((int)companyID);
            CompanyID.Value = item.CompanyID.ToString();
            NameTextbox.Text = item.Name;
            AddressTextbox.Text = item.CompanyAddress;
            CityTextbox.Text = item.City;
            ProvinceTextbox.Text = item.Province;
            PostalCodeTextbox.Text = item.PostalCode;
            PhoneNumberTextbox.Text = item.PhoneNumber;
            WebsiteTextbox.Text = item.Website;
            Panel1.Visible = true;
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string name, address, city, province, postalcode, phonenumber, website;
        int companyID = int.Parse(CompanyID.Value);
        name = NameTextbox.Text.Trim();
        address = AddressTextbox.Text.Trim();
        city = CityTextbox.Text.Trim();
        province = ProvinceTextbox.Text.Trim();
        postalcode = PostalCodeTextbox.Text.Trim();
        phonenumber = PhoneNumberTextbox.Text.Trim();
        website = WebsiteTextbox.Text.Trim();

        try
        {
            Brentwood.UpdateCompany(companyID, name, address, city, province, 
                postalcode, phonenumber, website);
            FormMessage.Text = "Account successfully updated!";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}