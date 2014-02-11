<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="Customer_Admin_ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>Accounts Management</h1>
    <asp:GridView ID="UsersGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="UserId" 
        onpageindexchanging="UsersGridView_PageIndexChanging" 
        onselectedindexchanging="UsersGridView_SelectedIndexChanging">
        <Columns>
            <asp:CommandField SelectText="Deactivate" ShowSelectButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="LastActivityDate" DataFormatString="{0:dd MMM yyyy}" 
                HeaderText="Last Active" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="CustomerAddress" HeaderText="Address" />
            <asp:BoundField DataField="City" HeaderText="City" />
            <asp:BoundField DataField="Province" HeaderText="Province" />
            <asp:BoundField DataField="PostalCode" HeaderText="Postal Code" />
            <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>