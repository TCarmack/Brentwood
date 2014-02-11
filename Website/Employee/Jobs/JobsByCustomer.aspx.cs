using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Web.Security;
using System.Drawing;

public partial class Employee_Jobs_JobsByCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }

    private void BindData()
    {
        Customers.DataSource = Brentwood.ListCustomers();
        Customers.DataBind();

        if (Session["CustomerID"] != null && Session["CustomerID"].ToString() != "-1")
        {
            Guid customerID = Guid.Parse(Session["CustomerID"].ToString());

            Jobs.DataSource = Brentwood.ListJobsByCustomer(customerID);
            Jobs.DataBind();
        }
    }

    protected void Customers_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Session["CustomerID"] = Guid.Parse(Customers.DataKeys[e.NewSelectedIndex].Value.ToString());
        BindData();
    }

    protected void Customers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Customers.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void Jobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Jobs.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void Jobs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Job item = Brentwood.GetJob(int.Parse(Jobs.DataKeys[e.NewSelectedIndex].Value.ToString()));
            int jobID = Brentwood.AddJob(item.JobTypeID, item.SpecialInstructions, item.Quantity, item.DeliveryOrPickup, item.CustomerID, (DateTime)item.PromiseDate);
            Brentwood.InitializeJobJobStatus(jobID, Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()));
            Brentwood.AddInfoToJob(item.JobInfoes.ToList<JobInfo>(), jobID);
            List<string> files = new List<string>();
            foreach (JobAsset item2 in item.JobAssets.ToList<JobAsset>())
                files.Add(item2.Filepath);
            Brentwood.AddAssetsToJob(files, jobID);
            FormMessage.Text = "Job reordered!";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}