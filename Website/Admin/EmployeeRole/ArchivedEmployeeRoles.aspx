<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedEmployeeRoles.aspx.cs" Inherits="Admin_EmployeeRole_ArchivedEmployeeRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="ArchivedEmployeeRoles" runat="server" AllowPaging="True" 
        DataKeyNames="RoleID" 
        onpageindexchanging="ArchivedEmployeeRoles_PageIndexChanging" 
        onrowdeleting="ArchivedEmployeeRoles_RowDeleting" 
        AutoGenerateColumns="False">
        <Columns>
            <asp:CommandField DeleteText="Reactivate" ShowDeleteButton="True" />
            <asp:BoundField DataField="RoleName" HeaderText="Role Name" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are no archived employee roles."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>

