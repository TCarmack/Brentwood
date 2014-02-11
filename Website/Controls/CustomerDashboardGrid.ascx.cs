using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Drawing;
using BrentwoodPrinting.Data;
//Jack
/// <summary>
/// A customer dashboard.
/// </summary>
public partial class Controls_CustomerDashboard : System.Web.UI.UserControl
{
    public string Username { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        FormMessage.Text = "";
        FormMessage.ForeColor = Color.Black;

        if (!Page.IsPostBack)
            ViewState[this.ClientID + "SortDirection"] = "Ascending";

        try
        {
            List<CustomerDashboard> jobs;
            aspnet_Users user = Brentwood.GetUser(Username);

            if (user.CompanyID == null)
                jobs = Brentwood.GetDashboard(Username);
            else
                jobs = Brentwood.ListJobsByCompany((int)user.CompanyID);

            //Filter by which items are selected
            if (TypeFilterCheckbox.Checked)
            {
                JobTypesList.Visible = true;

                List<string> jobTypeNames = new List<string>();

                foreach (ListItem item in JobTypesList.Items)
                    if (item.Selected)
                        jobTypeNames.Add(item.Text);

                List<CustomerDashboard> jobsToDisplay = new List<CustomerDashboard>();

                foreach (CustomerDashboard item in jobs)
                    foreach (string item2 in jobTypeNames)
                        if (item.JobType == item2)
                            jobsToDisplay.Add(item);

                if (jobTypeNames.Count != 0)
                {
                    jobs = jobsToDisplay;
                    JobsGridView.DataSource = jobsToDisplay;
                    JobsGridView.DataBind();
                    ViewState[this.ClientID + "Data"] = jobsToDisplay;
                }
                else
                    BindData();
            }
            else
            {
                JobTypesList.Visible = false;
                BindData();
            }

            //Filter by quantity
            if (QuantityCheckbox.Checked)
            {
                QuantityTextBox.Visible = true;
                QuantityOption.Visible = true;
                List<CustomerDashboard> jobsToDisplay = new List<CustomerDashboard>();
                int quantity = -2000000;

                if (QuantityTextBox.Text.Trim() != "")
                    quantity = int.Parse(QuantityTextBox.Text);

                switch (QuantityOption.SelectedValue)
                {
                    case "GreaterThan":
                        foreach (CustomerDashboard item in jobs)
                            if (item.Quantity > quantity)
                                jobsToDisplay.Add(item);
                        break;

                    case "LessThan":
                        foreach (CustomerDashboard item in jobs)
                            if (item.Quantity < quantity)
                                jobsToDisplay.Add(item);
                        break;
                }

                if (quantity != 2000000)
                {
                    jobs = jobsToDisplay;
                    JobsGridView.DataSource = jobsToDisplay;
                    JobsGridView.DataBind();
                    ViewState[this.ClientID + "Data"] = jobsToDisplay;
                }
            }
            else
            {
                QuantityTextBox.Visible = false;
                QuantityOption.Visible = false;
            }

            //Filter by promise date
            if (PromiseCheckbox.Checked)
            {
                PromiseCalendar.Visible = true;
                PromiseDateOptions.Visible = true;

                List<CustomerDashboard> jobsToDisplay = new List<CustomerDashboard>();
                DateTime time = PromiseCalendar.SelectedDate;

                switch (PromiseDateOptions.SelectedValue)
                { 
                    case "Before":
                        foreach (CustomerDashboard item in jobs)
                            if (DateTime.Compare((DateTime)item.PromiseDate, time) < 0)
                                jobsToDisplay.Add(item);
                        break;

                    case "After":
                        foreach (CustomerDashboard item in jobs)
                            if (DateTime.Compare((DateTime)item.PromiseDate, time) > 0)
                                jobsToDisplay.Add(item);
                        break;
                }

                if (time != (new DateTime(1,1,1,0,0,0)))
                {
                    jobs = jobsToDisplay;
                    JobsGridView.DataSource = jobsToDisplay;
                    JobsGridView.DataBind();
                    ViewState[this.ClientID + "Data"] = jobsToDisplay;
                }
            }
            else
            {
                PromiseCalendar.Visible = false;
                PromiseDateOptions.Visible = false;
                PromiseCalendar.SelectedDate = new DateTime(1,1,1,0,0,0);
            }

            //Filter by start date
            if (StartDateCheckbox.Checked)
            {
                StartCalendar.Visible = true;
                StartDateOptions.Visible = true;

                List<CustomerDashboard> jobsToDisplay = new List<CustomerDashboard>();
                DateTime time = StartCalendar.SelectedDate;

                switch (StartDateOptions.SelectedValue)
                {
                    case "Before":
                        foreach (CustomerDashboard item in jobs)
                            if (DateTime.Compare((DateTime)item.StartDate, time) < 0)
                                jobsToDisplay.Add(item);
                        break;

                    case "After":
                        foreach (CustomerDashboard item in jobs)
                            if (DateTime.Compare((DateTime)item.StartDate, time) > 0)
                                jobsToDisplay.Add(item);
                        break;
                }

                if (time != (new DateTime(1, 1, 1, 0, 0, 0)))
                {
                    jobs = jobsToDisplay;
                    JobsGridView.DataSource = jobsToDisplay;
                    JobsGridView.DataBind();
                    ViewState[this.ClientID + "Data"] = jobsToDisplay;
                }
            }
            else
            {
                StartCalendar.Visible = false;
                StartDateOptions.Visible = false;
                StartCalendar.SelectedDate = new DateTime(1, 1, 1, 0, 0, 0);

            }
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    public void LoadJobs(string username)
    {
        this.Username = username;
        BindData();
    }

    private void BindData()
    {
        try
        {
            aspnet_Users user = Brentwood.GetUser(Username);
            List<CustomerDashboard> data;

            if (user.CompanyID == null)
                data = Brentwood.GetDashboard(Username);
            else
                data = Brentwood.ListJobsByCompany((int)user.CompanyID);

            JobsGridView.DataSource = data;
            JobsGridView.DataBind();
            ViewState[this.ClientID + "Data"] = data;
        }

        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void JobsGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int jobID = int.Parse(JobsGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
        Context.Items.Add("JobID", jobID);
        Server.Transfer("JobPage.aspx");
    }

    protected void JobsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobsGridView.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void JobsGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        List<CustomerDashboard> data = (List<CustomerDashboard>)ViewState[this.ClientID + "Data"];

        switch (e.SortExpression)
        {
            case "JobID":
                if ((string)ViewState[this.ClientID + "SortDirection"] == "Ascending")
                {
                    data = data.OrderByDescending(d => d.JobID).ToList<CustomerDashboard>();
                    ViewState[this.ClientID + "SortDirection"] = "Descending";
                }
                else
                {
                    data = data.OrderBy(d => d.JobID).ToList<CustomerDashboard>();
                    ViewState[this.ClientID + "SortDirection"] = "Ascending";
                }
                break;

            case "JobType":
                if ((string)ViewState[this.ClientID + "SortDirection"] == "Ascending")
                {
                    ViewState[this.ClientID + "SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.JobType).ToList<CustomerDashboard>();
                }
                else
                {
                    ViewState[this.ClientID + "SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.JobType).ToList<CustomerDashboard>();
                }
                break;

            case "Quantity":
                if ((string)ViewState[this.ClientID + "SortDirection"] == "Ascending")
                {
                    ViewState[this.ClientID + "SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.Quantity).ToList<CustomerDashboard>();
                }
                else
                {
                    ViewState[this.ClientID + "SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.Quantity).ToList<CustomerDashboard>();
                }
                break;

            case "PromiseDate":
                if ((string)ViewState[this.ClientID + "SortDirection"] == "Ascending")
                {
                    ViewState[this.ClientID + "SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.PromiseDate).ToList<CustomerDashboard>();
                }
                else
                {
                    ViewState[this.ClientID + "SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.PromiseDate).ToList<CustomerDashboard>();
                }
                break;

            case "DateStarted":
                if ((string)ViewState[this.ClientID + "SortDirection"] == "Ascending")
                {
                    ViewState[this.ClientID + "SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.StartDate).ToList<CustomerDashboard>();
                }
                else
                {
                    ViewState[this.ClientID + "SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.StartDate).ToList<CustomerDashboard>();
                }
                break;
        }

        JobsGridView.DataSource = data;
        JobsGridView.DataBind();
    }
}