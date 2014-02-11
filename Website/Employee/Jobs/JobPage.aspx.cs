using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing;
using System.Xml.Linq;
using System.Net.Mail;
using System.Net;

struct JobData
{
    public int ID;
    public string key;
    public string value;
    public string controlID;
    public int jobID;

    public JobData(int id, string key, string value, string controlID, int jobID)
    {
        this.ID = id;
        this.key = key;
        this.value = value;
        this.controlID = controlID;
        this.jobID = jobID;
    }
}
public partial class Employee_Jobs_JobPage : System.Web.UI.Page
{
    private List<JobData> JobInformation = new List<JobData>();

    protected override void OnInit(EventArgs e)
    {
        if (!Page.IsPostBack)
            if (Context.Items["JobID"] == null)
                Response.Redirect("../EmployeeDashboard.aspx", true);
            else
                Session["JobID"] = Context.Items["JobID"];

        ControlsPanel.Controls.Add(CreateForm(int.Parse(Session["JobID"].ToString()), Server));

        //Get status, if is pending quote, make quote button visible
        int jobstatusid = Brentwood.GetJobStatusByJob(int.Parse(Session["JobID"].ToString())).JobStatusID;

        if (jobstatusid == 2)
            QuotePanel.Visible = true;
        else
            QuotePanel.Visible = false;

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }

