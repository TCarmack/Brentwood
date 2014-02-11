<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="JobsByCustomer.aspx.cs" Inherits="Admin_Jobs_JobsByCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>Select a Customer:</h1>
<asp:GridView ID="Customers" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="UserId" 
        onpageindexchanging="Customers_PageIndexChanging" 
        onselectedindexchanging="Customers_SelectedIndexChanging">
    <Columns>
        <asp:CommandField ShowSelectButton="True" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" 
            SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" 
            SortExpression="LastName" />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" 
            SortExpression="PhoneNumber" />
        <asp:BoundField DataField="Name" HeaderText="Company" SortExpression="Name" />
        <asp:CheckBoxField DataField="Approved" HeaderText="Approved" 
            SortExpression="Approved" />
        <asp:BoundField DataField="LastActivityDate" 
            DataFormatString="{0:MMM dd yyyy HH:mm}" HeaderText="Last Active" 
            SortExpression="LastActivityDate" />
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
        <asp:CheckBoxField DataField="IsAdmin" HeaderText="IsAdmin" 
            SortExpression="IsAdmin" />
    </Columns>
    </asp:GridView>

<h1>Customer's Jobs:</h1>
<asp:GridView ID="Jobs" runat="server" AutoGenerateColumns="False" 
        onpageindexchanging="Jobs_PageIndexChanging" 
        onselectedindexchanging="Jobs_SelectedIndexChanging" DataKeyNames="JobID">
    <Columns>
        <asp:CommandField SelectText="Reorder" ShowSelectButton="True" />
        <asp:BoundField DataField="Name" HeaderText="Job Type" 
            SortExpression="JobTypeID" />
        <asp:BoundField DataField="SpecialInstructions" 
            HeaderText="SpecialInstructions" SortExpression="SpecialInstructions" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
            SortExpression="Quantity" />
        <asp:BoundField DataField="DeliveryOrPickup" HeaderText="DeliveryOrPickup" 
            SortExpression="DeliveryOrPickup" />
        <asp:BoundField DataField="PromiseDate" HeaderText="PromiseDate" 
            SortExpression="PromiseDate" DataFormatString="{0:dd MMM yyyy hh:mm tt}" />
        <asp:BoundField DataField="StartDate" HeaderText="StartDate" 
            SortExpression="StartDate" DataFormatString="{0:dd MMM yyyy hh:mm tt}" />
        <asp:BoundField DataField="InvoicedDate" HeaderText="InvoicedDate" 
            SortExpression="InvoicedDate" 
            DataFormatString="{0:dd MMM yyyy hh:mm tt}" />
        <asp:BoundField DataField="Paid" HeaderText="Paid" SortExpression="Paid" />
        <asp:BoundField DataField="QuoteAmount" HeaderText="QuoteAmount" 
            SortExpression="QuoteAmount" DataFormatString="{0:C}" />
    </Columns>
    </asp:GridView>

<asp:Label ID="FormMessage" runat="server" />
</asp:Content>