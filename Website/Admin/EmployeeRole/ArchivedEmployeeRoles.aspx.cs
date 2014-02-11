using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Admin_EmployeeRole_ArchivedEmployeeRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }

    private void BindData()
    {
        try
        {
            ArchivedEmployeeRoles.DataSource = Brentwood.ListArchivedEmployeeRoles();
            ArchivedEmployeeRoles.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void ArchivedEmployeeRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Brentwood.UnArchiveEmployeeRole(int.Parse(ArchivedEmployeeRoles.DataKeys[e.RowIndex].Value.ToString()));
            Response.Redirect("~/Admin/EmployeeRole/ArchivedEmployeeRoles.aspx");
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }

    }

    protected void ArchivedEmployeeRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ArchivedEmployeeRoles.PageIndex = e.NewPageIndex;
        BindData();
    }
}