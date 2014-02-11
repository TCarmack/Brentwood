<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="JobTypePage.aspx.cs" Inherits="Admin_JobTypePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
    <script type="text/javascript">
        $(document).ready(function () {
            $('input[id$="EstDaysTextbox"]').blur(function () {
                if (isNaN($('input[id$="EstDaysTextbox"]').val())) {
                    $('input[id$="EstDaysTextbox"]').val('24');
                }
            });

            $('input[id$="EstHoursTextbox"]').blur(function () {
                if (isNaN($('input[id$="EstHoursTextbox"]').val())) {
                    $('input[id$="EstHoursTextbox"]').val('24');
                }

                if ($('input[id$="EstHoursTextbox"]').val() > 24) {
                    $('input[id$="EstHoursTextbox"]').val('24');
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="JobTypeNameLabel" runat="server" Text="Job Type Name"></asp:Label>
<asp:TextBox ID="JobTypeNameTextBox" runat="server" ValidationGroup="MainGroup"></asp:TextBox>
    <br />
    <asp:Label ID="EstimatedTimeToCompleteLabel" runat="server" Text="Estimated Time To Complete"></asp:Label>
    <asp:TextBox ID="EstDaysTextbox" runat="server" MaxLength="2" 
        Width="37px" ValidationGroup="MainGroup"></asp:TextBox>
    <asp:Label ID="Label1" runat="server" Text="Days"></asp:Label>
    <asp:TextBox ID="EstHoursTextbox" runat="server" MaxLength="2" Width="37px" 
    ValidationGroup="MainGroup"></asp:TextBox>
    <asp:Label ID="Label2" runat="server" Text="Hours"></asp:Label>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
    <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine" 
    ValidationGroup="MainGroup"></asp:TextBox>
    <br />
    <h2>Controls</h2>
    <asp:GridView ID="ControlsGridView" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="JobControlID" 
        onpageindexchanging="ControlsGridView_PageIndexChanging" 
        onrowdeleting="ControlsGridView_RowDeleting" 
        onrowcancelingedit="ControlsGridView_RowCancelingEdit" 
        onrowediting="ControlsGridView_RowEditing" 
        onrowupdating="ControlsGridView_RowUpdating">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />
            <asp:TemplateField HeaderText="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="NameTextbox" runat="server" Text='<%# Bind("ControlName") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="NameTextbox" ErrorMessage="Name is required!" 
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ControlName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Control Type">
                <EditItemTemplate>
                    <asp:DropDownList ID="ControlTypeList" runat="server" 
                        DataSourceID="ControlTypesDS" DataTextField="ControlTypeName" 
                        DataValueField="JobControlTypeID">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="ControlTypesDS" runat="server" 
                        OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="ListJobControlTypes" TypeName="BrentwoodPrinting.Data.Brentwood">
                    </asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("JobControlTypeName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label4" runat="server" Text="This job type has no controls!"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <h3>Add New Control</h3>
    <asp:DetailsView ID="ControlView" runat="server" Height="50px" 
    Width="125px" AutoGenerateRows="False" DefaultMode="Insert" 
    oniteminserting="ControlView_ItemInserting" 
    onmodechanging="ControlView_ModeChanging">
        <Fields>
            <asp:TemplateField HeaderText="Control Name">
                <EditItemTemplate>
                    <asp:TextBox ID="NewControlNameTextbox" runat="server" 
                        ValidationGroup="NewItemGroup"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="NewControlNameTextbox" ErrorMessage="Name is required." 
                        ForeColor="Red" ValidationGroup="NewItemGroup"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text="Control Name"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Control Type">
                <EditItemTemplate>
                    <asp:DropDownList ID="NewControlTypeList" runat="server" 
                        DataSourceID="NewControlTypeDS" DataTextField="ControlTypeName" 
                        DataValueField="ControlTypeName">
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="NewControlTypeDS" runat="server" 
                        OldValuesParameterFormatString="original_{0}" 
                        SelectMethod="ListJobControlTypes" TypeName="BrentwoodPrinting.Data.Brentwood">
                    </asp:ObjectDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text="Control Type"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowInsertButton="True" ValidationGroup="NewItemGroup" />
        </Fields>
    </asp:DetailsView>
    <br />
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" 
    ValidationGroup="MainGroup" />
    <br />
    <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
        onclick="CancelButton_Click" ValidationGroup="MainGroup">Cancel Edit</asp:LinkButton>
    <asp:LinkButton ID="SaveButton" runat="server" onclick="SaveButton_Click" 
    ValidationGroup="MainGroup">Save Edit</asp:LinkButton>
    <br />
</asp:Content>

