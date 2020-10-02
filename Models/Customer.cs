using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class Customer 
    {
        public int Id { get; set; }
        public ASL user { get; set; }
        public IEnumerable<RepairOrder> RepairOrders { get; set; }
    }
}