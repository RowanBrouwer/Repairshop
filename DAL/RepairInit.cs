using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repairshop.DAL
{
    public class RepairInit
    {
        public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RepairContext>
        {
            protected override void Seed(RepairContext context)
            {

            }
        }
    }
}