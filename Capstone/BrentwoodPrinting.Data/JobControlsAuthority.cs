using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
//Jack
namespace BrentwoodPrinting.Data
{
    /// <summary>
    /// A class to handle the transaction of adding a list of job control objects
    /// </summary>
    internal class JobControlsAuthority
    {
        /// <summary>
        /// Inserts a list of job controls into the database and adds them to a specified job type.
        /// </summary>
        /// <param name="JobTypeID">An integer representing the ID of the job type to add the controls to.</param>
        /// <param name="data">The list of job controls to insert and add.</param>
        public static List<JobControl> ProcessJobControlsList(int JobTypeID, List<JobControl> data)
        {
            List<JobControl> returnArray = data;
            //The scope of the transaction
            using (TransactionScope scope = new TransactionScope())
            {
                var connection = new Entities();
                int? cid;
                int i = 0;
                
                //Insert and add
                foreach (JobControl jc in data)
                {
                    cid = connection.JobControl_Insert(jc.ControlName, jc.JobControlTypeID, JobTypeID).FirstOrDefault();
                    data[i].JobControlID = (int)cid;
                    i++;
                }

                //Complete transaction
                scope.Complete();
            }

            return returnArray;
        }

        /// <summary>
        /// Updates the list of job controls.
        /// </summary>
        /// <param name="data">The list of job control objects to update.</param>
        public static void ProcessJobControlsListUpdate(int jobTypeID, List<JobControl> data)
        {
            var connection = new Entities();
            List<JobControl_Get_Result> controls = connection.JobControl_ListByJobType(jobTypeID).ToList<JobControl_Get_Result>();

            using (TransactionScope scope = new TransactionScope())
            {
                foreach (JobControl r in data)
                    connection.JobControl_Delete(r.JobControlID);

                foreach(JobControl jc in data)
                    connection.JobControl_Insert(jc.ControlName, jc.JobControlTypeID, jobTypeID).FirstOrDefault();

                scope.Complete();
            }
        }
    }
}
