using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;
//Jack
public partial class Admin_Accounts_Company_CompanyPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Context.Items["CompanyID"] == null)
                Response.Redirect("Companies.aspx", true);
            else
                Session["CompanyID"] = Context.Items["CompanyID"];

            BindData();
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        BindData();
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
            if (Approved.Checked)
                Brentwood.ApproveCompany(int.Parse(Session["CompanyID"].ToString()));
            else
                Brentwood.DisapproveCompany(int.Parse(Session["CompanyID"].ToString()));

            Brentwood.UpdateCompany(int.Parse(Session["CompanyID"].ToString()), name, address, city, province, postalcode, phone, website);
            FormMessage.Text = "Company successfully updated.";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    private void BindData()
    {
        Company item = Brentwood.GetCompany(int.Parse(Session["CompanyID"].ToString()));
        NameTextbox.Text = item.Name;
        AddressTextbox.Text = item.CompanyAddress;
        CityTextbox.Text = item.City;
        ProvinceTextbox.Text = item.Province;
        PostalCodeTextbox.Text = item.PostalCode;
        PhoneNumberTextbox.Text = item.PhoneNumber;
        WebsiteTextbox.Text = item.Website;
        Approved.Checked = (bool)item.Approved;
    }
}