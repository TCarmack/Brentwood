using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using BrentwoodPrinting.CookieClasses.Customer;
using BrentwoodPrinting.Data;
//Jack
/// <summary>
/// Cart Manager class for the Customer cart
/// </summary>
public static class CustomerControlCart
{
    /// <summary>
    /// Retrieves the currently stored cart
    /// </summary>
    /// <returns>A list of customers that represents the current cart</returns>
    public static List<CustomerControlCookie> RetrieveCart()
    {
        List<CustomerControlCookie> cart = new List<CustomerControlCookie>();

        if (CookieExists())
        {
            try
            {
                string xmlCartString =
                    CookieUtils.FromEncodedCookie(HttpContext.Current.Request.Cookies["CustomerControlCart"].Value);
                System.Xml.Serialization.XmlSerializer serializer =
                    new System.Xml.Serialization.XmlSerializer(typeof(List<CustomerControlCookie>));
                cart = (List<CustomerControlCookie>)serializer.Deserialize(new System.Xml.XmlTextReader(new System.IO.StringReader(xmlCartString)));
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading cart; Exception:" + ex.Message);
            }
        }

        return cart;
    }

    /// <summary>
    /// Saves the current cart
    /// </summary>
    /// <param name="data">A list of job type controls that represents the current cart</param>
    public static void SaveCart(List<CustomerControlCookie> data)
    {
        StringBuilder XMLCart = new StringBuilder();
        System.IO.StringWriter XMLCartStream = new System.IO.StringWriter(XMLCart);

        System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(data.GetType());
        serializer.Serialize(XMLCartStream, data);

        StringBuilder XMLCompressedCart = new StringBuilder();

        for (int i = 0; i < XMLCart.Length; i++)
        {
            char c = XMLCart[i];

            if (!char.IsControl(c))
                XMLCompressedCart.Append(c);
        }

        HttpContext.Current.Response.Cookies.Add(new HttpCookie("CustomerControlCart",
            CookieUtils.ToEncodedCookie(XMLCompressedCart.ToString())));
    }

    #region CRUD Functionality
    /// <summary>
    /// Adds a new customer to the current cart.
    /// </summary>
    /// <param name="userid">The GUID UserId of the customer.</param>
    /// <param name="username">The username of the customer.</param>
    /// <param name="firstname">The customer's first name.</param>
    /// <param name="lastname">The customer's last name.</param>
    /// <param name="customeraddress">The customer's address.</param>
    /// <param name="city">The customer's city.</param>
    /// <param name="province">The customer's province.</param>
    /// <param name="postalcode">The customer's postal code.</param>
    /// <param name="phonenumber">The customer's phone number.</param>
    /// <param name="name">The name of the company the customer works for, if appropriate.</param>
    /// <param name="approved">Whether the customer is approved for ordering or not.</param>
    /// <param name="lastactivitydate">The last time the customer interacted with the system.</param>
    /// <returns>The current cart.</returns>
    public static List<CustomerControlCookie> AddItem(Guid userid, string username,
        string firstname, string lastname, string customeraddress, string city,
        string province, string postalcode, string phonenumber, string name,
        bool approved, DateTime lastactivitydate)
    {
        List<CustomerControlCookie> cart = RetrieveCart();
        CustomerControlCookie customer = new CustomerControlCookie();

        customer.UserId = userid;
        customer.FirstName = firstname;
        customer.LastName = lastname;
        customer.UserName = username;
        customer.CustomerAddress = customeraddress;
        customer.City = city;
        customer.Province = province;
        customer.PostalCode = postalcode;
        customer.PhoneNumber = phonenumber;
        customer.Name = name;
        customer.Approved = approved;
        customer.LastActivityDate = lastactivitydate;

        cart.Add(customer);

        return cart;
    }

    public static List<CustomerControlCookie> AddItem(CustomerControlCookie item)
    {
        List<CustomerControlCookie> cart = RetrieveCart();
        cart.Add(item);
        return cart;
    }

    /// <summary>
    /// Updates an item in the current cart.
    /// </summary>
    /// <param name="userid">The GUID UserId of the customer.</param>
    /// <param name="username">The username of the customer.</param>
    /// <param name="firstname">The customer's first name.</param>
    /// <param name="lastname">The customer's last name.</param>
    /// <param name="customeraddress">The customer's address.</param>
    /// <param name="city">The customer's city.</param>
    /// <param name="province">The customer's province.</param>
    /// <param name="postalcode">The customer's postal code.</param>
    /// <param name="phonenumber">The customer's phone number.</param>
    /// <param name="name">The name of the company the customer works for, if appropriate.</param>
    /// <param name="approved">Whether the customer is approved for ordering or not.</param>
    /// <param name="lastactivitydate">The last time the customer interacted with the system.</param>
    /// <returns>The current cart.</returns>
    public static List<CustomerControlCookie> UpdateItem(Guid userid, string username,
        string firstname, string lastname, string customeraddress, string city,
        string province, string postalcode, string phonenumber, string name,
        bool approved, DateTime lastactivitydate)
    {
        List<CustomerControlCookie> cart = RetrieveCart();
        int itemIndex = cart.FindIndex(item => item.UserId == userid);

        cart[itemIndex].UserId = userid;
        cart[itemIndex].FirstName = firstname;
        cart[itemIndex].LastName = lastname;
        cart[itemIndex].UserName = username;
        cart[itemIndex].CustomerAddress = customeraddress;
        cart[itemIndex].City = city;
        cart[itemIndex].Province = province;
        cart[itemIndex].PostalCode = postalcode;
        cart[itemIndex].PhoneNumber = phonenumber;
        cart[itemIndex].Name = name;
        cart[itemIndex].Approved = approved;
        cart[itemIndex].LastActivityDate = lastactivitydate;

        return cart;
    }

    /// <summary>
    /// Removes the specified item from the cart
    /// </summary>
    /// <param name="itemID">The index position in the cart list that represents the item to remove</param>
    /// <returns>The current cart. An empty List of CustomerControl is returned if empty</returns>
    public static List<CustomerControlCookie> RemoveItem(string username)
    {
        List<CustomerControlCookie> cart = RetrieveCart();
        int itemIndex = cart.FindIndex(item => item.UserName == username);
        cart.RemoveAt(itemIndex);
        SaveCart(cart);
        return cart;
    }
    #endregion

    #region Supporting Methods
    /// <summary>
    /// Destroys the current cart
    /// </summary>
    public static void DestroyCart()
    {
        HttpCookie cookie = new HttpCookie("CustomerControlCart");
        cookie.Expires = DateTime.Today.AddDays(-30);
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    /// <summary>
    /// Checks to see whether the cart exists in the current session
    /// </summary>
    /// <returns>A boolean indicating whether the cookie exists in the current server request</returns>
    public static bool CookieExists()
    {
        return (HttpContext.Current.Request.Cookies["CustomerControlCart"] != null);
    }
    #endregion
}