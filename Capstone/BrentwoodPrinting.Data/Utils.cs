using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//Jack
namespace BrentwoodPrinting.Data
{
    public static class Utils
    {
        public static DateTime GetPromiseDate(int jobTypeID)
        {
            DateTime promiseDate = DateTime.Now;
            JobType jobType = Brentwood.GetJobType(jobTypeID);
            int days = GetDaysFromEstimatedTimeToComplete(jobType.EstimatedTimeToComplete);
            int hours = GetHoursFromEstimatedTimeToComplete(jobType.EstimatedTimeToComplete);
            promiseDate = promiseDate.AddDays(days);
            promiseDate = promiseDate.AddHours(hours);
            return promiseDate;
        }

        public static int GetHoursFromEstimatedTimeToComplete(string estimatedTime)
        {
            return int.Parse(estimatedTime.Substring(3, 2));
        }

        public static int GetDaysFromEstimatedTimeToComplete(string estimatedTime)
        {
            return int.Parse(estimatedTime.Substring(0, 2));
        }
    }
}
