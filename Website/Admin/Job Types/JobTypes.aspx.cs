using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using BrentwoodPrinting.CookieClasses.JobType;
//Jack
public partial class Admin_JobTypes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FormMessage.ForeColor = System.Drawing.Color.Black;
            FormMessage.Text = "";
        }

        try
        {
            List<JobType> data = Brentwood.ListJobTypes();        
            JobTypesList.DataSource = data;
            JobTypesList.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = System.Drawing.Color.Red;
            FormMessage.Text = ex.Message;
        }

    }

    protected void JobTypesList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Context.Items.Add("JobTypeID", JobTypesList.DataKeys[e.NewSelectedIndex].Value);
        Server.Transfer("JobTypePage.aspx", false);
    }

    protected void JobTypesList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = int.Parse(JobTypesList.DataKeys[e.RowIndex].Value.ToString());
        Brentwood.ArchiveJobType(index);
        try
        {
            List<JobType> data = Brentwood.ListJobTypes();
            JobTypesList.DataSource = data;
            JobTypesList.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = System.Drawing.Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void JobTypesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobTypesList.PageIndex = e.NewPageIndex;
        try
        {
            List<JobType> data = Brentwood.ListJobTypes();
            JobTypesList.DataSource = data;
            JobTypesList.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = System.Drawing.Color.Red;
            FormMessage.Text = ex.Message;
        }
    }
}