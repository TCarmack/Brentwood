using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BrentwoodPrinting;
using BrentwoodPrinting.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;
//Jack
public class WebUtils
{
	/// <summary>
    /// Gets the current folder path for the customer with the specified username and job ID.
    /// </summary>
    /// <param name="username">The username to get a folder path for.</param>
    /// <param name="jobID">The Job ID to get the folder path for.</param>
    /// <returns>A string indicating the current folder path.</returns>
    public static string GetFolderPath(string username, int jobID, HttpServerUtility server)
    {
        string folderPath = server.MapPath("..\\Images\\");
        
        /* -Get the username
         * -Check if he's part of a company
         * -If yes, get/create company folder
         * -If no, get/create user folder
         * -Get the current year
         * -Get/Create current year folder
         * -Get the name of the current month
         * -Get/Create current month folder
         * -Get the job object
         * -Get/create job object folder
         * -Concatenate all into return string
         * -Return string
         * -Pray
         */
        aspnet_Users user = Brentwood.LookupCustomerByUsername(username);
        if (user.CompanyID == null)
            folderPath = GetOrCreateFolder(folderPath + user.UserName);
        else
        {
            Company company = Brentwood.GetCompanyByCustomerId(user.UserId);
            folderPath = GetOrCreateFolder(folderPath + company.Name);
        }

        string year = DateTime.Today.ToString("yyyy");
        folderPath = GetOrCreateFolder(folderPath + "\\" + year);

        string currentMonth = DateTime.Today.ToString("MMM");
        folderPath = GetOrCreateFolder(folderPath + "\\" + currentMonth);

        Job job = Brentwood.GetJob(jobID);
        JobType jobType = Brentwood.GetJobTypeByJob(jobID);
        string date = DateTime.Today.ToString("dd");
        date += DateTime.Now.ToString("-HH-mm");
        string folder = jobType.Name + "_" + date;
        folderPath = GetOrCreateFolder(folderPath + "\\" + folder);

        return folderPath;
    }

    public static string ResolveVirtualPath(string physicalPath)
    {
        string virtualPath = "";

        string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
        virtualPath = physicalPath.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");

        return virtualPath;
    }

    private static string GetOrCreateFolder(string folderPath)
    {
        string returnValue = "";

        if (Directory.Exists(folderPath))
            returnValue = folderPath;
        else
            returnValue = Directory.CreateDirectory(folderPath).FullName;

        return returnValue;
    }
}

public static class FormBuilder
{
    public static HtmlTable CreateTable(int jobID)
    {
        HtmlTable table = new HtmlTable();
        List<JobControl_Get_Result> results = Brentwood.ListJobControlByJobType(jobID);

        foreach (JobControl_Get_Result r in results)
            table.Rows.Add(AddRow(r));

        table = AddStandardControls(table);

        return table;
    }

    private static HtmlTableRow AddRow(JobControl_Get_Result controlResult)
    {
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(AddLabel(controlResult.ControlName));
        row.Cells.Add(AddControl(controlResult));
        return row;
    }

    private static HtmlTableCell AddLabel(string labelText)
    {
        HtmlTableCell cell = new HtmlTableCell();
        Label l = new Label();
        l.Text = labelText;
        cell.Controls.Add(l);
        return cell;
    }

    private static HtmlTableCell AddControl(JobControl_Get_Result control)
    {
        HtmlTableCell cell = new HtmlTableCell();
        Control c = new Control();

        switch (control.ControlTypeName)
        {
            case "TextBox":
                c = new TextBox();
                c.ID = control.JobControlID + control.ControlTypeName + control.ControlName;
                break;

            case "CheckBox":
                c = new CheckBox();
                c.ID = control.JobControlID + control.ControlTypeName + control.ControlName;
                break;

            case "ImageUpload":
                c = new FileUpload();
                c.ID = control.JobControlID + control.ControlTypeName + control.ControlName;
                break;
        }

        cell.Controls.Add(c);
        return cell;
    }

    private static HtmlTable AddStandardControls(HtmlTable table)
    {
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(AddLabel("Quantity"));
        row.Cells.Add(AddControl(JobControl_Get_Result.CreateJobControl_Get_Result(500, "QuantityTextbox", "TextBox")));
        table.Rows.Add(row);

        row = new HtmlTableRow();
        row.Cells.Add(AddLabel("Special Instructions"));
        TextBox box = new TextBox();
        box.ID = "501TextBoxSpecialInstructionsTextbox";
        box.TextMode = TextBoxMode.MultiLine;
        HtmlTableCell cell = new HtmlTableCell();
        cell.Controls.Add(box);
        row.Cells.Add(cell);
        table.Rows.Add(row);

        row = new HtmlTableRow();
        row.Cells.Add(AddLabel("Delivery?"));
        row.Cells.Add(AddControl(JobControl_Get_Result.CreateJobControl_Get_Result(502, "DeliveryCheckbox", "CheckBox")));
        table.Rows.Add(row);

        return table;
    }
}

public static class JobDataFormBuilder
{
    public static HtmlTable CreateForm(int JobID, HttpServerUtility server)
    {
        List<JobInfo> info = Brentwood.ListJobInfoByJob(JobID);
        List<JobAsset> assets = Brentwood.ListJobAssetsByJobID(JobID);

        HtmlTable table = new HtmlTable();

        foreach (JobInfo item in info)
            table.Rows.Add(CreateRow(item));

        foreach (JobAsset item in assets)
            table.Rows.Add(CreateAssetRow(item, server));

        return table;
    }

    private static HtmlTableRow CreateRow(JobInfo item)
    {
        HtmlTableRow row = new HtmlTableRow();
        row.Cells.Add(CreateCell(item.NameKey));
        row.Cells.Add(CreateCell(item.DataValue));
        return row;
    }

    private static HtmlTableCell CreateCell(string data)
    {
        HtmlTableCell cell = new HtmlTableCell();
        Label label = new Label();
        label.ID = data + "Label";
        label.Text = data;
        cell.Controls.Add(label);
        return cell;
    }

    private static HtmlTableRow CreateAssetRow(JobAsset item, HttpServerUtility server)
    {
        HtmlTableRow row = new HtmlTableRow();
        string filename = Path.GetFileName(item.Filepath);
        row.Cells.Add(CreateCell("Job Asset: " + filename));
        HtmlTableCell cell = new HtmlTableCell();
        cell = new HtmlTableCell();
        LinkButton button = new LinkButton();

        button.Text = "Open File";
        button.OnClientClick = "OFButton_Click(\"" + WebUtils.ResolveVirtualPath(item.Filepath) + "\");";

        cell.Controls.Add(button);
        row.Cells.Add(cell);

        return row;
    }
}