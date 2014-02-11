<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedEmployees.aspx.cs" Inherits="Admin_Accounts_Employee_ArchivedEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Archived Employees</h1>
    <asp:GridView ID="ArchivedEmployees" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="UserId" 
        onpageindexchanging="ArchivedEmployees_PageIndexChanging" 
        onselectedindexchanged="ArchivedEmployees_SelectedIndexChanged">
        <Columns>
            <asp:CommandField SelectText="Unarchive" ShowSelectButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="LastActivityDate" 
                DataFormatString="{0: MMM dd yyyy HH:mm}" 
                HeaderText="Last Active" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are currently no archived employees in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>