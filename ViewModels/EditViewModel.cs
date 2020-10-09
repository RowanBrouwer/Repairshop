using Repairshop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repairshop.ViewModels
{
    public class EditViewModel
    {
        public ApplicationUser user { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        public string City { get; set; }
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
    }
}