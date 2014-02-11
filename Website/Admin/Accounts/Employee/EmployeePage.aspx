<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="EmployeePage.aspx.cs" Inherits="Admin_Accounts_Employee_EmployeePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Employee Maintenance</h1>
    <asp:DetailsView ID="EmployeeView" runat="server" AllowPaging="True" 
        AutoGenerateRows="False" DataKeyNames="UserId" 
        onpageindexchanging="EmployeeView_PageIndexChanging" 
        ondatabound="EmployeeView_DataBound">
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" 
                Text="There are no employees in the database."></asp:Label>
        </EmptyDataTemplate>
        <Fields>
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="LastActivityDate" HeaderText="Last Active" />
            <asp:TemplateField HeaderText="Email">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="EmailTextbox" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="First Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FirstName") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="FirstNameTextbox" runat="server" 
                        Text='<%# Bind("FirstName") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:TextBox ID="LastNameTextbox" runat="server" Text='<%# Bind("LastName") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Old Password">
                <ItemTemplate>
                    <asp:TextBox ID="OldPassTextbox" runat="server" TextMode="Password"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="New Password">
                <ItemTemplate>
                    <asp:TextBox ID="NewPassTextbox" runat="server" TextMode="Password"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <asp:CheckBox ID="AdminCheckbox" runat="server" AutoPostBack="True" 
        oncheckedchanged="AdminCheckbox_CheckedChanged" Text="Admin" />
    <asp:CheckBoxList ID="RolesList" runat="server" DataTextField="RoleName" 
        DataValueField="RoleID">
    </asp:CheckBoxList>
    <br />
    <asp:Label ID="FormMessage" runat="server">
    </asp:Label>
    <br />
    <asp:LinkButton ID="CancelButton" runat="server" onclick="CancelButton_Click">Cancel</asp:LinkButton>
    <asp:LinkButton ID="SaveButton" runat="server" onclick="SaveButton_Click">Save</asp:LinkButton>
    <asp:LinkButton ID="ArchiveButton" runat="server" onclick="ArchiveButton_Click">Archive</asp:LinkButton>
</asp:Content>