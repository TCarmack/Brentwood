<%@ Page Title="" Language="C#" MasterPageFile="~/Employee/Employee.master" AutoEventWireup="true" CodeFile="EmployeeDashboard.aspx.cs" Inherits="Employee_EmployeeDashboard" %>
<%@ Reference Control="~/Controls/DashboardGrid.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!--Jack-->
<script runat="server">
    /*
     * Grab a list of roles by the current employee
     * Load up a grid view for each role
     */ 
    
    protected override void OnInit(EventArgs e)
    {
        List<BrentwoodPrinting.Data.EmployeeRole> roles = BrentwoodPrinting.Data.Brentwood.ListRolesByEmployee(Guid.Parse(Membership.GetUser().ProviderUserKey.ToString()));
        List<BrentwoodPrinting.Data.JobStatus> buffer = new List<BrentwoodPrinting.Data.JobStatus>();
        DashboardGrid grid = null;
        Literal lit = null;
        
        foreach (BrentwoodPrinting.Data.EmployeeRole item in roles)
        {
            List<BrentwoodPrinting.Data.JobStatus> newStatuses = BrentwoodPrinting.Data.Brentwood.ListStatusesByRole(item.RoleID);
            
            foreach(BrentwoodPrinting.Data.JobStatus item2 in newStatuses)
                buffer.Add(item2);
        }

        List<BrentwoodPrinting.Data.JobStatus> statuses = buffer.Distinct<BrentwoodPrinting.Data.JobStatus>().ToList<BrentwoodPrinting.Data.JobStatus>();        
        base.OnInit(e);

        SortedList<int, string> JobGrids = new SortedList<int, string>();
        
        foreach (BrentwoodPrinting.Data.JobStatus item in statuses)
        {
            lit = new Literal();
            grid = new DashboardGrid();

            lit.Text = String.Format("<h1>{0} Jobs</h1>", item.Name);
            grid.ID = String.Format("{0}JobsGrid", item.Name);
            
            ControlsPanel.Controls.Add(lit);
            ControlsPanel.Controls.Add(grid);

            JobGrids.Add(item.JobStatusID, grid.ID);
        }

        ViewState["Grids"] = JobGrids;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SortedList<int, string> grids = (SortedList<int, string>)ViewState["Grids"];
        string id = "";
        int jobStatusID = -1;

        for (int i = 0; i < grids.Count; i++)
        {
            jobStatusID = grids.Keys[i];
            id = grids.Values[i];

            ((DashboardGrid)ControlsPanel.FindControl(id)).LoadJobs(jobStatusID);
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1>My Dashboard</h1>
    <asp:Panel ID="ControlsPanel" runat="server">
    </asp:Panel>
    <asp:Label ID="FormMessage" runat="server"></asp:Label>
</asp:Content>