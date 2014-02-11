using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
//Jack
public partial class Admin_ArchivedJobTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = "";
        }

        try
        {
            List<JobType> data = Brentwood.ListArchivedJobTypes();
            JobTypesGridView.DataSource = data;
            JobTypesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void JobTypesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobTypesGridView.PageIndex = e.NewPageIndex;
        try
        {
            List<JobType> data = Brentwood.ListArchivedJobTypes();
            JobTypesGridView.DataSource = data;
            JobTypesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void JobTypesGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int index = int.Parse(JobTypesGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
        try
        {
            Brentwood.UnArchiveJobType(index);
            List<JobType> data = Brentwood.ListArchivedJobTypes();
            JobTypesGridView.DataSource = data;
            JobTypesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }
}