using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BrentwoodPrinting.Data;

public partial class Admin_Accounts_Company_NewCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
            Brentwood.AddCompany(name, address, city, province, postalcode, phone, website);
            FormMessage.Text = "Company successfully added.";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}