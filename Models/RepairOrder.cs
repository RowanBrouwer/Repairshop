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
        public PartsNeeded parts { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

    }
}