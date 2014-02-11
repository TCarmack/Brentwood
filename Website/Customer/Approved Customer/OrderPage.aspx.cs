using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using BrentwoodPrinting.Data.FormBuilder;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;
using System.Xml.XPath;
using System.Web.Security;
//Jack
public partial class Customer_Approved_Customer_OrderPage : System.Web.UI.Page
{
    //OnInit method override on page html

    protected void Page_Load(object sender, EventArgs e)
    {
        JobTypeLabel.Text = Brentwood.GetJobType(int.Parse(Session["JobTypeID"].ToString())).Name;
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
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

        string filename = Server.MapPath("../../Admin/Settings.xml");
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
            currentControl = FormPanel.FindControl(row.Cells[1].Controls[0].ID);

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
            FormMessage.ForeColor = Color.Green;

            Context.Items.Add("JobID", jobID);
            Server.Transfer("OrderCompletePage.aspx");
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
        //End of SubmitButton_Click method
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("NewOrder.aspx");
    }
}