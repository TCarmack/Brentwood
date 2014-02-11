<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ArchivedJobStatuses.aspx.cs" Inherits="Admin_JobStatus_ArchivedJobStatuses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Archived Job Status</h1>
    <asp:GridView ID="JobStatusesGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="JobStatusID" 
        onpageindexchanging="JobStatusesGridView_PageIndexChanging" 
        onselectedindexchanging="JobStatusesGridView_SelectedIndexChanging">
        <Columns>
            <asp:CommandField SelectText="Reactivate" ShowSelectButton="True" />
            <asp:BoundField DataField="JobStatusID" HeaderText="JobStatusID" 
                SortExpression="JobStatusID" Visible="False" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are currently no archived job statuses in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>