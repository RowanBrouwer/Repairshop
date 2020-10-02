using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repairshop.Models
{
    public class part
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}