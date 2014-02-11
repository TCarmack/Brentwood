<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.master" AutoEventWireup="true" CodeFile="OrderPage.aspx.cs" Inherits="Customer_Approved_Customer_OrderPage" %>
<%@ Reference Control="~/Controls/MultiFileUpload.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Context.Items["JobTypeID"] == null)
                Response.Redirect("NewOrder.aspx", true);
            else
            {
                Session["JobTypeID"] = Context.Items["JobTypeID"];
                HtmlTable table = FormBuilder.CreateTable(int.Parse(Session["JobTypeID"].ToString()));
                int number = 0;

                foreach (HtmlTableRow row in table.Rows)
                {
                    if (row.Cells[1].Controls[0] is FileUpload)
                    {
                        number++;
                        MultiFileUpload upload = new MultiFileUpload();
                        upload.ID = "10" + "FileUpload" + "FileUpload" + number;
                        row.Cells[1].Controls.Clear();
                        row.Cells[1].Controls.Add(upload);
                    }
                }
                
                Session["CurrentForm"] = table;

                foreach (Control c in Form.Controls)
                    if (c.ClientID == "MainContent")
                        c.Controls.AddAt(c.Controls.Count - 4, (HtmlTable)Session["CurrentForm"]);
            }
        }

        FormPanel.Controls.Add((HtmlTable)Session["CurrentForm"]);
        base.OnInit(e);
    }
</script>
<script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.MultiFile.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        //Ensures only numbers are input
        $('input[id$="500TextBoxQuantityTextbox"]').keydown(function (event) {
            if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
                event.preventDefault();
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Step Two: Additional Information</h1>
    <p>Selected Job Type: <asp:Label ID="JobTypeLabel" runat="server" /></p>
    <asp:Panel ID="FormPanel" runat="server">
    </asp:Panel>
    <asp:LinkButton ID="BackButton" runat="server" Text="Back" 
        onclick="BackButton_Click" />
    <asp:LinkButton runat="server" ID="SubmitButton" Text="Submit Job" onclick="SubmitButton_Click" PostBackUrl="~/Customer/Approved Customer/OrderPage.aspx"/>
    <asp:Label runat="server" ID="FormMessage" />
</asp:Content>