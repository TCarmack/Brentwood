using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BrentwoodPrinting.Data;
using System.IO;

public partial class Customer_Approved_Customer_OrderCompletePage : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        if (Context.Items["JobID"] == null)
            Response.Redirect("NewOrder.aspx", true);

        ControlsPanel.Controls.Add(CreateForm(int.Parse(Context.Items["JobID"].ToString()), Server));
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region FormBuilderStuff
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
    #endregion

}