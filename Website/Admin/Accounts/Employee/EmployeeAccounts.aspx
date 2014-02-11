<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EmployeeAccounts.aspx.cs" Inherits="Admin_Accounts_Employee_EmployeeAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Employee Accounts</h1>
    <asp:GridView ID="EmployeesGridView" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" DataKeyNames="UserId" 
    onpageindexchanging="EmployeesGridView_PageIndexChanging" 
    onrowdeleting="EmployeesGridView_RowDeleting" 
    onselectedindexchanging="EmployeesGridView_SelectedIndexChanging">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:CommandField DeleteText="Archive" ShowDeleteButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="LastActivityDate" HeaderText="Last Active" 
                DataFormatString="{0: MMM dd yyyy HH:mm}" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are no employees currently in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>