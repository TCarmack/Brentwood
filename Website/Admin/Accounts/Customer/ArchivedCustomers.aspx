<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedCustomers.aspx.cs" Inherits="Admin_Accounts_Customer_ArchivedCustomers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Archived Customers</h1>
    <asp:GridView ID="CustomersGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="UserId" 
        onpageindexchanging="CustomersGridView_PageIndexChanging" 
        onselectedindexchanging="CustomersGridView_SelectedIndexChanging">
        <Columns>
            <asp:CommandField SelectText="Reactivate" ShowSelectButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="LastActivityDate" 
                DataFormatString="{0:MMM dd yyyy HH:mm}" HeaderText="Last Active" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="Name" HeaderText="Company" />
            <asp:CheckBoxField DataField="Approved" Text="Approved" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are no archived customers in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>