    private void BindData()
    {
        try
        {
            int jobID = int.Parse(Session["JobID"].ToString());
            StatusesList.DataSource = Brentwood.ListJobStatusesByJob(jobID);
            StatusesList.DataBind();

            List<JobStatus> statuses = Brentwood.ListJobStatuses();
            JobStatusDropDown.DataSource = statuses;
            int index = statuses.FindIndex(item => item.JobStatusID == (Brentwood.GetCurrentJobStatus(jobID)).JobStatusID);

            JobStatusDropDown.SelectedIndex = index;
            JobStatusDropDown.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    public void OFButton_Click(string url)
    {
        Response.Redirect(url);
    }

    protected void ChangeStatusButton_Click(object sender, EventArgs e)
    {
        Brentwood.ChangeJobJobStatus(int.Parse(JobStatusDropDown.SelectedValue.ToString()),
            int.Parse(Session["JobID"].ToString()),
            Brentwood.GetUser(User.Identity.Name).UserId);
        BindData();
    }

    protected void QuoteJob_Click(object sender, EventArgs e)
    {
        try
        {
            decimal quoteAmount = decimal.Parse(QuoteTextbox.Text);
            JobInfo info = new JobInfo();
            info.NameKey = "Price Quote";
            info.DataValue = quoteAmount.ToString("C");
            List<JobInfo> infoList = new List<JobInfo>();
            infoList.Add(info);
            Brentwood.AddInfoToJob(infoList, int.Parse(Session["JobID"].ToString()));
            string toAddress = Brentwood.GetCustomer(Brentwood.GetJob(int.Parse(Session["JobID"].ToString())).CustomerID).Email;
            XDocument document = XDocument.Load(Server.MapPath("../Admin/Settings.xml"));
            string fromAddress = document.Element("appSettings").Element("fromAddress").Value;
            string smtpPassword = document.Element("appSettings").Element("smtpPassword").Value;
            string smtpClientAddress = document.Element("appSettings").Element("smtpClient").Value;
            string subject = document.Element("appSettings").Element("quoteSubject").Value;
            string body = document.Element("appSettings").Element("quoteBody").Value;
            int smtpPort = int.Parse(document.Element("appSettings").Element("smtpPort").Value);
            SmtpClient smtpclient = new SmtpClient(smtpClientAddress, smtpPort);
            smtpclient.EnableSsl = true;
            smtpclient.UseDefaultCredentials = false;
            smtpclient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
            smtpclient.Send(message);
            FormMessage.Text = "Job has been quoted. An email has been sent to the customer.";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    #region FormBuilderStuff
    public HtmlTable CreateForm(int JobID, HttpServerUtility server)
    {
        List<JobInfo> info = Brentwood.ListJobInfoByJob(JobID);
        List<JobAsset> assets = Brentwood.ListJobAssetsByJobID(JobID);

        HtmlTable table = new HtmlTable();

        foreach (JobInfo item in info)
            table.Rows.Add(CreateRow(item));

        foreach (JobAsset item in assets)
            table.Rows.Add(CreateAssetRow(item, server));

        Job job = Brentwood.GetJob(JobID);
        List<HtmlTableRow> rows = AddAdditionalInfo(job);

        foreach (HtmlTableRow item in rows)
            table.Rows.Add(item);

        return table;
    }

    private HtmlTableRow CreateRow(JobInfo item)
    {
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(CreateCell(item.NameKey, item.JobInfoID));
        row.Cells.Add(CreateInputCell(item.JobInfoID.ToString(), item.DataValue));
        JobInformation.Add(new JobData(item.JobInfoID, item.NameKey, item.DataValue, item.DataValue + "TextBox", item.JobID));
        return row;
    }

    private HtmlTableCell CreateInputCell(string key, string data)
    {
        HtmlTableCell cell = new HtmlTableCell();
        TextBox label = new TextBox();
        label.ID = key + "TextBox";
        label.Text = data;
        cell.Controls.Add(label);
        return cell;
    }

    private HtmlTableCell CreateCell(string data, int key)
    {
        HtmlTableCell cell = new HtmlTableCell();
        Label label = new Label();
        label.ID = key.ToString() + data + "Label";
        label.Text = data;
        cell.Controls.Add(label);
        return cell;
    }

    private HtmlTableRow CreateAssetRow(JobAsset item, HttpServerUtility server)
    {
        HtmlTableRow row = new HtmlTableRow();
        string filename = Path.GetFileName(item.Filepath);
        row.Cells.Add(CreateCell("Job Asset: " + filename, item.JobAssetID));
        HtmlTableCell cell = new HtmlTableCell();
        cell = new HtmlTableCell();
        string url = WebUtils.ResolveVirtualPath(item.Filepath).Replace("~", "..");
        cell.InnerHtml = String.Format("<a href=\"{0}\" target=\"blank\">{1}</a>", url, "Open File");
        row.Cells.Add(cell);

        return row;
    }

    private List<HtmlTableRow> AddAdditionalInfo(Job item)
    {
        List<HtmlTableRow> returnValue = new List<HtmlTableRow>();

        returnValue.Add(CreateRow("Special Instructions", item.SpecialInstructions));
        returnValue.Add(CreateRow("Quantity", item.Quantity));

        if (item.DeliveryOrPickup == "P")
            returnValue.Add(CreateReadOnlyRow("Delivery Or Pickup", "Pickup"));
        else if (item.DeliveryOrPickup == "D")
            returnValue.Add(CreateReadOnlyRow("Delivery Or Pickup", "Delivery"));
        else
            returnValue.Add(CreateReadOnlyRow("Delivery Or Pickup", "Unspecified"));

        returnValue.Add(CreateReadOnlyRow("Promise Date", item.PromiseDate));
        returnValue.Add(CreateReadOnlyRow("Start Date", item.StartDate));
        returnValue.Add(CreateReadOnlyRow("Invoiced Date", item.InvoicedDate));

        if (item.Paid == "Y")
            returnValue.Add(CreateReadOnlyRow("Paid", "Yes"));
        else
            returnValue.Add(CreateReadOnlyRow("Paid", "No"));

        returnValue.Add(CreateReadOnlyRow("Quote Amount", item.QuoteAmount));

        aspnet_Users customer = Brentwood.LookupCustomer(item.CustomerID);

        if (customer.FirstName == null || customer.LastName == null)
            returnValue.Add(CreateReadOnlyRow("Customer", customer.UserName));
        else
            returnValue.Add(CreateReadOnlyRow("Customer", customer.FirstName + " " + customer.LastName));

        return returnValue;
    }

    private HtmlTableRow CreateRow(object key, object value)
    {
        JobInfo item = new JobInfo();

        if (value != null)
            item.DataValue = value.ToString();
        else
            item.DataValue = "Not specified";

        item.NameKey = key.ToString();

        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        Label label = new Label();
        label.ID = item.NameKey + "Label";
        label.Text = item.NameKey;
        cell.Controls.Add(label);
        row.Cells.Add(cell);
        cell = new HtmlTableCell();
        TextBox textbox = new TextBox();
        textbox.ID = key + "TextBox";
        textbox.Text = item.DataValue;
        cell.Controls.Add(textbox);
        row.Cells.Add(cell);
        JobInformation.Add(new JobData(item.JobInfoID, item.NameKey, item.DataValue, item.DataValue + "TextBox", item.JobID));

        return row;
    }

    private HtmlTableRow CreateReadOnlyRow(object key, object value)
    {
        JobInfo item = new JobInfo();

        if (value != null)
            item.DataValue = value.ToString();
        else
            item.DataValue = "Not specified";

        item.NameKey = key.ToString();

        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        Label label = new Label();
        label.ID = item.NameKey + "Label";
        label.Text = item.NameKey;
        cell.Controls.Add(label);
        row.Cells.Add(cell);
        cell = new HtmlTableCell();
        Label textbox = new Label();
        textbox.ID = key + "TextBox";
        textbox.Text = item.DataValue;
        cell.Controls.Add(textbox);
        row.Cells.Add(cell);
        JobInformation.Add(new JobData(item.JobInfoID, item.NameKey, item.DataValue, item.DataValue + "TextBox", item.JobID));

        return row;
    }
    #endregion

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        List<JobInfo> data = new List<JobInfo>();
        JobInfo info;
        foreach (JobData item in JobInformation)
        {
            info = new JobInfo();
            info.JobInfoID = item.ID;
            info.NameKey = item.key;
            info.DataValue = item.value;
            info.JobID = item.jobID;
        }

        try
        {
            Job job = Brentwood.GetJob(int.Parse(Session["JobID"].ToString()));
            job.SpecialInstructions = (ControlsPanel.FindControl("Special InstructionsTextBox") as TextBox).Text.Trim();
            job.Quantity = int.Parse((ControlsPanel.FindControl("QuantityTextBox") as TextBox).Text.Trim());
            job.DeliveryOrPickup = (ControlsPanel.FindControl("Delivery or PickupTextBox") as TextBox).Text.Trim();

            Brentwood.UpdateJob(job);
            Brentwood.UpdateJobInfo(data);
            FormMessage.Text = "Job successfully updated.";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void FinishButton_Click(object sender, EventArgs e)
    {
        try
        {
            Brentwood.MarkJobAsInvoiced(int.Parse(Session["JobID"].ToString()));
            FormMessage.Text = "Job has been marked as completed.";
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}