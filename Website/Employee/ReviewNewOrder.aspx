<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="ReviewNewOrder.aspx.cs" Inherits="Employee_ReviewNewOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<style type="text/css">
        .style1
        {
        width: 100px;
        text-align: right;
        }
        .style2
        {
            width: 50%;
        }
        .style4
        {
        width: 200px;
        text-align: right;
        }
        .style5
        {
            width: 200px;
            text-align: right;
            height: 30px;
        }
        .style6
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>Review New Orders</h1>
<h2>Search:</h2>
<table style="width: 97%;">
        <tr>
            <td class="style5">By Customer Name:</td>
            <td class="style6">
                <asp:TextBox ID="CustomerSearch" runat="server"></asp:TextBox>
                <asp:Button ID="Button_CustomerSearch" runat="server" Text="Search" 
                    onclick="Button_CustomerSearch_Click" />
            </td>
        </tr>
        <tr>
            <td class="style4">OR</td>
        </tr>
        <tr>
            <td class="style4">By Company Name:</td>
            <td>
                <asp:TextBox ID="CompanySearch" runat="server"></asp:TextBox>
                <asp:Button ID="Button_CompanySearch" runat="server" Text="Search" 
                    onclick="Button_CompanySearch_Click" />
            </td>
        </tr>
    </table>
    <p style="margin-left:200px;">Note: Clicking "Search" with an empty field will display all new jobs.</p>
<h2>Select a Job (NEW JOBS):</h2>
<p>
    <asp:Label ID="Label_ReviewNewOrder" runat="server" Text=""></asp:Label>
</p>
<br />
    <table style="width: 97%;">
        <tr>
            <td>
                <asp:GridView ID="GV_NewOrder" runat="server" HorizontalAlign="Center" 
                    AutoGenerateColumns="False" onpageindexchanging="GV_NewOrder_PageIndexChanging" 
                    onselectedindexchanged="GV_NewOrder_SelectedIndexChanged" 
                    AllowPaging="True" PageSize="5" AllowSorting="True" 
                    onsorting="GV_NewOrder_Sorting" Width="90%">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="JobID" HeaderText="Job ID" />
                        <asp:BoundField DataField="JobName" HeaderText="Type" 
                            SortExpression="JobName" />
                        <asp:BoundField DataField="CompanyName" HeaderText="Company" 
                            SortExpression="CompanyName" />
                        <asp:BoundField DataField="CustomerFirstName" HeaderText="First Name" 
                            SortExpression="CustomerFirstName" />
                        <asp:BoundField DataField="CustomerLastName" HeaderText="Last Name" 
                            SortExpression="CustomerLastName" />
                        <asp:BoundField DataField="CustomerEmail" HeaderText="Email" />
                    </Columns>
                    <EmptyDataTemplate>There are no jobs 'Pending Approval' or no jobs for that search value.</EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
<br />
    <table style="width: 97%;">
    <tr>
        <td class="style2"><h2>Review Job Details:</h2></td>
        <td class="style2"><h2>Attributes:</h2>
        </td>
    </tr>
    </table>
<br />
    <table style="width: 97%;">
        <tr>
            <td class="style2">
                <asp:DetailsView ID="DV_NewOrder" runat="server" Height="50px" Width="90%" 
                    HorizontalAlign="Center" CellPadding="4" ForeColor="#333333" 
                    GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>There are no new print jobs with that ID.</EmptyDataTemplate>
                    <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                </asp:DetailsView>
                <br />
                <asp:Label ID="Label_EstPromiseDate" runat="server" ></asp:Label>
            </td>
            <td class="style2">
            <asp:GridView ID="GV_info" runat="server" Height="50px" Width="90%" 
                HorizontalAlign="Center" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="NameKey" HeaderText="Attribute Name" />
                    <asp:BoundField DataField="DataValue" HeaderText="Value" />
                </Columns>
            </asp:GridView>
            </td>
        </tr>
    </table>
<br />
<table style="width: 97%;">
    <tr>
        <td class="style2"><h2>Asset Files:</h2></td>
    </tr>
    </table>
    <br />
    <table style="width: 97%;">
    <tr>
        <td>
                <asp:GridView ID="GV_JobAssets" runat="server" 
                    onpageindexchanging="GV_JobAssets_PageIndexChanging" AllowPaging="True" 
                    PageSize="5" HorizontalAlign="Center" Width="90%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Hyperlink ID="AssetLink"
                                runat="server"
                                Target="_blank"
                                NavigateUrl='<%#WebUtils.ResolveVirtualPath(Eval("Filepath").ToString())%>'>View</asp:Hyperlink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>There are no Asset files for the selected job, or there is no job selected.</EmptyDataTemplate>
                </asp:GridView>
        </td>
    </tr>
    </table>
    <br />
<table style="width: 97%;">
    <tr>
        <td class="style2"><h2>Accept and Quote Job:</h2></td>
    </tr>
    </table>
<br />
<table style="width: 97%;">
        <tr>
            <td class="style1">
            </td>
            <td>
            Enter Quote Amount: $<asp:TextBox ID="QuoteTextbox" runat="server" Height="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td class="style1">
            </td>
            <td>

                <asp:Button ID="SubmitQuote_Button" runat="server" 
                    Text="Submit" onclick="SubmitQuote_Button_Click" />
                <br />
                Note: This will e-mail the customer.</td>
        </tr>
    </table>
    <h2>OR</h2>
    <br />
<table style="width: 97%;">
    <tr>
        <td class="style2"><h2>Request More Information:</h2></td>
    </tr>
    </table>
<table style="width: 97%;">
        <tr>
        <td class="style1">
            </td>
            <td>
                Enter the message you would like to send:</td>
        </tr>
        <tr>
        <td class="style1">
            </td>
            <td><asp:TextBox ID="MoreInfoTextbox" runat="server" Height="250px" 
                    TextMode="MultiLine" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td class="style1">
            </td>
            <td>

                <asp:Button ID="RequestInfo_Button" runat="server" Text="Submit" 
                    onclick="RequestInfo_Button_Click" />
                <br />
                Note: This will e-mail the customer.</td>
        </tr>
    </table>
</asp:Content>