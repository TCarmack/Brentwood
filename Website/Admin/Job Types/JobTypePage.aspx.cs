using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using BrentwoodPrinting.CookieClasses.JobType;
using System.Drawing;
//Jack
public partial class Admin_JobTypePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // If this page was loaded by the gridview on the job types page,
        // fill the form with the data specified by the job type ID
        if (!(Page.PreviousPage == null))
        {
            //Gets the ID of the job type
            int jobTypeID = (int)Context.Items["JobTypeID"];

            //Ensure there's an ID to pull data from
            if (!(jobTypeID == null))
            {
                //Populate the controls gridview
                JobType jobType = Brentwood.GetJobType((int)jobTypeID);
                List<JobControl_Get_Result> controls = Brentwood.ListJobControlByJobType((int)jobTypeID);
                List<JobTypeControlCookie> cartData = JobTypeUtils.ConvertToCartClass(controls);
                JobTypeControlCart.SaveCart(cartData);

                //Populate the form controls
                Session["JobTypeID"] = jobTypeID;
                JobTypeNameTextBox.Text = jobType.Name;
                EstDaysTextbox.Text = jobType.EstimatedTimeToComplete.Substring(0, 2);
                EstHoursTextbox.Text = jobType.EstimatedTimeToComplete.Substring(3, 2);
                DescriptionTextBox.Text = jobType.JobTypeDescription;

                //Bind the gridview
                ControlsGridView.DataSource = cartData;
                ControlsGridView.DataBind();
                Session["JobTypeID"] = jobTypeID;
            }
        }
        else if (!Page.IsPostBack)
        {
            //Reset the form message
            FormMessage.Text = "";
            FormMessage.ForeColor = Color.Black;

            if (Session["JobTypeID"] != null)
            {
                //If there's a cookie containing the controls
                if (JobTypeControlCart.CookieExists())
                {
                    //Put the cookie data in the gridview and bind
                    List<JobTypeControlCookie> data = JobTypeControlCart.RetrieveCart();
                    ControlsGridView.DataSource = data;
                    ControlsGridView.DataBind();
                }
            }
            else
            {
                JobTypeControlCart.DestroyCart();
                ControlsGridView.DataSource = null;
                ControlsGridView.DataBind();
            }
        }
    }

    protected void ControlsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = int.Parse(ControlsGridView.DataKeys[e.RowIndex].Value.ToString());
        try
        {
            List<JobTypeControlCookie> data = JobTypeControlCart.RemoveItem(id);
            JobTypeControlCart.SaveCart(data);
            ControlsGridView.DataSource = data;
            ControlsGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void ControlsGridView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (ControlsGridView.EditIndex > -1)
        {
            e.Cancel = true;
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = "Cancel or complete the current edit.";
        }
        else
        {
            ControlsGridView.EditIndex = e.NewEditIndex;
            try
            {
                List<JobTypeControlCookie> data = JobTypeControlCart.RetrieveCart();
                ControlsGridView.DataSource = data;
                ControlsGridView.DataBind();

                DropDownList list = ControlsGridView.Rows[e.NewEditIndex].FindControl("ControlTypeList") as DropDownList;
                list.Items.FindByText(JobTypeControlCart.RetrieveCart()[e.NewEditIndex].JobControlTypeName).Selected = true;
            }
            catch (Exception ex)
            {
                FormMessage.ForeColor = Color.Red;
                FormMessage.Text = ex.Message;
            }
        }
    }

    protected void ControlsGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ControlsGridView.EditIndex = -1;

        try
        {
            List<JobTypeControlCookie> data = JobTypeControlCart.RetrieveCart();
            ControlsGridView.DataSource = data;
            ControlsGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }

    }

    protected void ControlsGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gvr = ControlsGridView.Rows[ControlsGridView.EditIndex];
        string name, controlType;
        int id;

        id = int.Parse(ControlsGridView.DataKeys[ControlsGridView.EditIndex].Value.ToString());
        name = (gvr.FindControl("NameTextbox") as TextBox).Text.Trim();
        controlType = (gvr.FindControl("ControlTypeList") as DropDownList).SelectedItem.Text.Trim();

        //Ensure there's a name, if not, put in a default value
        if (name == "")
            name = "&lt;No Name Entererd&gt;";

        //Update the cookie
        try
        {
            List<JobTypeControlCookie> data = null;

            if (Session["JobTypeID"].ToString() != "")
                data = JobTypeControlCart.UpdateItem(id, name, controlType);
            else
                data = JobTypeControlCart.UpdateItemNewJobType(name, controlType, e.RowIndex);

            JobTypeControlCart.SaveCart(data);

            //Reset the edit index
            ControlsGridView.EditIndex = -1;

            //Rebind the gridview from the cookie
            ControlsGridView.DataSource = data;
            ControlsGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void ControlsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ControlsGridView.PageIndex = e.NewPageIndex;
        try
        {
            List<JobTypeControlCookie> controls = JobTypeControlCart.RetrieveCart();
            ControlsGridView.DataSource = controls;
            ControlsGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        if (ControlsGridView.EditIndex != -1)
        {
            FormMessage.Text = "Cancel or complete the control edit.";
            FormMessage.ForeColor = Color.Red;
        }
        else
        {
            //Local variables
            int id = -1;
            string name, desc, days, hours;
            List<JobTypeControlCookie> data;

            //Validate data
            //If there's an id, pull it from the hidden field on the form
            if (Session["JobTypeID"] != null)
                id = int.Parse(Session["JobTypeID"].ToString());

            name = JobTypeNameTextBox.Text.Trim();

            //Construct the estimatetimetocomplete string
            if (EstDaysTextbox.Text.Trim().Length == 1)
                days = "0" + EstDaysTextbox.Text.Trim();
            else
                days = EstDaysTextbox.Text.Trim();

            if (EstHoursTextbox.Text.Trim().Length == 1)
                hours = "0" + EstHoursTextbox.Text.Trim();
            else
                hours = EstHoursTextbox.Text.Trim();

            desc = DescriptionTextBox.Text.Trim();

            //Create the job type object
            JobType jt = new JobType();
            jt.JobTypeID = id;
            jt.Name = name;

            if (days == "")
                days = "00";

            if (hours == "")
                hours = "00";

            jt.EstimatedTimeToComplete = days + "d" + hours + "h";
            jt.JobTypeDescription = desc;

            try
            {
                //Populate the controls list
                data = JobTypeControlCart.RetrieveCart();
                List<JobControl> controls = JobTypeUtils.ConvertFromCartClass(data);
                //If ID = -1, then it's a new object
                if (id == -1)
                {
                    //Commit this data to the database
                    Brentwood.ProcessJobType(jt, controls);
                    FormMessage.Text = "Job type successfully created!";
                    FormMessage.ForeColor = Color.Black;
                }
                else
                {
                    //Update current job type

                    //Commit this data to the database
                    Brentwood.ProcessJobTypeUpdate(jt, controls);
                    FormMessage.Text = "Job type successfully updated!";
                    FormMessage.ForeColor = Color.Black;

                }
            }
            catch (Exception ex)
            {
                FormMessage.ForeColor = Color.Red;
                FormMessage.Text = ex.Message;
            }
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            //Reset the form and clear all data
            JobTypeControlCart.DestroyCart();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }

        Response.Redirect("JobTypes.aspx");
    }

    protected void ControlView_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        ControlView.DataSource = null;
        ControlView.DataBind();
    }

    protected void ControlView_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
        string name = (ControlView.Rows[0].FindControl("NewControlNameTextbox") as TextBox).Text.Trim();
        string type = (ControlView.Rows[0].FindControl("NewControlTypeList") as DropDownList).SelectedValue.ToString().Trim();

        try
        {
            List<JobTypeControlCookie> data = null;
            if (Session["JobTypeID"] == null)
                data = JobTypeControlCart.AddItemNewJob(-1, name, type, ControlsGridView.Rows.Count);
            else
                data = JobTypeControlCart.AddItem(-1, name, type);

            JobTypeControlCart.SaveCart(data);
            ControlsGridView.DataSource = data;
            ControlsGridView.DataBind();

            (ControlView.Rows[0].FindControl("NewControlNameTextbox") as TextBox).Text = "";
            (ControlView.Rows[0].FindControl("NewControlNameTextbox") as TextBox).Focus();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }
}