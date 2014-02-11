using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using BrentwoodPrinting.Data;
using BrentwoodPrinting;
using System.Web.Security;
using System.Data;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;

public partial class Employee_ReviewNewOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_ReviewNewOrder.Text = "";
        Label_ReviewNewOrder.ForeColor = Color.Black;
        Label_EstPromiseDate.Text = "";

        // instantiate customerID as all 0's to check against Guid.Empty
        Byte[] bytes = new Byte[16];
        Guid customerID = new Guid(bytes);

        // retrieve CustomerID from context(when not postback) or viewstate(after postback)
        if ((!(Page.IsPostBack)) && (Context.Items["CustomerID"] != null))
        {
            customerID = (Guid)Context.Items["CustomerID"];
            ViewState.Add("thiscustomerid", customerID);
        }
        else if (ViewState["thiscustomerid"] != null)
        {
            customerID = (Guid)ViewState["thiscustomerid"];
        }

        // If the list has been sorted, get from viewstate
        if (ViewState["newjobs"] != null)
        {
            try
            {
                List<JobsByJobStatusID_Result> data = (List<JobsByJobStatusID_Result>)ViewState["newjobs"];
                GV_NewOrder.DataSource = data;
                GV_NewOrder.DataBind();
            }
            catch (Exception ex)
            {
                Label_ReviewNewOrder.Text = ex.ToString();
                Label_ReviewNewOrder.ForeColor = Color.Red;
            }
        }
        else
        {
            // current status ID of 'Pending Approval'
            int newJobStatusID = Brentwood.GetJobStatusByName("Pending Approval").JobStatusID;

            // If the customerid has been set, call By JobStatusID and CustomerID
            if (!(customerID == Guid.Empty))
            {
                try
                {
                    // fill gridview
                    List<JobsByJobStatusIDCustomerID_Result> customerNewJobs = new List<JobsByJobStatusIDCustomerID_Result>();
                    customerNewJobs = Brentwood.ListJobsByJobStatusIDCustomerID(newJobStatusID, customerID);
                    ViewState.Add("newjobs", customerNewJobs);
                    GV_NewOrder.DataSource = customerNewJobs;
                    GV_NewOrder.DataBind();
                }
                catch (Exception ex)
                {
                    Label_ReviewNewOrder.Text = ex.ToString();
                    Label_ReviewNewOrder.Text = "Job status ID does not exist.  Please ensure the page is correctly configured with the 'New Job' ID.";
                    Label_ReviewNewOrder.ForeColor = Color.Red;
                }
                Label_ReviewNewOrder.Text = customerID.ToString();
            }

            // Load all jobs if no customerID is set.
            else
            {
                try
                {
                    List<JobsByJobStatusID_Result> newJobs = new List<JobsByJobStatusID_Result>();
                    newJobs = Brentwood.ListJobsByJobStatusID(newJobStatusID);
                    ViewState.Add("newjobs", newJobs);
                    GV_NewOrder.DataSource = newJobs;
                    GV_NewOrder.DataBind();
                }
                catch (Exception ex)
                {
                    Label_ReviewNewOrder.Text = ex.ToString();
                    Label_ReviewNewOrder.Text = "Job status ID does not exist.  Please ensure the page is correctly configured with the 'New Job' ID.";
                    Label_ReviewNewOrder.ForeColor = Color.Red;
                }
            }
        }
    }

    protected void GV_JobAssets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_JobAssets.PageIndex = e.NewPageIndex;
        RefreshAssets();
    }

    protected void RefreshAssets()
    {
        GridViewRow gvrow = GV_NewOrder.SelectedRow;
        int jobID = int.Parse(gvrow.Cells[1].Text.Trim());
        List<JobAsset> jobAssets = new List<JobAsset>();
        jobAssets = Brentwood.ListJobAssetsByJobID(jobID);
        GV_JobAssets.DataSource = jobAssets;
        GV_JobAssets.DataBind();
    }

    protected void GV_NewOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_NewOrder.PageIndex = e.NewPageIndex;
        Page_Load(sender, e);
    }

    protected void GV_NewOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = GV_NewOrder.SelectedRow;
            int jobID = int.Parse(gvrow.Cells[1].Text.Trim());
            Job thisjob = Brentwood.GetJob(jobID);
            int thisjobtypeid = thisjob.JobTypeID;
            DateTime estcompletiondate = Utils.GetPromiseDate(thisjobtypeid);

            // Load JobID specific data containers
            List<JobDetailsByJobID_Result> jobDetails = new List<JobDetailsByJobID_Result>();
            jobDetails = Brentwood.GetJobDetailsByJobID(jobID);
            DV_NewOrder.DataSource = jobDetails;
            DV_NewOrder.DataBind();
            RefreshAssets();
            Label_EstPromiseDate.Text = "Estimated Completion Date: " + estcompletiondate.ToString("dd/MMM/yyyy");

            List<JobInfo> jobInfo = new List<JobInfo>();
            jobInfo = Brentwood.ListJobInfoByJob(jobID);
            GV_info.DataSource = jobInfo;
            GV_info.DataBind();
        }

        catch (Exception ex)
        {
            Label_ReviewNewOrder.Text = ex.ToString();
            Label_ReviewNewOrder.ForeColor = Color.Red;
        }
    }

    protected SortDirection GetSortDirection(string column)
    {
        SortDirection nextDir = SortDirection.Ascending; // Default next sort expression behaviour.
        if (ViewState["sort"] != null && ViewState["sort"].ToString() == column)
        {   // Exists... DESC.
            nextDir = SortDirection.Descending;
            ViewState["sort"] = null;
        }
        else
        {   // Doesn't exists, set ViewState.
            ViewState["sort"] = column;
        }
        return nextDir;
    }

    protected void RequestInfo_Button_Click(object sender, EventArgs e)
    {
        bool valid = true;

        if (DV_NewOrder.Rows.Count == 0)
        {
            valid = false;
            Label_ReviewNewOrder.Text = "Please select a Job to request more info for before submitting.";
            Label_ReviewNewOrder.ForeColor = Color.Red;
        }

        if (MoreInfoTextbox.Text.Trim() == "")
        {
            valid = false;
            Label_ReviewNewOrder.Text = "Please enter a message to inform the customer of what info is required.";
            Label_ReviewNewOrder.ForeColor = Color.Red;
        }

        if (valid)
        {
            try
            {
                var document = XDocument.Load(Server.MapPath("../Admin/Settings.xml"));

                // values from appSettings
                string fromAddress = document.Element("appSettings").Element("fromAddress").Value;
                string smtpPassword = document.Element("appSettings").Element("smtpPassword").Value;
                string smtpClientAddress = document.Element("appSettings").Element("smtpClient").Value;
                int smtpPort = int.Parse(document.Element("appSettings").Element("smtpPort").Value);
                string subjectText = document.Element("appSettings").Element("moreInfoSubject").Value;
                string bodyStart = document.Element("appSettings").Element("moreInfoBodyStart").Value;
                string bodyEnd = document.Element("appSettings").Element("moreInfoBodyEnd").Value;

                // Values from form
                int jobID = int.Parse(DV_NewOrder.Rows[0].Cells[1].Text.Trim());
                string toAddress = DV_NewOrder.Rows[12].Cells[1].Text.Trim();
                string jobName = DV_NewOrder.Rows[1].Cells[1].Text.Trim();
                string subject = subjectText.Replace("[Job ID]", jobID.ToString()).Replace("[Job Name]", jobName);
                string mainBody = MoreInfoTextbox.Text.Trim();
                string body = bodyStart + mainBody + bodyEnd;

                // Send email
                SmtpClient smtpclient = new SmtpClient(smtpClientAddress, smtpPort);
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                smtpclient.Send(message);
                clearData();

                Label_ReviewNewOrder.Text = "Customer has been Emailed.";
                Label_ReviewNewOrder.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                Label_ReviewNewOrder.Text = ex.ToString();
                Label_ReviewNewOrder.ForeColor = Color.Red;
            }
        }
    }
    protected void SubmitQuote_Button_Click(object sender, EventArgs e)
    {
        bool valid = true;
        decimal quoteAmount;
        string testQuote = QuoteTextbox.Text.Trim();

        if (!(decimal.TryParse(testQuote, out quoteAmount)))
        {
            valid = false;
            Label_ReviewNewOrder.Text = "Quote must be a valid dollar amount.";
            Label_ReviewNewOrder.ForeColor = Color.Red;
        }

        if (valid)
        {
            if ((decimal.Parse(testQuote) < 0))
            {
                valid = false;
                Label_ReviewNewOrder.Text = "Quote amount must be a positive number.";
                Label_ReviewNewOrder.ForeColor = Color.Red;
            }
        }

        if (string.IsNullOrEmpty(testQuote))
        {
            valid = false;
            Label_ReviewNewOrder.Text = "Please enter the job quote amount before submitting.";
            Label_ReviewNewOrder.ForeColor = Color.Red;
        }

        if (DV_NewOrder.Rows.Count == 0)
        {
            valid = false;
            Label_ReviewNewOrder.Text = "Please select a job to quote before submitting.";
            Label_ReviewNewOrder.ForeColor = Color.Red;
        }

        if (valid)
        {
            try
            {
                var document = XDocument.Load(Server.MapPath("../Admin/Settings.xml"));

                // values from appSettings
                string fromAddress = document.Element("appSettings").Element("fromAddress").Value;
                string smtpPassword = document.Element("appSettings").Element("smtpPassword").Value;
                string smtpClientAddress = document.Element("appSettings").Element("smtpClient").Value;
                int smtpPort = int.Parse(document.Element("appSettings").Element("smtpPort").Value);
                string subjectText = document.Element("appSettings").Element("quoteSubject").Value;
                string body = document.Element("appSettings").Element("quoteBody").Value;

                // Values from form
                int jobID = int.Parse(DV_NewOrder.Rows[0].Cells[1].Text.Trim());
                string toAddress = DV_NewOrder.Rows[12].Cells[1].Text.Trim();
                string jobName = DV_NewOrder.Rows[1].Cells[1].Text.Trim();
                string subject = String.Format(subjectText, jobID, jobName);
                string mainBody = MoreInfoTextbox.Text.Trim();
                quoteAmount = decimal.Parse(QuoteTextbox.Text.Trim());

                // Add Quote to DB
                Brentwood.Job_AddQuote(jobID, quoteAmount);

                // Update Job Status
                int quoteApprovalStatusID = Brentwood.GetJobStatusByName("Pending Quote Approval").JobStatusID;
                Guid employeeid = (Guid)Membership.GetUser().ProviderUserKey;
                Brentwood.ChangeJobJobStatus(quoteApprovalStatusID, jobID, employeeid);

                // Send email
                SmtpClient smtpclient = new SmtpClient(smtpClientAddress, smtpPort);
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                smtpclient.Send(message);
                clearData();

                Label_ReviewNewOrder.Text = "Quote submitted.  The customer has been Emailed.";
                Label_ReviewNewOrder.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                Label_ReviewNewOrder.Text = ex.ToString();
                Label_ReviewNewOrder.ForeColor = Color.Red;
            }
        }
    }

    protected void Button_CustomerSearch_Click(object sender, EventArgs e)
    {
        int newJobStatusID = Brentwood.GetJobStatusByName("Pending Approval").JobStatusID;
        // get all if no search value
        if ((CustomerSearch.Text.Trim()) == "")
        {
            List<JobsByJobStatusID_Result> newJobs = new List<JobsByJobStatusID_Result>();
            newJobs = Brentwood.ListJobsByJobStatusID(newJobStatusID);
            ViewState.Add("newjobs", newJobs);
            GV_NewOrder.DataSource = newJobs;
            GV_NewOrder.DataBind();
        }
        else
        {
            try
            {
                // current status ID of 'In Design'
                string nameSearch = CustomerSearch.Text.Trim();

                // fill gridview
                List<JobsByJobStatusID_Result> customerSearchInDesign = new List<JobsByJobStatusID_Result>();
                customerSearchInDesign = Brentwood.ListJobsByJobStatusIDCustomerName(newJobStatusID, nameSearch);

                if (customerSearchInDesign.Count == 0)
                {
                    Label_ReviewNewOrder.Text = "No 'Pending Approval' jobs were found for that search value.";
                    Label_ReviewNewOrder.ForeColor = Color.Red;
                }
                else
                {
                    ViewState.Add("newjobs", customerSearchInDesign);
                    GV_NewOrder.DataSource = customerSearchInDesign;
                    GV_NewOrder.DataBind();
                }
            }
            catch (Exception ex)
            {
                Label_ReviewNewOrder.Text = ex.ToString();
                Label_ReviewNewOrder.ForeColor = Color.Red;
            }
        }
    }

    protected void Button_CompanySearch_Click(object sender, EventArgs e)
    {
        int newJobStatusID = Brentwood.GetJobStatusByName("Pending Approval").JobStatusID;
        // get all if no search value
        if ((CompanySearch.Text.Trim()) == "")
        {
            List<JobsByJobStatusID_Result> newJobs = new List<JobsByJobStatusID_Result>();
            newJobs = Brentwood.ListJobsByJobStatusID(newJobStatusID);
            ViewState.Add("newjobs", newJobs);
            GV_NewOrder.DataSource = newJobs;
            GV_NewOrder.DataBind();
        }
        else
        {
            try
            {
                // current status ID of 'In Design'
                string nameSearch = CompanySearch.Text.Trim();

                // fill gridview
                List<JobsByJobStatusID_Result> companySearchInDesign = new List<JobsByJobStatusID_Result>();
                companySearchInDesign = Brentwood.ListJobsByJobStatusIDCompanyName(newJobStatusID, nameSearch);

                if (companySearchInDesign.Count() == 0)
                {
                    Label_ReviewNewOrder.Text = "No 'Pending Approval' jobs were found for that search value.";
                    Label_ReviewNewOrder.ForeColor = Color.Red;
                }
                else
                {
                    ViewState.Add("newjobs", companySearchInDesign);
                    GV_NewOrder.DataSource = companySearchInDesign;
                    GV_NewOrder.DataBind();
                }
            }
            catch (Exception ex)
            {
                Label_ReviewNewOrder.Text = ex.ToString();
                Label_ReviewNewOrder.ForeColor = Color.Red;
            }
        }
    }
    protected void GV_NewOrder_Sorting(object sender, GridViewSortEventArgs e)
    {
        List<JobsByJobStatusID_Result> data = (List<JobsByJobStatusID_Result>)ViewState["newjobs"];
        #region sortingExpressions
        switch (e.SortExpression)
        {
            case "JobName":
                SortDirection jndirection = GetSortDirection("JobName");
                if (jndirection == SortDirection.Ascending)
                {
                    data.Sort((x, y) => string.Compare(x.JobName, y.JobName));
                }
                else
                {
                    data.Sort((x, y) => string.Compare(y.JobName, x.JobName));
                }
                break;

            case "CompanyName":
                SortDirection cndirection = GetSortDirection("CompanyName");
                if (cndirection == SortDirection.Ascending)
                {
                    data.Sort((x, y) => string.Compare(x.CompanyName, y.CompanyName));
                }
                else
                {
                    data.Sort((x, y) => string.Compare(y.CompanyName, x.CompanyName));
                }
                break;

            case "CustomerFirstName":
                SortDirection cfndirection = GetSortDirection("CustomerFirstName");
                if (cfndirection == SortDirection.Ascending)
                {
                    data.Sort((x, y) => string.Compare(x.CustomerFirstName, y.CustomerFirstName));
                }
                else
                {
                    data.Sort((x, y) => string.Compare(y.CustomerFirstName, x.CustomerFirstName));
                }
                break;

            case "CustomerLastName":
                SortDirection clndirection = GetSortDirection("CustomerLastName");
                if (clndirection == SortDirection.Ascending)
                {
                    data.Sort((x, y) => string.Compare(x.CustomerLastName, y.CustomerLastName));
                }
                else
                {
                    data.Sort((x, y) => string.Compare(y.CustomerLastName, x.CustomerLastName));

                }
                break;
        }
        #endregion
        ViewState.Add("newjobs", data);
        GV_NewOrder.DataSource = data;
        GV_NewOrder.DataBind();
    }

    protected void clearData()
    {
        GV_NewOrder.DataSource = null;
        GV_NewOrder.DataBind();

        DV_NewOrder.DataSource = null;
        DV_NewOrder.DataBind();

        GV_info.DataSource = null;
        GV_info.DataBind();

        GV_JobAssets.DataSource = null;
        GV_JobAssets.DataBind();
    }
}