<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="UserAccounts.aspx.cs" Inherits="Admin_Accounts_UserAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>All User Accounts</h1>
<asp:GridView ID="UsersView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="UserId" 
        onpageindexchanging="UsersView_PageIndexChanging" 
        onselectedindexchanging="UsersView_SelectedIndexChanging">
    <Columns>
        <asp:CommandField ShowSelectButton="True" />
        <asp:BoundField DataField="UserName" HeaderText="Username" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
        <asp:BoundField DataField="RoleName" HeaderText="Role" />
        <asp:TemplateField HeaderText="Archived">
            <EditItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" 
                    Checked='<%# Bind("[!IsApproved]") %>' />
            </EditItemTemplate>
            <ItemTemplate>
                <asp:CheckBox ID="CheckBox1" runat="server" 
                    Checked='<%# ((bool)DataBinder.Eval(Container.DataItem, "IsApproved")) == true ? false : true %>' Enabled="False" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Label ID="FormMessage" runat="server">
</asp:Label>
</asp:Content>