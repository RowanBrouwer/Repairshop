using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class Customer 
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser user { get; set; }
    }
}