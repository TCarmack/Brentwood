using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using BrentwoodPrinting;
using BrentwoodPrinting.Data;
using System.Net.Mail;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Net;
using System.Web.Security;

public partial class Admin_Invoicing_Invoicing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageLabel.Text = "";

        if (!Page.IsPostBack)
        {
            // Populate the GridView with outstanding invoices
            try
            {
                DateTime compareDate = DateTime.Now;
                List<GetOutstandingInvoices_Result> invoiceDays = new List<GetOutstandingInvoices_Result>();
                List<GetOutstandingInvoices_Result> outstandingInvoices = Brentwood.GetOutstandingInvoices();
                XDocument document = XDocument.Load(Server.MapPath("../Settings.xml"));
                int daysPastDue = int.Parse(document.Element("appSettings").Element("daysPastDue").Value);

                foreach (GetOutstandingInvoices_Result item in outstandingInvoices)
                {
                    compareDate = (DateTime)item.InvoicedDate;
                    compareDate = compareDate.AddDays(daysPastDue);
                    if (compareDate <= DateTime.Now)
                    {
                        invoiceDays.Add(item);
                    }
                }
                Session["outstandingInvoices"] = invoiceDays;
                BindData();
            }
            catch (Exception ex)
            {
                MessageLabel.Text = ex.Message;
                MessageLabel.ForeColor = Color.Red;
            }
        }
    }

    private void BindData()
    {
        InvoiceView.DataSource = (List<GetOutstandingInvoices_Result>)Session["outstandingInvoices"];
        InvoiceView.DataBind();
    }

    protected void SelectAll_Click(object sender, EventArgs e)
    {
        // Select all the checkboxes in the Send E-Mail column
        foreach (GridViewRow dgv in InvoiceView.Rows)
        {
            (dgv.Cells[7].FindControl("EmailCheckBox") as CheckBox).Checked = true;
        }
    }

    protected void UnselectAll_Click(object sender, EventArgs e)
    {
        // Unselect all the checkboxes in the Send E-Mail column
        foreach (GridViewRow dgv in InvoiceView.Rows)
        {
            (dgv.Cells[7].FindControl("EmailCheckBox") as CheckBox).Checked = false;
        }
    }

    protected void SendEmail_Click(object sender, EventArgs e)
    {
        XDocument document = XDocument.Load(Server.MapPath("../Settings.xml"));

        // Values from appSettings
        string fromAddress = document.Element("appSettings").Element("fromAddress").Value;
        string smtpPassword = document.Element("appSettings").Element("smtpPassword").Value;
        string smtpClientAddress = document.Element("appSettings").Element("smtpClient").Value;
        int smtpPort = int.Parse(document.Element("appSettings").Element("smtpPort").Value);
        string subjectText = document.Element("appSettings").Element("outstandingSubject").Value;
        string body = document.Element("appSettings").Element("outstandingBody").Value;

        // Run a loop to send out e-mails to each person that's checked off
        foreach (GridViewRow dgv in InvoiceView.Rows)
        {
            if ((dgv.Cells[7].FindControl("EmailCheckBox") as CheckBox).Checked)
            {
                // Values from form
                int jobID = int.Parse(dgv.Cells[2].Text.Trim());
                string toAddress = Brentwood.GetEmailByJobID(jobID);
                string jobName = dgv.Cells[1].Text.Trim();
                string subject = String.Format(subjectText, jobID, jobName);
                DateTime messagedOn = DateTime.Now;

                // Send E-Mail to customer notifying them of their outstanding invoice
                SmtpClient smtpclient = new SmtpClient(smtpClientAddress, smtpPort);
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(fromAddress, smtpPassword);
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
                smtpclient.Send(message);

                MessageLabel.Text = "E-Mails have been sent.";
                MessageLabel.ForeColor = Color.Red;

                Guid userId = (Brentwood.GetJob(jobID).aspnet_UsersReference.CreateSourceQuery().ToList<aspnet_Users>())[0].UserId;
                try
                {
                    // Update UserMessage
                    Guid employeeid = (Guid)Membership.GetUser().ProviderUserKey;
                    Brentwood.AddUserMessage(subject, body, jobID, Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()), userId, messagedOn);
                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.ToString();
                    MessageLabel.ForeColor = Color.Red;
                }
            }
        }
    }

    protected void InvoiceView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        InvoiceView.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void InvoiceView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            int jid = int.Parse(InvoiceView.DataKeys[e.NewSelectedIndex].Value.ToString());
            Brentwood.MarkJobAsPaid(jid);
            BindData();
            MessageLabel.Text = "Job marked as paid!";
            MessageLabel.ForeColor = Color.Blue;
            Response.Redirect("~/Admin/Invoicing/Invoicing.aspx");
        }
        catch (Exception ex)
        {
            MessageLabel.Text = ex.Message;
            MessageLabel.ForeColor = Color.Red;
        }
    }
}