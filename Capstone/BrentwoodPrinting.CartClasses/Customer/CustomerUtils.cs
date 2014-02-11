using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentwoodPrinting.Data;
//Jack
namespace BrentwoodPrinting.CookieClasses.Customer
{
    public static class CustomerUtils
    {
        public static List<CustomerControlCookie> ToCookieClass(List<ListCustomers_Result> data)
        {
            List<CustomerControlCookie> returnData = new List<CustomerControlCookie>();

            foreach (ListCustomers_Result u in data)
                returnData.Add(new CustomerControlCookie(u.UserId, u.UserName, u.FirstName, u.LastName,
                    u.CustomerAddress, u.City, u.Province, u.PostalCode, u.PhoneNumber, u.Name,
                    (bool)u.Approved, u.LastActivityDate, u.Email));

            return returnData;
        }

        public static List<ListCustomers_Result> FromCookieClass(List<CustomerControlCookie> data)
        {
            List<ListCustomers_Result> returnData = new List<ListCustomers_Result>();
            ListCustomers_Result item = new ListCustomers_Result();

            foreach (CustomerControlCookie c in data)
            {
                item.UserId = c.UserId;
                item.UserName = c.UserName;
                item.FirstName = c.FirstName;
                item.LastName = c.LastName;
                item.CustomerAddress = c.CustomerAddress;
                item.City = c.City;
                item.Province = c.Province;
                item.PostalCode = c.PostalCode;
                item.PhoneNumber = c.PhoneNumber;
                item.Name = c.Name;
                item.Approved = c.Approved;
                item.LastActivityDate = c.LastActivityDate;
                item.Email = c.Email;

                returnData.Add(item);
            }

            return returnData;
        }
    }
}
