using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;
//Jack
public partial class Admin_JobStatus_ArchivedJobStatuses : System.Web.UI.Page
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
            JobStatusesGridView.DataSource = Brentwood.ListArchivedJobStatuses();
            JobStatusesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void JobStatusesGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Brentwood.UnArchiveJobStatus(int.Parse(JobStatusesGridView.DataKeys[e.NewSelectedIndex].Value.ToString()));
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }

        BindData();
    }

    protected void JobStatusesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobStatusesGridView.PageIndex = e.NewPageIndex;
        BindData();
    }
}