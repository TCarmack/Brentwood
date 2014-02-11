<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="JobStatusesPage.aspx.cs" Inherits="Admin_JobStatuses_JobStatusesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Job Statuses</h1>
    <asp:GridView ID="JobStatusesGridView" runat="server" 
    AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="JobStatusID" 
        onpageindexchanging="JobStatusesGridView_PageIndexChanging" 
        onrowcancelingedit="JobStatusesGridView_RowCancelingEdit" 
        onrowdeleting="JobStatusesGridView_RowDeleting" 
        onrowediting="JobStatusesGridView_RowEditing" 
        onrowupdating="JobStatusesGridView_RowUpdating" 
        onrowcommand="JobStatusesGridView_RowCommand">
        <Columns>
            <asp:CommandField DeleteText="Archive" ShowDeleteButton="True" 
                ShowEditButton="True" />
            <asp:ButtonField CommandName="MoveItemUp" Text="Move Up" />
            <asp:ButtonField CommandName="MoveItemDown" Text="Move Down" />
            <asp:TemplateField HeaderText="JobStatusID" SortExpression="JobStatusID" 
                Visible="False">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("JobStatusID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="NameTextbox" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Associated Role">
                <EditItemTemplate>
                    <asp:DropDownList ID="EmployeeRoleDropdown" runat="server" 
                        DataSourceID="RolesDS" DataTextField="RoleName" DataValueField="RoleID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="RolesDS" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="ListEmployeeRoles" 
                        TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("RoleName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are currently no job statuses in the database."></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
    <h1>New Job Status</h1>
    <table>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="Name"></asp:Label></td>
            <td><asp:TextBox ID="NewStatusTextbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label5" runat="server" Text="Associated Role"></asp:Label></td>
            <td>
                <asp:DropDownList ID="RolesDropdown" runat="server" 
                    DataSourceID="EmployeeRolesDS" DataTextField="RoleName" DataValueField="RoleID">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="EmployeeRolesDS" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="ListEmployeeRoles" 
        TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>
    <br />
    <asp:LinkButton ID="SubmitButton" runat="server" Text="Submit" 
        onclick="SubmitButton_Click" 
        PostBackUrl="~/Admin/JobStatuses/JobStatusesPage.aspx"></asp:LinkButton>
</asp:Content>