using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class PartsNeeded 
    {
        [Key]
        public int Id { get; set; }
        public AmountPartsInStorage inStorage { get; set; }
        public part PartNeeded { get; set; }
        public int AmountNeeded { get; set; }
    }
}