﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="NewCustomer.aspx.cs" Inherits="Admin_Accounts_Customer_NewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
    <script type="text/javascript">
    $(document).ready(function () {
        //Ensures only numbers are input
        $('input[id$="PhoneNumberTextbox"]').keydown(function (event) {
            if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
                event.preventDefault();
            }
        });
    });

    function changeCase(element) {
        element.value = element.value.toUpperCase();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>New Customer Account</h1>
    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Username"></asp:Label></td>
            <td><asp:TextBox ID="UsernameTextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="UsernameTextbox" ErrorMessage="Username is required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Password"></asp:Label></td>
            <td><asp:TextBox ID="PasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="PasswordTextbox" ErrorMessage="Password is required!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label></td>
            <td><asp:TextBox ID="ConfirmPasswordTextbox" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="ConfirmPasswordTextbox" 
                    ErrorMessage="Password confirmation is required!"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="PasswordTextbox" ControlToValidate="ConfirmPasswordTextbox" 
                    ErrorMessage="Passwords must match!"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label4" runat="server" Text="Email Address"></asp:Label></td>
            <td><asp:TextBox ID="EmailAddressTextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="EmailAddressTextbox" ErrorMessage="Email is required!"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="EmailAddressTextbox" ErrorMessage="Must be a valid email!" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="First Name"></asp:Label>
            </td>
            <td><asp:TextBox ID="FirstNameTextbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td><asp:TextBox ID="LastNameTextbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Address"></asp:Label>
            </td>
            <td><asp:TextBox ID="AddressTextbox" runat="server"></asp:TextBox></td>
        </tr>
                <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="City"></asp:Label>
                    </td>
            <td><asp:TextBox ID="CityTextbox" runat="server"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Province"></asp:Label>
            </td>
            <td><asp:TextBox onblur="changeCase(this);" ID="ProvinceTextbox" runat="server" MaxLength="2" Width="33px"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="Postal Code"></asp:Label>
            </td>
            <td><asp:TextBox onblur="changeCase(this);" ID="PostalCodeTextbox" runat="server" MaxLength="6" Width="99px"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Phone Number"></asp:Label>
            </td>
            <td><asp:TextBox ID="PhoneNumberTextbox" runat="server" MaxLength="10"></asp:TextBox></td>
        </tr>

        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Company"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="CompanyDropdown" runat="server" 
                    AppendDataBoundItems="True" DataSourceID="CompaniesDS" DataTextField="Name" 
                    DataValueField="CompanyID">
                    <asp:ListItem Value="-1">No Company</asp:ListItem>
                </asp:DropDownList>
                <asp:ObjectDataSource ID="CompaniesDS" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="ListCompanies" 
                    TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Approved"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ApprovedCheckbox" runat="server" />
             </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
        onclick="CancelButton_Click">Cancel</asp:LinkButton>
    <asp:LinkButton ID="SubmitButton" runat="server" onclick="SubmitButton_Click">Submit</asp:LinkButton>
    <br />
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>

