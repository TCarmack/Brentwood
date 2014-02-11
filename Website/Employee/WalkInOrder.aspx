<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="WalkInOrder.aspx.cs" Inherits="Employee_WalkInOrder" %>
<%@ Reference Control="~/Controls/MultiFileUpload.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../Scripts/AddressScript.js"></script>
    <style type="text/css">
        .style1
        {
            width: 31%;
        }
        .style2
        {
            width: 116px;
            font-size: medium;
        }
        .style3
        {
            width: 38%;
            margin-right: 0px;
        }
        .style5
        {
            width: 112px;
        }
        .style8
        {
            width: 101px;
            font-size: medium;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //Ensures only numbers are input
            $('input[id$="PhoneNumberTextbox"]').keydown(function (event) {
                if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
                    event.preventDefault();
                }
            });

            $('input[id$="QuantityTextbox"]').keydown(function (event) {
                if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
                    event.preventDefault();
                }
            });
        });
    </script>
    <script runat="server">
    protected override void OnInit(EventArgs e)
    {
        if (Session["JobTypeID"] != null)
        {
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
            
           ControlsPanel.Controls.Add((HtmlTable)Session["CurrentForm"]);
        }
        base.OnInit(e);
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Walk In Order</h1>
    <asp:Label ID="Label1" runat="server" Text="Select a Job Type"></asp:Label>
    
    <asp:DropDownList ID="JobTypesList" runat="server"
        DataSourceID="ObjectDataSource1" DataTextField="Name" 
        DataValueField="JobTypeID">
    </asp:DropDownList>
    <asp:LinkButton ID="SelectButton" runat="server" onclick="SelectButton_Click">Select</asp:LinkButton>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="ListJobTypes" 
        TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>
    <asp:Panel ID="ControlsPanel" runat="server">
        <table class="style1">
            <tr>
                <td class="style2">
                    First Name:</td>
                <td>
                    <asp:TextBox ID="FirstNameTextbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Last Name:</td>
                <td>
                    <asp:TextBox ID="LastNameTextbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Email:</td>
                <td> <asp:TextBox ID="EmailTextbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="style2">
                    Phone Number:</td>
                <td>
                    <asp:TextBox ID="PhoneNumberTextbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Delivery?</td>
                <td>
                    <asp:CheckBox ID="DeliveryCheckbox" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style8">
                    Address:</td>
                <td class="style5">
                    <asp:TextBox ID="AddressTextbox" runat="server" Height="100px" Width="200px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8">
    &nbsp;City:</td>
                <td class="style5">
                    <asp:TextBox ID="CityTextbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    Province:</td>
                <td class="style5">
                    <asp:TextBox ID="ProvinceTextbox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    Postal Code:</td>
                <td class="style5">
                    <asp:TextBox ID="PostalTextbox" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Label ID="FormMessage" runat="server" />
    <asp:LinkButton ID="SubmitButton" runat="server" onclick="SubmitButton_Click">Submit Order</asp:LinkButton>
</asp:Content>