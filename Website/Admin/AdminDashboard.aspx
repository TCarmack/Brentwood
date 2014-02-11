<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="AdminDashboard.aspx.cs" Inherits="Admin_AdminDefault" %>
<%@ Reference Control="~/Controls/DashboardGrid.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
<script runat="server">
    private List<DashboardGrid> grids = new List<DashboardGrid>();
    
    protected override void OnInit(EventArgs e)
    {
        try
        {
            List<BrentwoodPrinting.Data.JobStatus> statuses = BrentwoodPrinting.Data.Brentwood.ListJobStatuses();
            DashboardGrid grid = null;
            Literal lit = null;

            foreach (BrentwoodPrinting.Data.JobStatus item in statuses)
            {
                grid = new DashboardGrid();
                lit = new Literal();

                lit.Text = String.Format("<h1>{0} Jobs</h1>", item.Name);
                grid.ID = item.Name + "Jobs";
                grid.JobStatusID = item.JobStatusID;
                
                ControlsPanel.Controls.Add(lit);
                ControlsPanel.Controls.Add(grid);
                grids.Add(grid);
            }
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = System.Drawing.Color.Red;
        }


        base.OnInit(e);

        foreach (DashboardGrid item in grids)
            item.LoadJobs(item.JobStatusID);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>My Dashboard</h1>
    <asp:Panel ID="ControlsPanel" runat="server"></asp:Panel>
    <asp:Label ID="FormMessage" runat="server" />
</asp:Content>