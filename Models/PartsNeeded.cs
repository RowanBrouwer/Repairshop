using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class PartsNeeded
    {
        [Key]
        public int Id { get; set; }
        public AmountPartsInStorage PartInStorage { get; set; }
        public part NeededPart { get; set; }
        public int AmountNeeded { get; set; }
    }
}