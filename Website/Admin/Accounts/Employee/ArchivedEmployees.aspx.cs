using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using BrentwoodPrinting;
using BrentwoodPrinting.Data;
using System.Web.Security;
//Jack
public partial class Admin_Accounts_Employee_ArchivedEmployees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                List<GetEmployees_Result> data = Brentwood.ListArchivedEmployees();
                ArchivedEmployees.DataSource = data;
                ArchivedEmployees.DataBind();

                FormMessage.ForeColor = Color.Black;
                FormMessage.Text = "";
            }
            catch (Exception ex)
            {
                FormMessage.Text = ex.Message;
                FormMessage.ForeColor = Color.Red;
            }
        }
    }

    protected void ArchivedEmployees_SelectedIndexChanged(object sender, EventArgs e)
    {
        Brentwood.UnArchiveEmployee(Guid.Parse(ArchivedEmployees.SelectedDataKey.Value.ToString()));

        List<GetEmployees_Result> data = Brentwood.ListArchivedEmployees();
        ArchivedEmployees.DataSource = data;
        ArchivedEmployees.DataBind();
    }

    protected void ArchivedEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ArchivedEmployees.PageIndex = e.NewPageIndex;

        List<GetEmployees_Result> data = Brentwood.ListArchivedEmployees();
        ArchivedEmployees.DataSource = data;
        ArchivedEmployees.DataBind();
    }
}