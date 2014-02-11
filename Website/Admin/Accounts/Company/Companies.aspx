<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Companies.aspx.cs" Inherits="Admin_Accounts_Company_Companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Company Account Management</h1>
    <asp:GridView ID="CompaniesGridView" runat="server" AllowPaging="True" 
        onselectedindexchanging="CompaniesGridView_SelectedIndexChanging" 
        AutoGenerateColumns="False" DataKeyNames="CompanyID" 
        onrowdeleting="CompaniesGridView_RowDeleting">
        <Columns>
            <asp:CommandField ShowSelectButton="True" DeleteText="Archive" 
                ShowDeleteButton="True" />
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
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server" />
</asp:Content>