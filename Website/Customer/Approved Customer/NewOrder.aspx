<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.master" AutoEventWireup="true" CodeFile="NewOrder.aspx.cs" Inherits="Customer_Approved_Customer_NewOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Step One: Choose a Job Type</h1>
    <asp:GridView ID="TypesGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="JobTypeID" DataSourceID="TypesDS" 
        onselectedindexchanging="GridView1_SelectedIndexChanging" ShowHeader="False">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Name" SortExpression="Name" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are currently no job types. Please contact Brentwood Printing for assistance."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="TypesDS" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="ListJobTypes" 
        TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>
    <asp:Label runat="server" ID="FormMessage"></asp:Label>
</asp:Content>