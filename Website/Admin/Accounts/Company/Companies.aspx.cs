using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Admin_Accounts_Company_Companies : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }

    protected void CompaniesGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Context.Items.Add("CompanyID", CompaniesGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
        Server.Transfer("CompanyPage.aspx", false);
    }

    protected void CompaniesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Brentwood.ArchiveCompany(int.Parse(CompaniesGridView.DataKeys[e.RowIndex].Value.ToString()));
            FormMessage.Text = "Company successfully archived.";
            FormMessage.ForeColor = Color.Blue;
            BindData();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void CompaniesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CompaniesGridView.PageIndex = e.NewPageIndex;
        BindData();
    }

    private void BindData()
    {
        CompaniesGridView.DataSource = Brentwood.ListCompanies();
        CompaniesGridView.DataBind();
    }
}