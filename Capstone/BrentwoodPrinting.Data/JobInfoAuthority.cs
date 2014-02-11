using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
//Jack
namespace BrentwoodPrinting.Data
{
    internal static class JobInfoAuthority
    {
        public static void ProcessJobInfo(List<JobInfo> info, int JobID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var connection = new Entities();

                foreach (JobInfo item in info)
                    connection.JobInfo_Insert(item.NameKey, item.DataValue, JobID);

                scope.Complete();
            }
        }

        public static void UpdateJobInfo(List<JobInfo> info)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var connection = new Entities();

                foreach (JobInfo item in info)
                    connection.JobInfo_Update(item.JobInfoID, item.NameKey, item.DataValue, item.JobID);
                scope.Complete();
            }
        }
    }
}
