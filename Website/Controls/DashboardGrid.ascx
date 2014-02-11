<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashboardGrid.ascx.cs" Inherits="Controls_DashboardGrid" className="DashboardGrid"%>
<%--class="DashboardGrid"--%>
<!--Jack-->
<table>
    <tr>
        <td>
            <h3>Filtering Options</h3>
        </td>
    </tr>
    <tr>
        <td><!--Checkbox List-->
            <asp:Label ID="Label1" runat="server" Text="Job Types"></asp:Label>
            <asp:CheckBox ID="TypeFilterCheckbox" runat="server" AutoPostBack="True" />
        </td>
        <td><!--Quantity-->
            <asp:Label ID="Label2" runat="server" Text="Quantity"></asp:Label>
            <asp:CheckBox ID="QuantityCheckbox" runat="server" AutoPostBack="True" />
        </td>
        <td><!--Promise Date-->
            <asp:Label ID="Label3" runat="server" Text="Promise Date"></asp:Label>
            <asp:CheckBox ID="PromiseCheckbox" runat="server" AutoPostBack="True" />
        </td>
        <td><!--Start Date-->
            <asp:Label ID="Label4" runat="server" Text="Start Date"></asp:Label>
            <asp:CheckBox ID="StartDateCheckbox" runat="server" AutoPostBack="True" />
        </td>
        <td><!--Customer-->
            <asp:Label ID="Label5" runat="server" Text="Customer"></asp:Label>
            <asp:CheckBox ID="CustomerCheckbox" runat="server" AutoPostBack="True" />
        </td>
    </tr>
    <tr>
        <td><!--Checkbox List-->
            <asp:CheckBoxList ID="JobTypesList" runat="server" DataSourceID="TypesDS" 
                AutoPostBack="True" Visible="False" DataTextField="Name"></asp:CheckBoxList>
        </td>
        <td><!--Quantity-->
            <script type="text/javascript">
                $(document).ready(function () {
                    //Ensures only numbers are input
                    $('input[id$="QuantityTextBox"]').keydown(function (event) {
                        if (((event.keyCode == 8 || event.keyCode == 46) || (event.keyCode >= 47 && event.keyCode < 58) || (event.keyCode >= 96 && event.keyCode < 106)) == false) {
                            event.preventDefault();
                        }
                    });
                });
            </script>
            <asp:TextBox ID="QuantityTextBox" runat="server" AutoPostBack="True" 
                Visible="False" Height="25px"></asp:TextBox>
            <asp:DropDownList ID="QuantityOption" runat="server" AutoPostBack="True" 
                Visible="False">
                <asp:ListItem Value="GreaterThan" Text="Greater Than" />
                <asp:ListItem Value="LessThan" Text="Less Than" />
            </asp:DropDownList>
        </td>
        <td><!--Promise Date-->
            <asp:Calendar ID="PromiseCalendar" runat="server" Visible="False"></asp:Calendar>
            <asp:DropDownList ID="PromiseDateOptions" runat="server" Visible="False" 
                AutoPostBack="True">
                <asp:ListItem Value="Before" Text="Before" />
                <asp:ListItem Value="After" Text="After" />
            </asp:DropDownList>
        </td>
        <td><!--Start Date-->
            <asp:Calendar ID="StartCalendar" runat="server" Visible="False"></asp:Calendar>
            <asp:DropDownList ID="StartDateOptions" runat="server" Visible="False" 
                AutoPostBack="True">
                <asp:ListItem Value="Before" Text="Before" />
                <asp:ListItem Value="After" Text="After" />
            </asp:DropDownList>
        </td>
        <td><!--Customer-->
            <asp:TextBox ID="CustomerTextbox" runat="server"></asp:TextBox>
            <asp:DropDownList ID="NameSearchOptions" runat="server">
                <asp:ListItem Text="Starts With" Value="StartsWith" />
                <asp:ListItem Text="Contains" Value="Contains" />
                <asp:ListItem Text="Ends With" Value="EndsWith" />
            </asp:DropDownList>
        </td>
    </tr>
</table>
<asp:GridView ID="JobsGridView" runat="server" AllowPaging="True" 
    DataKeyNames="JobID" onpageindexchanging="JobsGridView_PageIndexChanging" 
    onselectedindexchanging="JobsGridView_SelectedIndexChanging" 
    AutoGenerateColumns="False" AllowSorting="True" 
    onsorting="JobsGridView_Sorting">
    <Columns>
        <asp:CommandField ShowSelectButton="True" />
        <asp:BoundField DataField="JobID" HeaderText="Job ID" SortExpression="JobID" />
        <asp:BoundField DataField="Name" HeaderText="Job Type" 
            SortExpression="JobType" />
        <asp:BoundField DataField="Quantity" HeaderText="Quantity" 
            SortExpression="Quantity" />
        <asp:BoundField DataField="Customer" HeaderText="Customer" 
            SortExpression="Customer" />
        <asp:BoundField DataField="PromiseDate" 
            DataFormatString="{0:dd MMM yyyy hh:mm tt}" HeaderText="Promise Date" 
            SortExpression="PromiseDate" />
        <asp:BoundField DataField="StartDate" 
            DataFormatString="{0:dd MMM yyyy hh:mm tt}" HeaderText="Date Started" 
            SortExpression="DateStarted" />
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="NoDataLabel" runat="server" 
            Text="There are no jobs of this status to display."></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>
<asp:Label ID="FormMessage" runat="server"></asp:Label>
<asp:ObjectDataSource ID="TypesDS" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="ListJobTypes" 
        TypeName="BrentwoodPrinting.Data.Brentwood"></asp:ObjectDataSource>