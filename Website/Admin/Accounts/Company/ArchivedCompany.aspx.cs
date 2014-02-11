using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Admin_Accounts_Company_ArchivedCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }

    protected void CompaniesGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Brentwood.UnArchiveCompany(int.Parse(CompaniesGridView.DataKeys[e.NewSelectedIndex].Value.ToString()));
            FormMessage.Text = "Company successfully reactivated.";
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
        CompaniesGridView.DataSource = Brentwood.ListArchivedCompanies();
        CompaniesGridView.DataBind();
    }

}