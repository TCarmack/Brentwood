using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using BrentwoodPrinting.CookieClasses.Customer;
//Jack
public partial class Admin_Accounts_Customer_Customer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FormMessage.ForeColor = Color.Black;
            FormMessage.Text = "";

            try
            {
                List<ListCustomers_Result> data = Brentwood.ListCustomers();
                CustomerView.DataSource = data;
                CustomerView.DataBind();

                if (Context.Items["UserId"] == null)
                    Response.Redirect("Customers.aspx", true);
                else
                {
                    Guid userid = Guid.Parse(Context.Items["UserId"].ToString());
                    int itemIndex = data.FindIndex(item => item.UserId == userid);
                    CustomerView.PageIndex = itemIndex;
                    CustomerView.DataSource = data;
                    CustomerView.DataBind();
                }
            }
            catch (Exception ex)
            {
                FormMessage.ForeColor = Color.Red;
                FormMessage.Text = ex.Message;
            }
        }
    }

    protected void CustomerView_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {
        try
        {
            CustomerView.PageIndex = e.NewPageIndex;
            List<ListCustomers_Result> data = Brentwood.ListCustomers();
            CustomerView.DataSource = data;
            CustomerView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void CustomerView_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        if (e.CancelingEdit)
        {
            try
            {
                List<ListCustomers_Result> data = Brentwood.ListCustomers();
                Guid userid = Guid.Parse(CustomerView.DataKey.Value.ToString());
                int itemIndex = data.FindIndex(item => item.UserId == userid);
                CustomerView.PageIndex = itemIndex;
                CustomerView.DataSource = data;
                CustomerView.DataBind();
            }
            catch (Exception ex)
            {
                FormMessage.ForeColor = Color.Red;
                FormMessage.Text = ex.Message;
            }
        }
    }

    protected void CustomerView_DataBound(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)this.CustomerView.FindControl("CompanyList");
            ddl.DataSource = Brentwood.ListCompanies();
            ddl.DataBind();

            Company company = Brentwood.GetCompanyByCustomerId(Guid.Parse(CustomerView.DataKey.Value.ToString()));

            if (company != null)
                ddl.Items.FindByValue(company.CompanyID.ToString()).Selected = true;
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        try
        {
            List<ListCustomers_Result> data = Brentwood.ListCustomers();
            CustomerView.DataSource = data;
            CustomerView.DataBind();
            Guid userid = Guid.Parse(CustomerView.DataKey.Value.ToString());
            int itemIndex = data.FindIndex(item => item.UserId == userid);
            CustomerView.PageIndex = itemIndex;
            CustomerView.DataSource = data;
            CustomerView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        aspnet_Users item = new aspnet_Users();
        item.UserId = Guid.Parse(CustomerView.DataKey.Value.ToString());
        item.UserName = CustomerView.Rows[0].Cells[1].Text.Trim();
        item.FirstName = (CustomerView.Rows[2].Cells[1].Controls[1] as TextBox).Text.Trim();
        item.LastName = (CustomerView.Rows[3].Cells[1].FindControl("LastNameTextbox") as TextBox).Text.Trim();
        item.CustomerAddress = (CustomerView.Rows[4].Cells[1].FindControl("AddressTextbox") as TextBox).Text.Trim();
        item.City = (CustomerView.Rows[5].Cells[1].FindControl("CityTextbox") as TextBox).Text.Trim();
        item.Province = (CustomerView.Rows[6].FindControl("ProvinceTextbox") as TextBox).Text.Trim();
        item.PostalCode = (CustomerView.Rows[7].FindControl("PostalCodeTextbox") as TextBox).Text.Trim();
        item.PhoneNumber = (CustomerView.Rows[8].FindControl("PhoneNumberTextbox") as TextBox).Text.Trim();
        item.CompanyID = int.Parse((CustomerView.Rows[10].FindControl("CompanyList") as DropDownList).SelectedValue);
        string email = (CustomerView.Rows[9].FindControl("EmailTextbox") as TextBox).Text.Trim();
        if (item.CompanyID == -1)
            item.CompanyID = null;        
        item.Approved = (CustomerView.Rows[12].FindControl("ApprovedCheckbox") as CheckBox).Checked;

        try
        {
            Brentwood.UpdateCustomer(item, email);

            if ((CustomerView.Rows[14].FindControl("NewPassTextbox") as TextBox).Text.Trim() != "")
                Membership.GetUser(item.UserName).ChangePassword((CustomerView.Rows[13].FindControl("OldPassTextbox") as TextBox).Text.Trim(), (CustomerView.Rows[13].FindControl("NewPassTextbox") as TextBox).Text.Trim());

            if ((CustomerView.Rows[11].FindControl("IsAdminCheckbox") as CheckBox).Checked)
                Brentwood.MakeCustomerAdmin(item.UserId);
            else
                Brentwood.MakeCustomerNotAdmin(item.UserId);

            FormMessage.Text = "Customer account successfully updated!";
            FormMessage.ForeColor = Color.Black;
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }
    }

    protected void ApproveButton_Click(object sender, EventArgs e)
    {
        aspnet_Users item = new aspnet_Users();
        item.UserId = Guid.Parse(CustomerView.DataKey.Value.ToString());
        item.UserName = CustomerView.Rows[0].Cells[1].Text.Trim();
        item.FirstName = (CustomerView.Rows[2].Cells[1].Controls[1] as TextBox).Text.Trim();
        item.LastName = (CustomerView.Rows[3].Cells[1].FindControl("LastNameTextbox") as TextBox).Text.Trim();
        item.CustomerAddress = (CustomerView.Rows[4].Cells[1].FindControl("AddressTextbox") as TextBox).Text.Trim();
        item.City = (CustomerView.Rows[5].Cells[1].FindControl("CityTextbox") as TextBox).Text.Trim();
        item.Province = (CustomerView.Rows[6].FindControl("ProvinceTextbox") as TextBox).Text.Trim();
        item.PostalCode = (CustomerView.Rows[7].FindControl("PostalCodeTextbox") as TextBox).Text.Trim();
        item.PhoneNumber = (CustomerView.Rows[8].FindControl("PhoneNumberTextbox") as TextBox).Text.Trim();
        item.CompanyID = int.Parse((CustomerView.Rows[10].FindControl("CompanyList") as DropDownList).SelectedValue);
        string email = (CustomerView.Rows[9].FindControl("EmailTextbox") as TextBox).Text.Trim();
        if (item.CompanyID == -1)
            item.CompanyID = null;
        item.IsAdmin = (CustomerView.Rows[11].FindControl("IsAdminCheckbox") as CheckBox).Checked;

        item.Approved = true;

        try
        {
            Roles.AddUserToRole(item.UserName, "Approved Customer");
            Brentwood.UpdateCustomer(item, email);
            FormMessage.Text = "Customer successfully approved!";
            FormMessage.ForeColor = Color.Black;
        }
        catch (Exception ex)
        {
            FormMessage.ForeColor = Color.Red;
            FormMessage.Text = ex.Message;
        }

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        MembershipUser user = Membership.GetUser(CustomerView.Rows[0].Cells[1].Text.Trim());
        user.IsApproved = false;
        Membership.UpdateUser(user);
    }
}