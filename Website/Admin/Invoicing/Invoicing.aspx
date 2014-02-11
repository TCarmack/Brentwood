<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="Invoicing.aspx.cs" Inherits="Admin_Invoicing_Invoicing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>Outstanding Invoices</h1>
    <p>
        <asp:GridView ID="InvoiceView" runat="server" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
            style="text-align: center" DataKeyNames="JobID" 
            onpageindexchanging="InvoiceView_PageIndexChanging" 
            onselectedindexchanging="InvoiceView_SelectedIndexChanging">
            <Columns>
                <asp:CommandField SelectText="Paid" ShowSelectButton="True" />
                <asp:BoundField HeaderText="Customer Name" DataField="Customer" />
                <asp:BoundField HeaderText="Job Name" DataField="Name" />
                <asp:BoundField HeaderText="Job ID" DataField="JobID" />
                <asp:BoundField HeaderText="Delivery or Pickup" DataField="DeliveryOrPickup" />
                <asp:BoundField HeaderText="Invoiced Date" DataField="InvoicedDate" 
                    DataFormatString="{0: dd MMM yyyy hh:mm tt}" />
                <asp:BoundField DataField="Days_Past_Due" HeaderText="Days Past Due" />
                <asp:BoundField DataField="Last_Messaged_On" HeaderText="Last Messaged On" 
                    DataFormatString="{0: dd MMM yyyy hh:mm tt}" />
                <asp:TemplateField HeaderText="Send E-Mail">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="EmailCheckBox" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text="No Invoices to Display"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Button ID="SelectAll" runat="server" onclick="SelectAll_Click" 
            Text="Select All" />
        <asp:Button ID="UnselectAll" runat="server" onclick="UnselectAll_Click" 
            Text="Unselect All" />
    </p>
    <p>
        <asp:Button ID="SendEmail" runat="server" onclick="SendEmail_Click" 
            Text="Send E-Mails" />
       
    </p>
 <asp:Label ID="MessageLabel" runat="server"></asp:Label>
</asp:Content>