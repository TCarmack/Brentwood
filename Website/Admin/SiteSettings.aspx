<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="SiteSettings.aspx.cs" Inherits="Admin_SiteSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h1>App Settings</h1>
<h3>Warning: Ensure that these settings are correct before saving. Incorrect settings can cause undesirable behavior in the application. 
    Hover over textboxes to see a description.</h3>
    <p>Note: Text in square brackets ("[" and "]") denote program variables. If the setting item has these, ensure that they remain, though you may reorder them in the text.</p>
    <asp:Panel ID="ControlsPanel" runat="server">
    </asp:Panel>
<asp:Label ID="FormMessage" runat="server" />
    <br />
<asp:Button ID="SubmitButton" runat="server" Text="Save" OnClick="SubmitButton_Click" />
    <asp:Button ID="CreateBackupButton" runat="server" 
        onclick="CreateBackupButton_Click" Text="Create Backup" />
    <asp:Button ID="BackupButton" runat="server" onclick="BackupButton_Click" 
        Text="Restore From Backup" />
    <asp:Button ID="MasterButton" runat="server" 
        Text="Restore From Master" onclick="MasterBackup_Click" />
</asp:Content>