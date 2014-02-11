<%@ Page Title="" Language="C#" MasterPageFile="~/Customer/Customer.master" AutoEventWireup="true" CodeFile="AccountPage.aspx.cs" Inherits="Customer_AccountPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
<script type="text/javascript">
    $(document).ready(function () {
        //Ensures only numbers are input
        $('input[id$="PhoneTextbox"]').keydown(function (event) {
            if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
                event.preventDefault();
            }
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>Account Maintenance</h1>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            </td>
            <td>
                <asp:Label ID="UsernameLabel" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="First Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="FirstNameTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="LastNameTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Address"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="AddressTextbox" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="City"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="CityTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Province"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="ProvinceTextbox" runat="server" Width="40px" MaxLength="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Postal Code"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PostalTextbox" runat="server" Width="90px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label8" runat="server" Text="Phone Number"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="PhoneTextbox" runat="server" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label9" runat="server" Text="Old Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="OldPassTextbox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label10" runat="server" Text="New Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="NewPassTextbox" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label11" runat="server" Text="Email"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="EmailTextbox" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
    <br />
    <asp:LinkButton ID="CancelButton" runat="server" onclick="CancelButton_Click">Cancel</asp:LinkButton>
    <asp:LinkButton ID="UpdateButton" runat="server" onclick="UpdateButton_Click">Save</asp:LinkButton>
</asp:Content>