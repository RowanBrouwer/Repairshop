using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class RepairGuy
    {
        [Key]
        public int Id { get; set; }
        public string Complaints { get; set; }
        public ApplicationUser user { get; set; }   
        public IEnumerable<RepairOrder> RepairOrders { get; set; }
    }
}