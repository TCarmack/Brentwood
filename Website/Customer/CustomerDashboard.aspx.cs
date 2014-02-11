using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
//Jack
public partial class Customer_CustomerDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Invoices.Visible = false;
        Invoices.Enabled = false;

        if (!Page.IsPostBack)
            BindData();
    }

    private void BindData()
    {
        List<GetOutstandingInvoicesByCustomer_Result> data = Brentwood.GetOutstandingInvoicesByCustomer(User.Identity.Name);

        if (data.Count > 0)
        {
            Invoices.Visible = true;
            Invoices.Enabled = true;
        }
        else
        {
            Invoices.Visible = false;
            Invoices.Enabled = false;
        }

        InvoiceView.DataSource = data;
        InvoiceView.DataBind();
    }

    protected void InvoiceView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        InvoiceView.PageIndex = e.NewPageIndex;
        BindData();
    }
}