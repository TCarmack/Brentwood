using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;
//Jack
public partial class Controls_DashboardGrid : System.Web.UI.UserControl
{
    public int JobStatusID { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        FormMessage.Text = "";
        FormMessage.ForeColor = Color.Black;

        if (!Page.IsPostBack)
            ViewState["SortDirection"] = "Ascending";

        try
        {
            List<ListByJobStatus_Result> jobs = Brentwood.ListJobsByJobStatus(JobStatusID);

            //Filter by which items are selected
            if (TypeFilterCheckbox.Checked)
            {
                JobTypesList.Visible = true;

                List<string> jobTypeNames = new List<string>();

                foreach (ListItem item in JobTypesList.Items)
                    if (item.Selected)
                        jobTypeNames.Add(item.Text);

                List<ListByJobStatus_Result> jobsToDisplay = new List<ListByJobStatus_Result>();

                foreach (ListByJobStatus_Result item in jobs)
                    foreach (string item2 in jobTypeNames)
                        if (item.Name == item2)
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
                List<ListByJobStatus_Result> jobsToDisplay = new List<ListByJobStatus_Result>();
                int quantity = -2000000;

                if (QuantityTextBox.Text.Trim() != "")
                    quantity = int.Parse(QuantityTextBox.Text);

                switch (QuantityOption.SelectedValue)
                {
                    case "GreaterThan":
                        foreach (ListByJobStatus_Result item in jobs)
                            if (item.Quantity > quantity)
                                jobsToDisplay.Add(item);
                        break;

                    case "LessThan":
                        foreach (ListByJobStatus_Result item in jobs)
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

                List<ListByJobStatus_Result> jobsToDisplay = new List<ListByJobStatus_Result>();
                DateTime time = PromiseCalendar.SelectedDate;

                switch (PromiseDateOptions.SelectedValue)
                { 
                    case "Before":
                        foreach (ListByJobStatus_Result item in jobs)
                            if (DateTime.Compare((DateTime)item.PromiseDate, time) < 0)
                                jobsToDisplay.Add(item);
                        break;

                    case "After":
                        foreach (ListByJobStatus_Result item in jobs)
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

                List<ListByJobStatus_Result> jobsToDisplay = new List<ListByJobStatus_Result>();
                DateTime time = StartCalendar.SelectedDate;

                switch (StartDateOptions.SelectedValue)
                {
                    case "Before":
                        foreach (ListByJobStatus_Result item in jobs)
                            if (DateTime.Compare((DateTime)item.StartDate, time) < 0)
                                jobsToDisplay.Add(item);
                        break;

                    case "After":
                        foreach (ListByJobStatus_Result item in jobs)
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

            if (CustomerCheckbox.Checked)
            {
                CustomerTextbox.Visible = true;
                NameSearchOptions.Visible = true;

                if (CustomerTextbox.Text.Trim() != "")
                {
                    List<ListByJobStatus_Result> jobsToDisiplay = new List<ListByJobStatus_Result>();

                    foreach (ListByJobStatus_Result item in jobs)
                    {
                        switch (NameSearchOptions.SelectedValue)
                        {
                            case "StartsWith":
                                if (item.Customer.StartsWith(CustomerTextbox.Text.Trim()))
                                    jobsToDisiplay.Add(item);
                                break;

                            case "Contains":
                                if (item.Customer.Contains(CustomerTextbox.Text.Trim()))
                                    jobsToDisiplay.Add(item);
                                break;

                            case "EndsWith":
                                if (item.Customer.EndsWith(CustomerTextbox.Text.Trim()))
                                    jobsToDisiplay.Add(item);
                                break;
                        }
                    }

                    jobs = jobsToDisiplay;
                    JobsGridView.DataSource = jobsToDisiplay;
                    JobsGridView.DataBind();
                    ViewState[this.ClientID + "Data"] = jobsToDisiplay;
                }
                else
                    BindData();
            }
            else
            {
                CustomerTextbox.Visible = false;
                NameSearchOptions.Visible = false;
            }
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    public void LoadJobs(int JobStatusID)
    {
        this.JobStatusID = JobStatusID;
        BindData();
    }

    private void BindData()
    {
        try
        {
            List<ListByJobStatus_Result> data = Brentwood.ListJobsByJobStatus(JobStatusID);
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
        JobStatus status = Brentwood.GetJobStatusByJob(jobID);
        Context.Items.Add("JobID", jobID);
        Server.Transfer("Jobs/JobPage.aspx");
    }

    protected void JobsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobsGridView.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void JobsGridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        List<ListByJobStatus_Result> data = (List<ListByJobStatus_Result>)ViewState[this.ClientID + "Data"];

        switch (e.SortExpression)
        {
            case "JobID":
                if ((string)ViewState["SortDirection"] == "Ascending")
                {
                    data = data.OrderByDescending(d => d.JobID).ToList<ListByJobStatus_Result>();
                    ViewState["SortDirection"] = "Descending";
                }
                else
                {
                    data = data.OrderBy(d => d.JobID).ToList<ListByJobStatus_Result>();
                    ViewState["SortDirection"] = "Ascending";
                }
                break;

            case "JobType":
                if ((string)ViewState["SortDirection"] == "Ascending")
                {
                    ViewState["SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.Name).ToList<ListByJobStatus_Result>();
                }
                else
                {
                    ViewState["SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.Name).ToList<ListByJobStatus_Result>();
                }
                break;

            case "Quantity":
                if ((string)ViewState["SortDirection"] == "Ascending")
                {
                    ViewState["SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.Quantity).ToList<ListByJobStatus_Result>();
                }
                else
                {
                    ViewState["SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.Quantity).ToList<ListByJobStatus_Result>();
                }
                break;

            case "Customer":
                if ((string)ViewState["SortDirection"] == "Ascending")
                {
                    ViewState["SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.Customer).ToList<ListByJobStatus_Result>();

                }
                else
                {
                    ViewState["SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.Customer).ToList<ListByJobStatus_Result>();
                }
                break;

            case "PromiseDate":
                if ((string)ViewState["SortDirection"] == "Ascending")
                {
                    ViewState["SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.PromiseDate).ToList<ListByJobStatus_Result>();
                }
                else
                {
                    ViewState["SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.PromiseDate).ToList<ListByJobStatus_Result>();
                }
                break;

            case "DateStarted":
                if ((string)ViewState["SortDirection"] == "Ascending")
                {
                    ViewState["SortDirection"] = "Descending";
                    data = data.OrderBy(d => d.StartDate).ToList<ListByJobStatus_Result>();
                }
                else
                {
                    ViewState["SortDirection"] = "Ascending";
                    data = data.OrderByDescending(d => d.StartDate).ToList<ListByJobStatus_Result>();
                }
                break;
        }

        JobsGridView.DataSource = data;
        JobsGridView.DataBind();
    }
}