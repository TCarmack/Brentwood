using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using System.Drawing;
//Jack
public partial class Admin_JobStatuses_JobStatusesPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }
    
    private void BindData()
    {
        try
        {
            JobStatusesGridView.DataSource = Brentwood.ListJobStatusWithName();
            JobStatusesGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void JobStatusesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int jobStatusID = int.Parse(JobStatusesGridView.DataKeys[e.RowIndex].Value.ToString());

        if (jobStatusID < 8 || jobStatusID == 25)
        {
            FormMessage.Text = "Default job statuses may not be archived.";
            FormMessage.ForeColor = Color.Red;
        }
        else
        {
            Brentwood.ArchiveJobStatus(jobStatusID);
            BindData();
        }
    }

    protected void JobStatusesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        JobStatusesGridView.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void JobStatusesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int ID = int.Parse(JobStatusesGridView.DataKeys[e.RowIndex].Value.ToString());
        if (ID < 8)
        {
            FormMessage.Text = "Default job statuses may not be edited.";
            FormMessage.ForeColor = Color.Red;
        }
        else
        {
            try
            {
                string name = (JobStatusesGridView.Rows[e.RowIndex].Cells[2].FindControl("NameTextbox") as TextBox).Text.Trim();
                Brentwood.UpdateJobStatus(name, ID);
                FormMessage.Text = "Update successful!";
                FormMessage.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                FormMessage.Text = ex.Message;
                FormMessage.ForeColor = Color.Red;
            }

            JobStatusesGridView.EditIndex = -1;
            BindData();
        }
    }

    protected void JobStatusesGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (JobStatusesGridView.EditIndex != -1)
        {
            e.Cancel = true;
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = "Cancel or complete the current edit!";
        }
        else
        {
            JobStatusesGridView.EditIndex = e.NewEditIndex;
            BindData();
            (JobStatusesGridView.Rows[e.NewEditIndex].Cells[3].FindControl("EmployeeRoleDropdown") as DropDownList).SelectedValue = Brentwood.GetEmployeeRoleByStatusID(int.Parse(JobStatusesGridView.DataKeys[e.NewEditIndex].Value.ToString())).RoleID.ToString();
        }
    }

    protected void JobStatusesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        JobStatusesGridView.EditIndex = -1;
        BindData();
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            Brentwood.InsertJobStatus(NewStatusTextbox.Text.Trim(), int.Parse(RolesDropdown.SelectedValue));
            Response.Redirect("JobStatusesPage.aspx", false);
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void JobStatusesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (JobStatusesGridView.EditIndex != -1)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = "Cancel or complete the current edit!";
        }
        else
        {
            if (e.CommandName == "MoveItemUp" || e.CommandName == "MoveItemDown")
            {
                int index = int.Parse(e.CommandArgument.ToString());

                try
                {
                    int id = int.Parse(JobStatusesGridView.DataKeys[index].Value.ToString());

                    if (id < 8)
                    {
                        FormMessage.Text = "These status cannot be reordered.";
                        FormMessage.ForeColor = Color.Red;
                    }
                    else
                    {
                        JobStatus item = Brentwood.GetJobStatus(id);

                        switch (e.CommandName)
                        {
                            case "MoveItemUp":
                                if (item.OrderingNumber - 1 <= 7)
                                {
                                    FormMessage.Text = "This status cannot be moved above \"Designs Approved\"";
                                    FormMessage.ForeColor = Color.Red;
                                }
                                else
                                {
                                    JobStatus currentItem = Brentwood.GetStatusByOrderingNumber((int)item.OrderingNumber - 1);
                                    Brentwood.ChangeStatusOrderingNo((int)item.OrderingNumber, currentItem.JobStatusID);
                                    Brentwood.ChangeStatusOrderingNo((int)item.OrderingNumber - 1, item.JobStatusID);
                                }
                                break;

                            case "MoveItemDown":
                                if (item.OrderingNumber == (Brentwood.GetLastOrderingNo()))
                                {
                                    FormMessage.Text = "Status is already last.";
                                    FormMessage.ForeColor = Color.Red;
                                }
                                else
                                {
                                    JobStatus currentItem = Brentwood.GetStatusByOrderingNumber((int)item.OrderingNumber + 1);
                                    Brentwood.ChangeStatusOrderingNo((int)item.OrderingNumber, currentItem.JobStatusID);
                                    Brentwood.ChangeStatusOrderingNo((int)item.OrderingNumber + 1, item.JobStatusID);
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FormMessage.Text = ex.Message;
                    FormMessage.ForeColor = Color.Red;
                }

                BindData();
            }
        }
    }
}