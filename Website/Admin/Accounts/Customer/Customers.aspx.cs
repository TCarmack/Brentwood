using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BrentwoodPrinting.Data;
using BrentwoodPrinting.CookieClasses.Customer;
using System.Drawing;
//Jack
public partial class Admin_Accounts_Customer_Customers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Retrieve a list of customers, load it into cookies, and bind the gridview
            List<CustomerControlCookie> data = CustomerUtils.ToCookieClass(Brentwood.ListCustomers());
            CustomersGridView.DataSource = data;
            CustomersGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }

        //If the page isn't being accessed from another page
        if (!Page.IsPostBack)
        {
            FormMessage.ForeColor = Color.Black;
            FormMessage.Text = "";
        }
    }

    protected void CustomersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        //When selected, pull the id from the table and redirect to the customer page
        Context.Items.Add("UserId", CustomersGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
        Server.Transfer("CustomerPage.aspx");
    }

    protected void CustomersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            CustomersGridView.PageIndex = e.NewPageIndex;
            List<CustomerControlCookie> data = CustomerUtils.ToCookieClass(Brentwood.ListCustomers());
            CustomersGridView.DataSource = data;
            CustomersGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }

    protected void CustomersGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            aspnet_Users item = new aspnet_Users();
            item.UserId = Guid.Parse(CustomersGridView.DataKeys[e.RowIndex].Value.ToString());
            item.UserName = CustomersGridView.Rows[e.RowIndex].Cells[2].Text.Trim();
            Brentwood.ArchiveCustomer(item);
            List<CustomerControlCookie> data = CustomerUtils.ToCookieClass(Brentwood.ListCustomers());
            CustomersGridView.DataSource = data;
            CustomersGridView.DataBind();
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}