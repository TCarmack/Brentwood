<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="CompanyPage.aspx.cs" Inherits="Admin_Accounts_Company_CompanyPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>Company Management</h1>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NameTextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="NameTextbox" ErrorMessage="Company name is required." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AddressTextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="AddressTextbox" ErrorMessage="Address is required." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="City"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CityTextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="CityTextbox" ErrorMessage="City is required." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Province"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProvinceTextbox" runat="server" MaxLength="2" Width="30px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="ProvinceTextbox" ErrorMessage="Province is required." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Postal Code"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PostalCodeTextbox" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="PostalCodeTextbox" ErrorMessage="Postal Code is required." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="PostalCodeTextbox" 
                    ErrorMessage="Postal Code must be in the format 'A1A 1A1' or 'A1A-1A1'." 
                    ForeColor="Red" ValidationExpression="^([A-Za-z]\d[A-Za-z][- ]?\d[A-Za-z]\d)"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Phone Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PhoneNumberTextbox" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="PhoneNumberTextbox" ErrorMessage="Phone Number is required." 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Website"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="WebsiteTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="Label8" runat="server" Text="Approved"></asp:Label></td>
            <td>
                <asp:CheckBox ID="Approved" runat="server" />
            </td>
        </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ForeColor="Red" />
        <asp:LinkButton ID="CancelButton" runat="server" Text="Cancel"
        onclick="CancelButton_Click" />
        <asp:LinkButton ID="SubmitButton" runat="server" Text="Submit" 
        onclick="SubmitButton_Click" />
        <br />
        <asp:Label ID="FormMessage" runat="server" />
</asp:Content>