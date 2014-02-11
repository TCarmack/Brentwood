using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Web.Security;
using System.Xml.XPath;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Employee_WalkInOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public HtmlTable CreateForm(int JobID, HttpServerUtility server)
    {
        Job job = Brentwood.GetJob(JobID);
        List<JobInfo> info = Brentwood.ListJobInfoByJob(JobID);
        List<JobAsset> assets = Brentwood.ListJobAssetsByJobID(JobID);

        HtmlTable table = new HtmlTable();

        table.Rows.Add(CreateRow("Order ID No:", job.JobID.ToString()));

        foreach (JobInfo item in info)
            table.Rows.Add(CreateRow(item));

        table.Rows.Add(CreateRow("Estimated Delivery Date:", ((DateTime)job.PromiseDate).ToString("dd MMM yyyy hh:mm tt")));

        foreach (JobAsset item in assets)
            table.Rows.Add(CreateAssetRow(item, server));

        return table;
    }

    private HtmlTableRow CreateRow(JobInfo item)
    {
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(CreateCell(item.NameKey));
        row.Cells.Add(CreateCell(item.DataValue));
        return row;
    }

    private HtmlTableRow CreateRow(string name, string value)
    {
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(CreateCell(name));
        row.Cells.Add(CreateCell(value));
        return row;
    }

    private HtmlTableCell CreateCell(string data)
    {
        HtmlTableCell cell = new HtmlTableCell();
        Label label = new Label();
        label.ID = data + "Label";
        label.Text = data;
        cell.Controls.Add(label);
        return cell;
    }

    private HtmlTableRow CreateAssetRow(JobAsset item, HttpServerUtility server)
    {
        HtmlTableRow row = new HtmlTableRow();
        string filename = Path.GetFileName(item.Filepath);
        row.Cells.Add(CreateCell("Job Asset: " + filename));
        HtmlTableCell cell = new HtmlTableCell();
        cell = new HtmlTableCell();
        string url = WebUtils.ResolveVirtualPath(item.Filepath).Replace("~", "..");
        url = url.Replace("Customer/", "");
        cell.InnerHtml = String.Format("<a href=\"{0}\" target=\"blank\">{1}</a>", url, "Open File");
        row.Cells.Add(cell);

        return row;
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string firstname = FirstNameTextbox.Text.Trim();
        string lastname = LastNameTextbox.Text.Trim();
        string username = firstname + lastname;
        string email = EmailTextbox.Text.Trim();

        aspnet_Users user = Brentwood.GetUser(username);

        if (user == null)
        {
            Membership.CreateUser(username, username + "1", email);
            user = Brentwood.GetUser(username);
            Roles.AddUserToRole(user.UserName, "Approved Customer");
            Brentwood.UpdateCustomer(user, email);
        }

        //Submit job
        int jobID = -1;
        JobInfo info = new JobInfo();
        Control currentControl = null;
        int jobTypeID = int.Parse(Session["JobTypeID"].ToString());
        string specialInstructions = "";
        int quantity = -1;
        string deliveryOrPickup = "";
        Guid customerID = Guid.Empty;
        DateTime promiseDate = DateTime.Today.AddYears(-400);
        List<string> jobAssets = new List<string>();
        List<JobInfo> jobInfo = new List<JobInfo>();
        bool getFiles = false;

        string filename = Server.MapPath("../Admin/Settings.xml");
        XPathDocument document = new XPathDocument(filename);
        XPathNavigator navigator = document.CreateNavigator();
        XPathExpression expression = navigator.Compile("/appSettings/initialJobStatus");
        XPathNodeIterator iterator = navigator.Select(expression);
        iterator.MoveNext();
        XPathNavigator nav2 = iterator.Current.Clone();
        int jobStatus = Brentwood.GetJobStatusByName("Pending Approval").JobStatusID;

        try
        {
            customerID = (Brentwood.LookupCustomerByUsername(User.Identity.Name)).UserId;
            promiseDate = Utils.GetPromiseDate(jobTypeID);
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }


        foreach (HtmlTableRow row in ((HtmlTable)Session["CurrentForm"]).Rows)
        {
            info = new JobInfo();
            info.NameKey = (row.Cells[0].Controls[0] as Label).Text;
            currentControl = ControlsPanel.FindControl(row.Cells[1].Controls[0].ID);

            if (currentControl.ID == "500TextBoxQuantityTextbox")
            {
                if ((currentControl as TextBox).Text != "")
                {
                    quantity = int.Parse((currentControl as TextBox).Text.Trim());
                }
            }
            else if (currentControl.ID == "501TextBoxSpecialInstructionsTextbox")
            {
                specialInstructions = (currentControl as TextBox).Text.Trim();
            }
            else if (currentControl.ID == "502CheckBoxDeliveryCheckbox")
            {
                if ((currentControl as CheckBox).Checked)
                {
                    deliveryOrPickup = "D";
                }
                else
                {
                    deliveryOrPickup = "P";
                }
            }
            else
            {
                if (row.Cells[1].Controls[0] is TextBox)
                {
                    info.DataValue = ((TextBox)currentControl).Text.Trim();
                    jobInfo.Add(info);
                }
                else if (row.Cells[1].Controls[0] is CheckBox)
                {
                    if (((CheckBox)currentControl).Checked)
                    {
                        info.DataValue = "true";
                        jobInfo.Add(info);
                    }
                    else
                    {
                        info.DataValue = "false";
                        jobInfo.Add(info);
                    }
                }
                else if (row.Cells[1].Controls[0] is Controls_MultiFileUpload)
                {
                    getFiles = true;
                }
                else
                {
                    FormMessage.Text = "Control type invalid.";
                    FormMessage.ForeColor = Color.Red;
                }
            }
        }

        try
        {
            jobID = Brentwood.AddJob(jobTypeID, specialInstructions, quantity, deliveryOrPickup, customerID, promiseDate);
            Brentwood.InitializeJobJobStatus(jobID, (Brentwood.LookupCustomerByUsername(User.Identity.Name.ToString())).UserId);
            Brentwood.AddInfoToJob(jobInfo, jobID);

            if (getFiles)
            {
                string physicalPath = "";
                string virtualPath = "";

                // Get the HttpFileCollection
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];
                    if (hpf.ContentLength > 0)
                    {
                        physicalPath = WebUtils.GetFolderPath(User.Identity.Name, jobID, Server) + "\\" + System.IO.Path.GetFileName(hpf.FileName);
                        virtualPath = WebUtils.ResolveVirtualPath(physicalPath);

                        hpf.SaveAs(Server.MapPath(virtualPath));
                        jobAssets.Add(physicalPath);
                    }
                }
            }

            Brentwood.AddAssetsToJob(jobAssets, jobID);


            FormMessage.Text = "Job successfully submitted!";
            FormMessage.ForeColor = Color.Blue;
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void SelectButton_Click(object sender, EventArgs e)
    {
        Session["JobTypeID"] = JobTypesList.SelectedValue;
        Response.Redirect("WalkInOrder.aspx");
    }
}