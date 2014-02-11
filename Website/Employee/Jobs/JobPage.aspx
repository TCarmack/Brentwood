<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="JobPage.aspx.cs" Inherits="Employee_Jobs_JobPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Job Page</h2>
    <asp:Panel ID="ControlsPanel" runat="server"></asp:Panel>
    <asp:Panel ID="QuotePanel" runat="server">
        <asp:TextBox ID="QuoteTextbox" runat="server"></asp:TextBox>
        <asp:LinkButton ID="QuoteJob" runat="server" onclick="QuoteJob_Click">Quote Job</asp:LinkButton>
    </asp:Panel>
    <asp:LinkButton ID="UpdateButton" runat="server" onclick="UpdateButton_Click">Update</asp:LinkButton>
    <br />
    <asp:Label ID="FormMessage" runat="server" />
    <h2>Job Statuses</h2>
    <asp:GridView ID="StatusesList" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="ChangedOn" 
                DataFormatString="{0:dd MMM yyyy hh:mm tt}" HeaderText="Last Changed On" />
            <asp:BoundField DataField="User" HeaderText="Changed By" />
            <asp:BoundField DataField="CompletedOn" 
                DataFormatString="{0:dd MMM yyyy hh:mm tt}" HeaderText="Completed On" />
        </Columns>
    </asp:GridView>
    <asp:DropDownList ID="JobStatusDropDown" runat="server" DataTextField="Name" 
        DataValueField="JobStatusID">
    </asp:DropDownList>
    <asp:LinkButton ID="ChangeStatusButton" runat="server" Text="Change" 
        onclick="ChangeStatusButton_Click" PostBackUrl="~/Employee/Jobs/JobPage.aspx" />
    <asp:LinkButton ID="FinishButton" runat="server" onclick="FinishButton_Click">Set Job as Complete</asp:LinkButton>
</asp:Content>