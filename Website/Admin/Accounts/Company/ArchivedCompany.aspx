<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedCompany.aspx.cs" Inherits="Admin_Accounts_Company_ArchivedCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Archived Companies</h1>
    <asp:GridView ID="CompaniesGridView" runat="server" AllowPaging="True" 
        onselectedindexchanging="CompaniesGridView_SelectedIndexChanging" 
        AutoGenerateColumns="False" DataKeyNames="CompanyID">
        <Columns>
            <asp:CommandField ShowSelectButton="True" DeleteText="Archive" 
                SelectText="UnArchive" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="CompanyAddress" HeaderText="CompanyAddress" 
                SortExpression="CompanyAddress" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="Province" HeaderText="Province" 
                SortExpression="Province" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber" 
                SortExpression="PhoneNumber" />
            <asp:BoundField DataField="Website" HeaderText="Website" 
                SortExpression="Website" />
            <asp:CheckBoxField DataField="Flagged" HeaderText="Flagged" 
                SortExpression="Flagged" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are no archived companies in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server" />
</asp:Content>