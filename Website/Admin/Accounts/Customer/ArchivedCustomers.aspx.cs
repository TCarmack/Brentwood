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
public partial class Admin_Accounts_Customer_ArchivedCustomers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<ListCustomers_Result> data = Brentwood.ListArchivedCustomers();
        CustomersGridView.DataSource = data;
        CustomersGridView.DataBind();
    }

    protected void CustomersGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CustomersGridView.PageIndex = e.NewPageIndex;
        CustomersGridView.DataSource = Brentwood.ListArchivedCustomers();
        CustomersGridView.DataBind();
    }

    protected void CustomersGridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Guid userid = Guid.Parse(CustomersGridView.DataKeys[e.NewSelectedIndex].Value.ToString());
            Brentwood.UnarchiveCustomer(userid);
            CustomersGridView.DataSource = Brentwood.ListArchivedCustomers();
            CustomersGridView.DataBind();

            List<ListCustomers_Result> data = new List<ListCustomers_Result>() { Brentwood.GetCustomer(userid) };
            CustomerControlCookie item = CustomerUtils.ToCookieClass(data).FirstOrDefault();
            CustomerControlCart.AddItem(item);
        }
        catch (Exception ex)
        {
            FormMessage.Text = ex.Message;
            FormMessage.ForeColor = Color.Red;
        }
    }
}