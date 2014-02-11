using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrentwoodPrinting.Data;
//Jack
namespace BrentwoodPrinting.CookieClasses.JobType
{
    /// <summary>
    /// Contains the methods for converting to and from the cookie cart class and the entity model class.
    /// </summary>
    public static class JobTypeUtils
    {
        /// <summary>
        /// Converts the specified list from an ADO.NET data entity list into a list of cookie class instances
        /// </summary>
        /// <param name="data">The ADO.NET Data Entity list to convert</param>
        /// <returns>A list of cookie classes</returns>
        public static List<JobTypeControlCookie> ConvertToCartClass(List<JobControl_Get_Result> data)
        {
            List<JobTypeControlCookie> items = new List<JobTypeControlCookie>();

            foreach (JobControl_Get_Result jc in data)
                items.Add(new JobTypeControlCookie(jc.JobControlID, jc.ControlName, jc.ControlTypeName));

            return items;
        }

        /// <summary>
        /// Converts the specified list from an a list of cookie class instances into a list of ADO.NET data entities
        /// </summary>
        /// <param name="data">The list of cookie classes to convert.</param>
        /// <returns>A list of data entities.</returns>
        public static List<JobControl> ConvertFromCartClass(List<JobTypeControlCookie> data)
        {
            List<JobControl> returnData = new List<JobControl>();
            JobControl item = new JobControl();

            foreach (JobTypeControlCookie c in data)
            {
                item = new JobControl();
                item.JobControlID = c.JobControlID;
                item.ControlName = c.ControlName;
                item.JobControlTypeID = Brentwood.GetJobControlTypeByName(c.JobControlTypeName).JobControlTypeID;

                returnData.Add(item);
            }

            return returnData;
        }

    }
}
