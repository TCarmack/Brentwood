using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
//Jack
struct ControlListItem
{
    public string XmlNodeName;
    public string ControlName;

    public ControlListItem(string xmlNodeName, string controlName)
    {
        XmlNodeName = xmlNodeName;
        ControlName = controlName;
    }
}

public partial class Admin_SiteSettings : System.Web.UI.Page
{
    List<ControlListItem> controls = new List<ControlListItem>();

    protected override void OnInit(EventArgs e)
    {
        string filename = Server.MapPath("settings.xml");
        XPathDocument document = new XPathDocument(filename);
        XPathNavigator navigator = document.CreateNavigator();
        XPathExpression expression = navigator.Compile("/appSettings/*");
        XPathNodeIterator iterator = navigator.Select(expression);

        HtmlTable table = new HtmlTable();
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        string tooltip = "";
        try
        {
            while (iterator.MoveNext())
            {
                tooltip = "";
                row = new HtmlTableRow();
                cell = new HtmlTableCell();

                XPathNavigator navigator2 = iterator.Current.Clone();
                Label label = new Label();
                label.ID = navigator2.Name + "Label";
                label.Text = navigator2.GetAttribute("name", navigator2.NamespaceURI);
                tooltip = navigator2.GetAttribute("description", navigator2.NamespaceURI);

                Control c = null;

                if (label.ID != "")
                {
                    c = new TextBox();
                    c.ID = navigator2.GetAttribute("name", navigator2.NamespaceURI) + "Textbox";
                    (c as TextBox).Text = navigator2.Value;
                    (c as TextBox).ToolTip = tooltip;
                    (c as TextBox).Width = 700;
                    (c as TextBox).TextMode = TextBoxMode.MultiLine;
                    label.ToolTip = tooltip;
                }

                cell.Controls.Add(label);
                row.Cells.Add(cell);
                
                cell = new HtmlTableCell();
                cell.Controls.Add(c);
                row.Cells.Add(cell);
                controls.Add(new ControlListItem(navigator2.Name, c.ID));

                table.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }

        ControlsPanel.Controls.Add(table);

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        string filename = Server.MapPath("settings.xml");
        var document = XDocument.Load(filename);

        foreach (ControlListItem c in controls)
        {
            TextBox control = (ControlsPanel.FindControl(c.ControlName) as TextBox);
            string value = control.Text.Trim();

            document
                .Element("appSettings")
                .SetElementValue(c.XmlNodeName, value);
        }

        document.Save(filename);

        FormMessage.Text = "File updated successfully.";
    }

    protected void BackupButton_Click(object sender, EventArgs e)
    {
        File.Copy(Server.MapPath("BackupSettings.xml"), Server.MapPath("Settings.xml"), true);
        Response.Redirect("~/Admin/SiteSettings.aspx");
        FormMessage.Text = "Settings restored from backup.";
    }

    protected void CreateBackupButton_Click(object sender, EventArgs e)
    {
        File.Copy(Server.MapPath("Settings.xml"), Server.MapPath("BackupSettings.xml"), true);
        FormMessage.Text = "Backup successfully created.";
    }

    protected void MasterBackup_Click(object sender, EventArgs e)
    {
        File.Copy(Server.MapPath("MasterSettings.xml"), Server.MapPath("Settings.xml"), true);
        FormMessage.Text = "Settings restored from master file.";
    }
}