<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedJobTypes.aspx.cs" Inherits="Admin_ArchivedJobTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="JobTypesGridView" runat="server" AllowPaging="True" 
    AutoGenerateColumns="False" DataKeyNames="JobTypeID" 
    onpageindexchanging="JobTypesGridView_PageIndexChanging" 
        onselectedindexchanging="JobTypesGridView_SelectedIndexChanging">
    <Columns>
        <asp:CommandField SelectText="Reactivate" ShowSelectButton="True" />
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="EstimatedTimeToComplete" 
            HeaderText="Estimated Time To Complete" />
        <asp:BoundField DataField="JobTypeDescription" HeaderText="Description" />
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="Label1" runat="server" Text="There are no archived job types."></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>
    <asp:Label ID="FormMessage" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>