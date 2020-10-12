using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class AmountPartsInStorage
    {
        [Key]
        public int Id { get; set; }
        public part Part { get; set; }
        public int AmountInStorage { get; set; }
    }
}