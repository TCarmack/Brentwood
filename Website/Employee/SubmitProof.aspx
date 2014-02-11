<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="SubmitProof.aspx.cs" Inherits="Employee_SubmitProof" %>

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
        .style3
        {
            width: 50%;
        }
        .style4
        {
        width: 200px;
        text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Proof Job</h1>
<h2>Search:</h2>
<table style="width: 97%;">
        <tr>
            <td class="style4">By Customer Name:</td>
            <td>
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
<h2>Select a Job (IN DESIGN):</h2>
    <p>
        <asp:Label ID="Label_SubmitProof" runat="server" Text=""></asp:Label>
    </p>
<br />
    <table style="width: 97%;">
        <tr>
            <td>
                <asp:GridView ID="GV_InDesign" runat="server" 
                    onselectedindexchanged="GV_InDesign_SelectedIndexChanged" 
                    AllowPaging="True" onpageindexchanging="GV_InDesign_PageIndexChanging" 
                    AutoGenerateColumns="False" HorizontalAlign="Center" AllowSorting="True" 
                    onsorting="GV_InDesign_Sorting" Width="90%">
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
                        <asp:BoundField DataField="StartDate" DataFormatString="{0:dd/MMM/yyyy}" 
                            HeaderText="Start Date" SortExpression="StartDate" />
                        <asp:BoundField DataField="PromiseDate" DataFormatString="{0:dd/MMM/yyyy}" 
                            HeaderText="Promised By" SortExpression="PromiseDate" />
                        <asp:TemplateField HeaderText="Time Left" SortExpression="TimeLeft">
                            <ItemTemplate>
                            <%# (DateTime.Parse(Eval("PromiseDate").ToString()) - DateTime.Now).Days + " Days " + 
                                (DateTime.Parse(Eval("PromiseDate").ToString()) - DateTime.Now).Hours + " Hrs"%>
                            </ItemTemplate>
                            <ItemStyle Wrap="False" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustomerEmail" HeaderText="Email" />
                    </Columns>
                    <EmptyDataTemplate>There are no jobs 'In Design' or no jobs for that search value.</EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="width: 97%;">
    <tr>
        <td class="style3"><h2>Review Job Details:</h2></td>
        <td><h2>Attributes:</h2>
        </td>
    </tr>
    </table>
<br />
    <table style="width: 97%;">
        <tr>
            <td class="style2">
                <asp:DetailsView ID="DV_InDesign" runat="server" Height="50px" Width="90%" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" 
                    HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" />
                    <CommandRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FieldHeaderStyle BackColor="#DEE8F5" Font-Bold="True" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>No job selected.</EmptyDataTemplate>
                    <RowStyle BackColor="#EFF3FB" />
                </asp:DetailsView>
            </td>
            <td>
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
<h2>Asset Files: </h2>
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
<h2>Proof Files: </h2>
<br />
    <table style="width: 97%;">
        <tr>
            <td>

                <asp:GridView ID="GV_JobProofs" runat="server" 
                    onpageindexchanging="GV_JobProofs_PageIndexChanging" AllowPaging="True" 
                    PageSize="5" HorizontalAlign="Center" AutoGenerateColumns="False" 
                    AllowSorting="True" onsorting="GV_JobProofs_Sorting" Width="90%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Hyperlink ID="ProofLink"
                                runat="server"
                                Target="_blank"
                                NavigateUrl='<%#WebUtils.ResolveVirtualPath(Eval("Filepath").ToString())%>'>View</asp:Hyperlink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JobProofID" HeaderText="ID" />
                        <asp:BoundField DataField="JobID" HeaderText="Job ID" />
                        <asp:BoundField DataField="Filepath" HeaderText="File Path" />
                        <asp:BoundField DataField="DateCreated" DataFormatString="{0:dd/MMM/yyyy}" 
                            HeaderText="Date Created" SortExpression="DateCreated" />
                        <asp:TemplateField HeaderText="Status" SortExpression="Active">
                            <ItemTemplate>
                            <%#(Boolean.Parse(Eval("Active").ToString())) ? "Active" : "Declined" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>There are no Proof files for the selected job, or there is no job selected.</EmptyDataTemplate>
                </asp:GridView>
            </td>
        </tr>
    </table>
<h2>Add Proof File: </h2>
<br />
    <table style="width: 97%;">
        <tr>
            <td class="style1">
            </td>
            <td>
                <asp:FileUpload ID="ProofFileUpload" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td>
                <asp:Button ID="SubmitProof" runat="server" Text="Submit" 
                    onclick="SubmitProof_Click" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
<h2>Notify Customer: </h2>
<br />
    <table style="width: 97%;">
        <tr>
            <td class="style1">
            </td>
            <td>
                <asp:Button ID="EmailButton" runat="server" Text="Send Notification" 
                    onclick="EmailButton_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td>
                Note: This will E-mail the customer, and enable them to view the job-proof files.
            </td>
        </tr>
    </table>
</asp:Content>