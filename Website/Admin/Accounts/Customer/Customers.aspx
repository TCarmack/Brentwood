<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Customers.aspx.cs" Inherits="Admin_Accounts_Customer_Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Customers</h1>
    <asp:TextBox ID="SearchTextBox" runat="server"></asp:TextBox>
    <asp:LinkButton ID="SearchButton" runat="server">Search by Name</asp:LinkButton>
    <asp:GridView ID="CustomersGridView" runat="server" 
    AutoGenerateColumns="False" DataKeyNames="UserId" 
        AllowPaging="True" style="margin-right: 1px" 
    onpageindexchanging="CustomersGridView_PageIndexChanging" 
    onselectedindexchanging="CustomersGridView_SelectedIndexChanging" 
        onrowdeleting="CustomersGridView_RowDeleting">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:CommandField DeleteText="Archive" ShowDeleteButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="Username" 
                SortExpression="UserName" />
            <asp:BoundField DataField="LastActivityDate" HeaderText="Last Active" 
                SortExpression="LastActivityDate" 
                DataFormatString="{0: MMM dd yyyy HH:mm}" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" 
                SortExpression="FirstName" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" 
                SortExpression="LastName" />
            <asp:BoundField DataField="Name" HeaderText="Company" />
            <asp:TemplateField HeaderText="Approved">
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("Approved") %>' 
                        Enabled="False" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are currently no active customers in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>

