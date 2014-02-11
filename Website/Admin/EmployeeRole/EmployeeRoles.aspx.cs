using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;

public partial class Admin_EmployeeRole_EmployeeRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        try
        {
            EmployeeRoles.DataSource = Brentwood.ListEmployeeRoles();
            EmployeeRoles.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void EmployeeRoles_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (EmployeeRoles.EditIndex != -1)
        {
            FormMessage.Text = "Cancel or complete the current edit.";
            FormMessage.ForeColor = Color.Red;
        }
        else
        {
            EmployeeRoles.EditIndex = e.NewEditIndex;
            BindData();
        }
    }

    protected void EmployeeRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Brentwood.ArchiveEmployeeRole(int.Parse(EmployeeRoles.DataKeys[e.RowIndex].Value.ToString()));
            Response.Redirect("~/Admin/EmployeeRole/EmployeeRoles.aspx");
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
        BindData();
    }

    protected void EmployeeRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        EmployeeRoles.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void EmployeeRoles_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string name = ((TextBox)EmployeeRoles.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            Brentwood.UpdateEmployeeRole(int.Parse(EmployeeRoles.DataKeys[e.RowIndex].Value.ToString()), name);
            Response.Redirect("~/Admin/EmployeeRole/EmployeeRoles.aspx");
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void EmployeeRoles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        EmployeeRoles.EditIndex = -1;
        BindData();
    }

    protected void NewRole_Click(object sender, EventArgs e)
    {
        try
        {
            Brentwood.InsertEmployeeRole(RoleTextBox.Text.Trim());
            Response.Redirect("~/Admin/EmployeeRole/EmployeeRoles.aspx");
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}