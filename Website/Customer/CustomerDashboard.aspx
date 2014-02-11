<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.master" AutoEventWireup="true" CodeFile="CustomerDashboard.aspx.cs" Inherits="Customer_CustomerDashboard" %>
<%@ Reference Control="~/Controls/CustomerDashboardGrid.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
<script runat="server">
    protected override void OnInit(EventArgs e)
    {
        CustomerDashboardGrid dashboard = new CustomerDashboardGrid();
        dashboard.ID = "UserDashboard";
        Panel1.Controls.Add(dashboard);
        base.OnInit(e);
        dashboard.LoadJobs(User.Identity.Name);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>My Dashboard</h1>
        <asp:Panel ID="Invoices" runat="server">
            <h2>You Have Outstanding Invoices</h2>
            <asp:GridView ID="InvoiceView" runat="server" 
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                style="text-align: center" 
            onpageindexchanging="InvoiceView_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Job Name" DataField="Name" />
                    <asp:BoundField HeaderText="Job ID" DataField="JobID" />
                    <asp:BoundField HeaderText="Delivery or Pickup" DataField="DeliveryOrPickup" />
                    <asp:BoundField HeaderText="Invoiced Date" DataField="InvoicedDate" />
                    <asp:BoundField DataField="Days_Past_Due" HeaderText="Days Past Due" />
                    <asp:BoundField DataField="Last_Messaged_On" HeaderText="Last Messaged On" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    <h2>My Jobs</h2>
    <asp:Panel ID="Panel1" runat="server"></asp:Panel>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>