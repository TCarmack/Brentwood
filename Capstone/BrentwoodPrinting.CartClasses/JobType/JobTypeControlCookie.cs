using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Jack
namespace BrentwoodPrinting.CookieClasses.JobType
{
    /// <summary>
    /// The cart class for the JobTypeControl database table
    /// </summary>
    [Serializable]
    public class JobTypeControlCookie
    {
        public int JobControlID { get; set; }
        public string ControlName { get; set; }
        public string JobControlTypeName { get; set; }
        public int InterimID { get; set; }

        /// <summary>
        /// Default contsructor
        /// </summary>
        public JobTypeControlCookie()
        {
        }

        /// <summary>
        /// Greedy constructor
        /// </summary>
        /// <param name="JobControlID">The ID of the job control</param>
        /// <param name="ControlName">The name of the job control</param>
        /// <param name="JobControlTypeID">The ID of the control type this control is</param>
        /// <param name="DefaultValues">Any default values that may be put in</param>
        public JobTypeControlCookie(int JobControlID, string ControlName, string JobControlTypeName)
        {
            this.JobControlID = JobControlID;
            this.ControlName = ControlName;
            this.JobControlTypeName = JobControlTypeName;
        }
    }
}
