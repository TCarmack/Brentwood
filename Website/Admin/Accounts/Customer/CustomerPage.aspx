<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CustomerPage.aspx.cs" Inherits="Admin_Accounts_Customer_Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
    <script type="text/javascript" src="../../../Scripts/Utils.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Customer Maintenance</h1>
    <asp:DetailsView ID="CustomerView" runat="server" AutoGenerateRows="False" 
        DataKeyNames="UserId" Height="50px" Width="125px" 
    AllowPaging="True" onmodechanging="CustomerView_ModeChanging" 
    onpageindexchanging="CustomerView_PageIndexChanging" 
        ondatabound="CustomerView_DataBound" >
        <EmptyDataTemplate>
            <asp:Label ID="Label5" runat="server" Text="No Data to Display"></asp:Label>
        </EmptyDataTemplate>
        <Fields>
            <asp:BoundField DataField="UserName" HeaderText="Username" />
            <asp:BoundField DataField="LastActivityDate" 
                DataFormatString="{0:MMM dd yyyy HH:mm}" HeaderText="Last Active" 
                ReadOnly="True" />
            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <asp:TextBox ID="FirstNameTextbox" runat="server" 
                        Text='<%# Bind("FirstName") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:TextBox ID="LastNameTextbox" runat="server"
                    Text='<%# Bind("LastName") %>' ></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Address">
                <ItemTemplate>
                    <asp:TextBox ID="AddressTextbox" runat="server"
                    Text=' <%# Bind("CustomerAddress") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City">
                <ItemTemplate>
                    <asp:TextBox ID="CityTextbox" runat="server"
                        Text=' <%# Bind("City") %> '></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Province">
                <ItemTemplate>
                    <asp:TextBox ID="ProvinceTextbox" runat="server"
                        Text=' <%# Bind("Province") %>' Width="35px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Postal Code">
                <ItemTemplate>
                    <asp:TextBox ID="PostalCodeTextbox" runat="server" 
                    Text='<%# Bind("PostalCode") %>' MaxLength="6"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone">
                <ItemTemplate>
                    <asp:TextBox ID="PhoneNumberTextbox" runat="server" 
                    Text=' <%# Bind("PhoneNumber") %>' MaxLength="11"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:TextBox ID="EmailTextbox" runat="server" Text='<%# Bind("Email") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Company">
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="CompanyList" runat="server" AppendDataBoundItems="True" 
                        DataTextField="Name" DataValueField="CompanyID">
                        <asp:ListItem Value="-1">No Company</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Is Admin">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsAdmin") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("IsAdmin") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="IsAdminCheckbox" runat="server" 
                        Checked='<%# Eval("IsAdmin") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Approved">
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="ApprovedCheckbox" runat="server" 
                        Checked='<%# Eval("Approved") %>' />
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
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
    <br />
    <asp:LinkButton ID="CancelButton" runat="server" onclick="CancelButton_Click">Cancel</asp:LinkButton>
    <asp:LinkButton ID="UpdateButton" runat="server" onclick="UpdateButton_Click">Save</asp:LinkButton>
    <asp:LinkButton ID="DeleteButton" runat="server" onclick="LinkButton1_Click">Inactivate</asp:LinkButton>
    <asp:LinkButton ID="ApproveButton" runat="server" onclick="ApproveButton_Click">Approve Customer</asp:LinkButton>
    <br />
</asp:Content>

