<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CreateEmployeeAccount.aspx.cs" Inherits="Admin_CreateEmployeeAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<!--Jack-->
    <style type="text/css">
        .style1
        {
            width: 49%;
        }
        .style2
        {
            width: 138px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>New Employee Account</h1>
    <br />
    <table class="style1">
        <tr>
            <td class="style2">
                <asp:Label ID="Label11" runat="server" Text="First Name"></asp:Label>
            </td>
            <td>
    <asp:TextBox ID="FirstNameTextbox" runat="server" Width="120px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="First Name is required!" 
                    ControlToValidate="FirstNameTextbox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label12" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td>
    <asp:TextBox ID="LastNameTextbox" runat="server" Width="120px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Last Name is required!" ControlToValidate="LastNameTextbox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label14" runat="server" Text="Email"></asp:Label>
            </td>
            <td>
    <asp:TextBox ID="EmailTextbox" runat="server" Width="120px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Email is required!" ControlToValidate="EmailTextbox"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="EmailTextbox" ErrorMessage="RegularExpressionValidator" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label15" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
    <asp:TextBox ID="UsernameTextbox" runat="server" Width="120px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="Username is required!" ControlToValidate="UsernameTextbox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label16" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
    <asp:TextBox ID="PasswordTextbox" runat="server" Width="120px" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ErrorMessage="Password is required!" ControlToValidate="PasswordTextbox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label17" runat="server" Text="Confirm Password"></asp:Label>
            </td>
            <td>
    <asp:TextBox ID="ConfirmPasswordTextbox" runat="server" Width="120px" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="PasswordTextbox" ControlToValidate="ConfirmPasswordTextbox" 
                    ErrorMessage="Passwords must match!"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ErrorMessage="Confirm Password is required!" 
                    ControlToValidate="ConfirmPasswordTextbox"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style2">
    <asp:Label ID="Label9" runat="server" Text="Admin Status"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="AdminCheckbox" runat="server" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <h6>Employee Roles (Check All That Apply)</h6>
    <asp:CheckBoxList ID="EmployeeStatus" runat="server" Height="25px" 
        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="431px" 
        DataSourceID="EmployeeRoleDS" DataTextField="RoleName" DataValueField="RoleID">
    </asp:CheckBoxList>
    <asp:ObjectDataSource ID="EmployeeRoleDS" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="ListEmployeeRoles" 
        TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>
    <br />
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
    <br />
&nbsp;
<asp:LinkButton ID="ConfirmButton" runat="server" onclick="ConfirmButton_Click">Confirm</asp:LinkButton>
<asp:LinkButton ID="CancelButton" runat="server" onclick="CancelBtn_Click" 
        CausesValidation="False">Cancel</asp:LinkButton>
</asp:Content>
