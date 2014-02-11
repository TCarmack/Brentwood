using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Jack
namespace BrentwoodPrinting.CookieClasses.Customer
{
    /// <summary>
    /// The cart class for the aspnet_Users database table, as pertaining to customer accounts.
    /// </summary>
    [Serializable]
    public class CustomerControlCookie
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CustomerAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public bool Approved { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CustomerControlCookie()
        {
        }

        /// <summary>
        /// Greedy constructor.
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
        public CustomerControlCookie(Guid userid, string username, string firstname, string lastname,
            string customeraddress, string city, string province, string postalcode, string phonenumber,
            string name, bool approved, DateTime lastactivitydate, string email)
        {
            this.UserId = userid;
            this.UserName = username;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.CustomerAddress = customeraddress;
            this.City = city;
            this.Province = province;
            this.PostalCode = postalcode;
            this.PhoneNumber = phonenumber;
            this.Name = name;
            this.Approved = approved;
            this.LastActivityDate = lastactivitydate;
            this.Email = email;
        }
    }
}
