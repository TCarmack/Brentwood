using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BrentwoodPrinting.Data
{
    internal static class JobAssetAuthority
    {
        public static void ProcessJobAssets(List<string> assets, int jobID)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var connection = new Entities();

                foreach (string item in assets)
                    connection.JobAsset_Insert(item, jobID);

                scope.Complete();
            }
        }
    }
}
