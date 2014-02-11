using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Configuration;
using BrentwoodPrinting.Data;
using BrentwoodPrinting;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Xml.XPath;
using System.Xml.Linq;


public partial class Employee_SubmitProof : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label_SubmitProof.Text = "";
        Label_SubmitProof.ForeColor = Color.Black;

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
        if (ViewState["jobsindesign"] != null)
        {
            try
            {
                List<JobsByJobStatusID_Result> data = (List<JobsByJobStatusID_Result>)ViewState["jobsindesign"];
                GV_InDesign.DataSource = data;
                GV_InDesign.DataBind();
            }
            catch (Exception ex)
            {
                Label_SubmitProof.Text = ex.ToString();
                Label_SubmitProof.ForeColor = Color.Red;
            }
        }
        else
        {
            if (!(customerID == Guid.Empty))
            {
                try
                {
                    // current status ID of 'In Design'
                    int inDesignStatusID = Brentwood.GetJobStatusByName("In Design").JobStatusID;

                    // fill gridview
                    List<JobsByJobStatusIDCustomerID_Result> customerJobsInDesign = new List<JobsByJobStatusIDCustomerID_Result>();
                    customerJobsInDesign = Brentwood.ListJobsByJobStatusIDCustomerID(inDesignStatusID, customerID);
                    ViewState.Add("jobsindesign", customerJobsInDesign);
                    GV_InDesign.DataSource = customerJobsInDesign;
                    GV_InDesign.DataBind();
                }
                catch (Exception ex)
                {
                    Label_SubmitProof.Text = ex.Message;
                    Label_SubmitProof.ForeColor = Color.Red;
                }
            }

            // Load all jobs if no customerID is set.
            else
            {
                try
                {
                    // current status ID of 'In Design'
                    int inDesignStatusID = Brentwood.GetJobStatusByName("In Design").JobStatusID;

                    List<JobsByJobStatusID_Result> jobsInDesign = new List<JobsByJobStatusID_Result>();
                    jobsInDesign = Brentwood.ListJobsByJobStatusID(inDesignStatusID);
                    ViewState.Add("jobsindesign", jobsInDesign);
                    GV_InDesign.DataSource = jobsInDesign;
                    GV_InDesign.DataBind();
                }
                catch (Exception ex)
                {
                    Label_SubmitProof.Text = ex.Message;
                    Label_SubmitProof.ForeColor = Color.Red;
                }
            }
        }
    }

    protected void SubmitProof_Click(object sender, EventArgs e)
    {
        bool valid = true;

        if ((ProofFileUpload.PostedFile.ContentLength <= 0))
        {
            valid = false;
            Label_SubmitProof.Text = "File contains no data.  Please choose another file.";
            Label_SubmitProof.ForeColor = Color.Red;
        }

        if (!(ProofFileUpload.HasFile))
        {
            valid = false;
            Label_SubmitProof.Text = "Please upload a job-proof file before submitting.";
            Label_SubmitProof.ForeColor = Color.Red;
        }

        if (DV_InDesign.Rows.Count == 0)
        {
            valid = false;
            Label_SubmitProof.Text = "Please select a job to proof before submitting.";
            Label_SubmitProof.ForeColor = Color.Red;
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
                string subjectText = document.Element("appSettings").Element("submitProofSubject").Value;
                string body = document.Element("appSettings").Element("submitProofBody").Value;

                // Values from form
                int jobID = int.Parse(DV_InDesign.Rows[0].Cells[1].Text.Trim());
                string toAddress = DV_InDesign.Rows[12].Cells[1].Text.Trim();
                string jobName = DV_InDesign.Rows[1].Cells[1].Text.Trim();
                string subject = String.Format(subjectText, jobID, jobName);

                // Insert Job Proof
                int newProof = -1;
                newProof = SaveFile(ProofFileUpload.PostedFile);
                RefreshProofs();

                // success message
                if (newProof != -1)
                {
                    // Update JobJobStatus
                    Guid employeeid = (Guid)Membership.GetUser().ProviderUserKey;
                    int statusID = Brentwood.GetJobStatusByName("In Design").JobStatusID;
                    Brentwood.UpdateJobJobStatus(jobID, employeeid, statusID);

                    RefreshProofsClean();
                    Label_SubmitProof.Text = "File upload completed. Upload another file or notify the customer.";
                    Label_SubmitProof.ForeColor = Color.DarkGreen;
                }
                else
                {
                    Label_SubmitProof.Text = "A database error has occurred. Job proof was not created in the database.";
                    Label_SubmitProof.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                Label_SubmitProof.Text = ex.ToString();
                Label_SubmitProof.ForeColor = Color.Red;
            }
        }
    }

    protected void GV_InDesign_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_InDesign.PageIndex = e.NewPageIndex;
        Page_Load(sender, e);
    }

    protected void GV_InDesign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrow = GV_InDesign.SelectedRow;
            int jobID = int.Parse(gvrow.Cells[1].Text.Trim());

            // Load JobID specific data containers
            List<JobDetailsByJobID_Result> jobDetails = new List<JobDetailsByJobID_Result>();
            jobDetails = Brentwood.GetJobDetailsByJobID(jobID);
            DV_InDesign.DataSource = jobDetails;
            DV_InDesign.DataBind();

            List<JobInfo> jobInfo = new List<JobInfo>();
            jobInfo = Brentwood.ListJobInfoByJob(jobID);
            GV_info.DataSource = jobInfo;
            GV_info.DataBind();

            List<JobProof> jobProofs = new List<JobProof>();
            jobProofs = Brentwood.ListJobProofsByJobID(jobID);
            ViewState.Add("jobproofs", jobProofs);
            GV_JobProofs.DataSource = jobProofs;
            GV_JobProofs.DataBind();

            RefreshProofsClean();
            RefreshAssets();
        }

        catch (Exception ex)
        {
            Label_SubmitProof.Text = ex.ToString();
            Label_SubmitProof.ForeColor = Color.Red;
        }
    }

    protected void GV_JobProofs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_JobProofs.PageIndex = e.NewPageIndex;
        RefreshProofs();
    }
    protected void GV_JobAssets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_JobAssets.PageIndex = e.NewPageIndex;
        RefreshAssets();
    }

    protected void RefreshProofs()
    {
        GridViewRow gvrow = GV_InDesign.SelectedRow;
        int jobID = int.Parse(gvrow.Cells[1].Text.Trim());
        List<JobProof> jobProofs = new List<JobProof>();
        if (ViewState["jobproofs"] != null)
        {
            jobProofs = (List<JobProof>)ViewState["jobproofs"];
        }
        else
        {
            jobProofs = Brentwood.ListJobProofsByJobID(jobID);
        }
        ViewState.Add("jobproofs", jobProofs);
        GV_JobProofs.DataSource = jobProofs;
        GV_JobProofs.DataBind();
    }

    protected void RefreshProofsClean()
    {
        GridViewRow gvrow = GV_InDesign.SelectedRow;
        int jobID = int.Parse(gvrow.Cells[1].Text.Trim());
        List<JobProof> jobProofs = new List<JobProof>();
        jobProofs = Brentwood.ListJobProofsByJobID(jobID);
        ViewState.Add("jobproofs", jobProofs);
        GV_JobProofs.DataSource = jobProofs;
        GV_JobProofs.DataBind();
    }

    protected void RefreshAssets()
    {
        GridViewRow gvrow = GV_InDesign.SelectedRow;
        int jobID = int.Parse(gvrow.Cells[1].Text.Trim());
        List<JobAsset> jobAssets = new List<JobAsset>();
        jobAssets = Brentwood.ListJobAssetsByJobID(jobID);
        GV_JobAssets.DataSource = jobAssets;
        GV_JobAssets.DataBind();
    }

    protected int SaveFile(HttpPostedFile file)
    {
        // Specify the path to save the uploaded file to
        int jobID = int.Parse(DV_InDesign.Rows[0].Cells[1].Text.Trim());
        Guid customerID = Guid.Parse(DV_InDesign.Rows[5].Cells[1].Text.Trim());
        string customerUserName = Brentwood.GetUserNameByUserID(customerID);

        string physPath = WebUtils.GetFolderPath(customerUserName, jobID, Server);

        // Get the name of the file to upload.
        string fileName = ProofFileUpload.FileName;

        // Create the path and file name to check for duplicates.
        string pathToCheck = physPath + "/" + fileName;

        // Create a temporary file name to use for checking duplicates.
        string tempfileName = "";

        // Check to see if a file already exists with the
        // same name as the file to upload.        
        if (File.Exists(pathToCheck))
        {
            int counter = 2;
            while (File.Exists(pathToCheck))
            {
                // if a file with this name already exists,
                // prefix the filename with a number.
                tempfileName = counter.ToString() + fileName;
                pathToCheck = physPath + tempfileName;
                counter++;
            }

            fileName = tempfileName;
        }

        // Append the name of the file to upload to the path.
        physPath = physPath + "/" + fileName;

        // Call the SaveAs method to save the uploaded
        // file to the specified directory.
        ProofFileUpload.SaveAs(physPath);
        int newProof = Brentwood.InsertJobProof(jobID, physPath);
        return newProof;
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

    protected void GV_InDesign_Sorting(object sender, GridViewSortEventArgs e)
    {

        List<JobsByJobStatusID_Result> data = (List<JobsByJobStatusID_Result>)ViewState["jobsindesign"];
        #region sortingExpressions
        switch (e.SortExpression)
        {

            case "TimeLeft":
                SortDirection tldirection = GetSortDirection("TimeLeft");
                if (tldirection == SortDirection.Ascending)
                {
                    data = data.OrderBy(d => d.PromiseDate).ToList();
                }
                else
                {
                    data = data.OrderByDescending(d => d.PromiseDate).ToList();
                }
                break;

            case "PromiseDate":
                SortDirection pddirection = GetSortDirection("PromiseDate");
                if (pddirection == SortDirection.Ascending)
                {
                    data = data.OrderBy(d => d.PromiseDate).ToList();
                }
                else
                {
                    data = data.OrderByDescending(d => d.PromiseDate).ToList();
                }
                break;

            case "StartDate":
                SortDirection sddirection = GetSortDirection("StartDate");
                if (sddirection == SortDirection.Ascending)
                {
                    data = data.OrderBy(d => d.StartDate).ToList();
                }
                else
                {
                    data = data.OrderByDescending(d => d.StartDate).ToList();
                }
                break;

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
        ViewState.Add("jobsindesign", data);
        GV_InDesign.DataSource = data;
        GV_InDesign.DataBind();
    }
    protected void GV_JobProofs_Sorting(object sender, GridViewSortEventArgs e)
    {
        List<JobProof> data = (List<JobProof>)ViewState["jobproofs"];
        #region sortingExpressions
        switch (e.SortExpression)
        {
            case "DateCreated":
                SortDirection dcdirection = GetSortDirection("DateCreated");
                if (dcdirection == SortDirection.Ascending)
                {
                    data = data.OrderBy(d => d.DateCreated).ToList();
                }
                else
                {
                    data = data.OrderByDescending(d => d.DateCreated).ToList();
                }
                break;

            case "Active":
                SortDirection adirection = GetSortDirection("Active");
                if (adirection == SortDirection.Ascending)
                {
                    data = data.OrderBy(d => d.Active).ToList();
                }
                else
                {
                    data = data.OrderByDescending(d => d.Active).ToList();
                }
                break;
        }
        #endregion
        ViewState.Add("jobproofs", data);
        RefreshProofs();
    }
    protected void EmailButton_Click(object sender, EventArgs e)
    {
        bool valid = true;

        if (GV_JobProofs.Rows.Count == 0)
        {
            valid = false;
            Label_SubmitProof.Text = "There are no proof files for this job.  E-mail was not sent.";
            Label_SubmitProof.ForeColor = Color.Red;
        }

        if (DV_InDesign.Rows.Count == 0)
        {
            valid = false;
            Label_SubmitProof.Text = "Please select a job before sending Email.";
            Label_SubmitProof.ForeColor = Color.Red;
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
                string subjectText = document.Element("appSettings").Element("submitProofSubject").Value;
                string body = document.Element("appSettings").Element("submitProofBody").Value;

                // Values from form
                int jobID = int.Parse(DV_InDesign.Rows[0].Cells[1].Text.Trim());
                string toAddress = DV_InDesign.Rows[12].Cells[1].Text.Trim();
                string jobName = DV_InDesign.Rows[1].Cells[1].Text.Trim();
                string subject = String.Format(subjectText, jobID, jobName);

                // Update Job Status
                int designApprovalStatusID = Brentwood.GetJobStatusByName("Pending Design Approval").JobStatusID;
                Guid employeeid = (Guid)Membership.GetUser().ProviderUserKey;
                Brentwood.ChangeJobJobStatus(designApprovalStatusID, jobID, employeeid);

                // Send email
                SmtpClient smtpclient = new SmtpClient(smtpClientAddress, smtpPort);
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                smtpclient.Send(message);

                // Clean form and prompt user
                ClearData();
                Label_SubmitProof.Text = "Customer has been Emailed.";
                Label_SubmitProof.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                Label_SubmitProof.Text = ex.ToString();
                Label_SubmitProof.ForeColor = Color.Red;
            }
        }
    }
    protected void Button_CustomerSearch_Click(object sender, EventArgs e)
    {
        // current status ID of 'In Design'
        int inDesignStatusID = Brentwood.GetJobStatusByName("In Design").JobStatusID;

        // get all if no search value
        if ((CustomerSearch.Text.Trim()) == "")
        {
            List<JobsByJobStatusID_Result> newJobs = new List<JobsByJobStatusID_Result>();
            newJobs = Brentwood.ListJobsByJobStatusID(inDesignStatusID);
            ViewState.Add("jobsindesign", newJobs);
            GV_InDesign.DataSource = newJobs;
            GV_InDesign.DataBind();
        }
        else
        {
            try
            {
                string nameSearch = CustomerSearch.Text.Trim();
                ClearData();

                // fill gridview
                List<JobsByJobStatusID_Result> customerSearchInDesign = new List<JobsByJobStatusID_Result>();
                customerSearchInDesign = Brentwood.ListJobsByJobStatusIDCustomerName(inDesignStatusID, nameSearch);

                if (customerSearchInDesign.Count == 0)
                {
                    Label_SubmitProof.Text = "No 'In Design' jobs were found for that search value.";
                    Label_SubmitProof.ForeColor = Color.Red;
                }
                else
                {
                    ViewState.Add("jobsindesign", customerSearchInDesign);
                    GV_InDesign.DataSource = customerSearchInDesign;
                    GV_InDesign.DataBind();
                }
            }
            catch (Exception ex)
            {
                Label_SubmitProof.Text = ex.ToString();
                Label_SubmitProof.ForeColor = Color.Red;
            }
        }
    }

    protected void Button_CompanySearch_Click(object sender, EventArgs e)
    {
        // current status ID of 'In Design'
        int inDesignStatusID = Brentwood.GetJobStatusByName("In Design").JobStatusID;

        // get all if no search value
        if ((CompanySearch.Text.Trim()) == "")
        {
            List<JobsByJobStatusID_Result> newJobs = new List<JobsByJobStatusID_Result>();
            newJobs = Brentwood.ListJobsByJobStatusID(inDesignStatusID);
            ViewState.Add("jobsindesign", newJobs);
            GV_InDesign.DataSource = newJobs;
            GV_InDesign.DataBind();
        }
        else
        {
            try
            {
                string nameSearch = CompanySearch.Text.Trim();
                ClearData();

                // fill gridview
                List<JobsByJobStatusID_Result> companySearchInDesign = new List<JobsByJobStatusID_Result>();
                companySearchInDesign = Brentwood.ListJobsByJobStatusIDCompanyName(inDesignStatusID, nameSearch);

                if (companySearchInDesign.Count == 0)
                {
                    Label_SubmitProof.Text = "No 'In Design' jobs were found for that search value.";
                    Label_SubmitProof.ForeColor = Color.Red;
                }
                else
                {
                    ViewState.Add("jobsindesign", companySearchInDesign);
                    GV_InDesign.DataSource = companySearchInDesign;
                    GV_InDesign.DataBind();
                }
            }
            catch (Exception ex)
            {
                Label_SubmitProof.Text = ex.ToString();
                Label_SubmitProof.ForeColor = Color.Red;
            }
        }
    }

    protected void ClearData()
    {
        GV_InDesign.DataSource = null;
        GV_InDesign.DataBind();

        DV_InDesign.DataSource = null;
        DV_InDesign.DataBind();

        GV_info.DataSource = null;
        GV_info.DataBind();

        GV_JobProofs.DataSource = null;
        GV_JobProofs.DataBind();

        GV_JobAssets.DataSource = null;
        GV_JobAssets.DataBind();
    }
}