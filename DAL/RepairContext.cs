using Microsoft.AspNet.Identity.EntityFramework;
using Repairshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Repairshop.DAL
{
    public class RepairContext : IdentityDbContext
    {
        public RepairContext() : base("DefaultConnection") 
        {
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<part> parts { get; set; }
        public DbSet<AmountParts> amountParts { get; set; }
        public DbSet<RepairGuy> repairGuys { get; set; }
        public DbSet<RepairOrder> repairOrders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}