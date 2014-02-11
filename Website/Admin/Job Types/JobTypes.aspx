<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="JobTypes.aspx.cs" Inherits="Admin_JobTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Job Types</h2>
    <br />
    <asp:GridView ID="JobTypesList" runat="server" AutoGenerateColumns="False" 
    AllowPaging="True" AllowSorting="True" DataKeyNames="JobTypeID" 
    onrowdeleting="JobTypesList_RowDeleting" 
    onselectedindexchanging="JobTypesList_SelectedIndexChanging" 
        onpageindexchanging="JobTypesList_PageIndexChanging">
        <Columns>
            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
            <asp:CommandField DeleteText="Archive" ShowDeleteButton="True" />
            <asp:BoundField DataField="JobTypeID" HeaderText="Job Type ID" 
                Visible="False" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="EstimatedTimeToComplete" 
                HeaderText="Estimated Time To Complete" />
            <asp:BoundField DataField="JobTypeDescription" HeaderText="Description" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are currently no job types in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
    <br />
</asp:Content>

