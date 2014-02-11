using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using BrentwoodPrinting.CookieClasses.JobType;
using BrentwoodPrinting.Data;
//Jack
/// <summary>
/// Cart Manager class for the JobTypeControl cart
/// </summary>
public static class JobTypeControlCart
{
    /// <summary>
    /// Retrieves the currently stored cart
    /// </summary>
    /// <returns>A list of job type controls that represents the current cart</returns>
    public static List<JobTypeControlCookie> RetrieveCart()
    {
        List<JobTypeControlCookie> cart = new List<JobTypeControlCookie>();

        if (CookieExists())
        {
            try
            {
                string xmlCartString = 
                    CookieUtils.FromEncodedCookie(HttpContext.Current.Request.Cookies["JobTypeControlCart"].Value);
                System.Xml.Serialization.XmlSerializer serializer = 
                    new System.Xml.Serialization.XmlSerializer(typeof(List<JobTypeControlCookie>));
                cart = (List<JobTypeControlCookie>)serializer.Deserialize(new System.Xml.XmlTextReader(new System.IO.StringReader(xmlCartString)));
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
    public static void SaveCart(List<JobTypeControlCookie> data)
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

        HttpContext.Current.Response.Cookies.Add(new HttpCookie("JobTypeControlCart", 
            CookieUtils.ToEncodedCookie(XMLCompressedCart.ToString())));
    }

    #region CRUD Functionality
    /// <summary>
    /// Adds a job type control to the current cart
    /// </summary>
    /// <param name="jobControlID">The ID of the job control</param>
    /// <param name="controlName">The name of the job control</param>
    /// <param name="jobControlTypeID">The ID of the control type this control is</param>
    /// <param name="maxLength">The max length of the control, where applicable</param>
    /// <param name="minLength">The min length of the control, where applicable</param>
    /// <returns>The current cart</returns>
    public static List<JobTypeControlCookie> AddItem(int jobTypeControlID, string controlName, string jobControlTypeName)
    {
        List<JobTypeControlCookie> cart = RetrieveCart();
        JobTypeControlCookie control = new JobTypeControlCookie();
        
        control.JobControlID = jobTypeControlID;
        control.ControlName = controlName;
        control.JobControlTypeName = jobControlTypeName;

        cart.Add(control);

        return cart;
    }

    public static List<JobTypeControlCookie> AddItemNewJob(int jobTypeControlID, string controlName, string jobControlTypeName, int id)
    {
        List<JobTypeControlCookie> cart = RetrieveCart();
        JobTypeControlCookie control = new JobTypeControlCookie();

        control.JobControlID = jobTypeControlID;
        control.ControlName = controlName;
        control.JobControlTypeName = jobControlTypeName;
        control.InterimID = id;

        cart.Add(control);

        return cart;
    }


    /// <summary>
    /// Updates an item in the current cart
    /// </summary>
    /// <param name="jobTypeControlID">The ID of the job control</param>
    /// <param name="controlName">The name of the job control</param>
    /// <param name="jobControlTypeID">The ID of the control type this control is</param>
    /// <returns>The current cart</returns>
    public static List<JobTypeControlCookie> UpdateItem(int jobTypeControlID, string controlName, 
        string jobControlTypeName)
    {
        List<JobTypeControlCookie> cart = RetrieveCart();
        int itemIndex = cart.FindIndex(item => item.JobControlID == jobTypeControlID);

        cart[itemIndex].ControlName = controlName;
        cart[itemIndex].JobControlTypeName = jobControlTypeName;
        return cart;
    }

    public static List<JobTypeControlCookie> UpdateItemNewJobType(string controlName,
    string jobControlTypeName, int interimID)
    {
        List<JobTypeControlCookie> cart = RetrieveCart();
        int itemIndex = cart.FindIndex(item => item.InterimID == interimID);

        cart[itemIndex].ControlName = controlName;
        cart[itemIndex].JobControlTypeName = jobControlTypeName;
        return cart;
    }


    /// <summary>
    /// Removes the specified item from the cart
    /// </summary>
    /// <param name="itemID">The index position in the cart list that represents the item to remove</param>
    /// <returns>The current cart. An empty List of JobTypeControl is returned if empty</returns>
    public static List<JobTypeControlCookie> RemoveItem(int jobTypeControlID)
    {
        List<JobTypeControlCookie> cart = RetrieveCart();
        int itemIndex = cart.FindIndex(item => item.JobControlID == jobTypeControlID);

        cart.RemoveAt(itemIndex);
        return cart;
    }

    public static List<JobTypeControlCookie> RemoveItemByIndex(int index)
    {
        List<JobTypeControlCookie> cart = RetrieveCart();
        cart.RemoveAt(index);
        return cart;
    }
    #endregion

    #region Supporting Methods
    /// <summary>
    /// Destroys the current cart
    /// </summary>
    public static void DestroyCart()
    {
        HttpCookie cookie = new HttpCookie("JobTypeControlCart");
        cookie.Expires = DateTime.Today.AddDays(-30);
        HttpContext.Current.Response.Cookies.Add(cookie);
    }

    /// <summary>
    /// Checks to see whether the cart exists in the current session
    /// </summary>
    /// <returns>A boolean indicating whether the cookie exists in the current server request</returns>
    public static bool CookieExists()
    {
        return (HttpContext.Current.Request.Cookies["JobTypeControlCart"] != null);
    }
    #endregion
}