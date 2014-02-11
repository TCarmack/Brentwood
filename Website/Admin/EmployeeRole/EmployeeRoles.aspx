<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EmployeeRoles.aspx.cs" Inherits="Admin_EmployeeRole_EmployeeRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:GridView ID="EmployeeRoles" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="RoleID" 
        onpageindexchanging="EmployeeRoles_PageIndexChanging" 
        onrowcancelingedit="EmployeeRoles_RowCancelingEdit" 
        onrowdeleting="EmployeeRoles_RowDeleting" 
        onrowediting="EmployeeRoles_RowEditing" 
        onrowupdating="EmployeeRoles_RowUpdating">
    <Columns>
        <asp:CommandField DeleteText="Archive" ShowDeleteButton="True" 
            ShowEditButton="True" />
        <asp:BoundField DataField="RoleName" HeaderText="Role Name" />
    </Columns>
    </asp:GridView>
<asp:Label ID="FormMessage" runat="server"></asp:Label>
    <br />
    <asp:TextBox ID="RoleTextBox" runat="server"></asp:TextBox>
    <asp:LinkButton ID="NewRole" runat="server" onclick="NewRole_Click">Add New Role</asp:LinkButton>
</asp:Content>