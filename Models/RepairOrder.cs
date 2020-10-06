using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class RepairOrder
    {
        [Key]
        public int Id { get; set; }
        public Customer customer  { get; set; }
        public RepairGuy repairGuy { get; set; }
        public Status status { get; set; }
        public IEnumerable<part> parts { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]      
        public DateTime EndDate { get; set; }

    }
}