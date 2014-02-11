<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.master" AutoEventWireup="true" CodeFile="JobPage.aspx.cs" Inherits="Customer_JobPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <h2>Job Page</h2>
    <asp:Panel ID="ControlsPanel" runat="server"></asp:Panel>
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
    <h2>Job Proofs</h2>
    <asp:GridView ID="GV_JobProofs" runat="server" 
                    onpageindexchanging="GV_JobProofs_PageIndexChanging" AllowPaging="True" 
                    PageSize="5" HorizontalAlign="Center" AutoGenerateColumns="False" 
                    AllowSorting="True" Width="90%">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Hyperlink ID="ProofLink"
                                runat="server"
                                Target="_blank"
                                NavigateUrl='<%#WebUtils.ResolveVirtualPath(Eval("Filepath").ToString())%>'>View</asp:Hyperlink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JobProofID" HeaderText="ID" />
                        <asp:BoundField DataField="JobID" HeaderText="Job ID" />
                        <asp:BoundField DataField="Filepath" HeaderText="File Path" />
                        <asp:BoundField DataField="DateCreated" DataFormatString="{0:dd/MMM/yyyy}" 
                            HeaderText="Date Created" SortExpression="DateCreated" />
                        <asp:TemplateField HeaderText="Status" SortExpression="Active">
                            <ItemTemplate>
                            <%#(Boolean.Parse(Eval("Active").ToString())) ? "Active" : "Declined" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>No proof files have been uploaded yet.</EmptyDataTemplate>
                </asp:GridView>
    <asp:LinkButton ID="AcceptQuoteButton" runat="server" Visible="False" 
            onclick="AcceptQuoteButton_Click">Accept Quote</asp:LinkButton>
    <asp:LinkButton ID="AcceptDesignButton" runat="server" Visible="False" 
            onclick="AcceptDesignButton_Click">Accept Designs</asp:LinkButton>
        <asp:LinkButton ID="ReorderButton" runat="server" onclick="ReorderButton_Click">Reorder Job</asp:LinkButton>
    </asp:Content>