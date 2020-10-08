using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using static Repairshop.Migrations.Configuration;

namespace Repairshop.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }
        public string City { get; set; }
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        public class MyDbContext : IdentityDbContext<ApplicationUser>
        {
            public MyDbContext() : base("DefaultConnection")
            {

            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<IdentityUser>().ToTable("Users");
                modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new RepairOrderInitializer<ApplicationDbContext>());
        }

        public DbSet<Customer> customers { get; set; }
        public DbSet<RepairGuy> repairGuys { get; set; }
        public DbSet<part> parts { get; set; }
        public DbSet<AmountPartsInStorage> amountParts { get; set; }
        public DbSet<RepairOrder> repairOrders { get; set; }
        public DbSet<PartsNeeded> partsNeeded { get; set; }

        private class RepairOrderInitializer<T> : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                if (context.parts == null)
                {
                    IList<part> parts = new List<part>();
                    parts.Add(new part() { Brand = "Intel", Name = "10900k", Type = "CPU", Price = 799.99 });
                    parts.Add(new part() { Brand = "AMD", Name = "Ryzen 7 3700x", Type = "CPU", Price = 349.99 });
                    parts.Add(new part() { Brand = "AMD", Name = "5700XT", Type = "GPU", Price = 399.99 });
                    parts.Add(new part() { Brand = "Nvidia", Name = "RTX 3080", Type = "GPU", Price = 399.99 });
                    foreach (part part in parts)
                    {
                        context.parts.Add(part);
                    }
                    base.Seed(context);
                }
                if (context.amountParts == null)
                {
                    IList<AmountPartsInStorage> amountParts = new List<AmountPartsInStorage>();
                    amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 4, part = context.parts.FirstOrDefault(p => p.Name == "10900k" && p.Brand == "Intel") });
                    amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 6, part = context.parts.FirstOrDefault(p => p.Name == "Ryzen 7 3700x" && p.Brand == "AMD") });
                    amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 7, part = context.parts.FirstOrDefault(p => p.Name == "5700XT" && p.Brand == "AMD") });
                    amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 2, part = context.parts.FirstOrDefault(p => p.Name == "RTX 3080" && p.Brand == "Nvidia") });
                    context.amountParts.AddRange(amountParts);
                    foreach (AmountPartsInStorage amount in amountParts)
                    {
                        context.amountParts.Add(amount);
                    }
                    base.Seed(context);
                }
                if (context.repairOrders == null)
                {
                    IList<RepairOrder> orders = new List<RepairOrder>();
                    orders.Add(new RepairOrder()
                    {
                        customer = context.customers.FirstOrDefault(p => p.user.Email == "cus2@localhost"),
                        repairGuy = context.repairGuys.FirstOrDefault(p => p.user.Email == "Rep2@localhost"),
                        status = Status.AwaitingParts,
                        Description = "waiting for CPU",
                        parts = context.partsNeeded.Add(new PartsNeeded() { AmountNeeded = 1, NeededPart = context.parts.FirstOrDefault(p => p.Name == "Ryzen 7 3700x" && p.Brand == "AMD"), PartInStorage = context.amountParts.FirstOrDefault(p => p.part.Name == "Ryzen 7 3700x" && p.part.Brand == "AMD") }),
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Today

                    });
                    orders.Add(new RepairOrder()
                    {
                        customer = context.customers.FirstOrDefault(p => p.user.Email == "cus1@localhost"),
                        repairGuy = context.repairGuys.FirstOrDefault(p => p.user.Email == "Rep2@localhost"),
                        status = Status.AwaitingParts,
                        Description = "waiting for GPU",
                        parts = context.partsNeeded.Add(new PartsNeeded() { AmountNeeded = 1, NeededPart = context.parts.FirstOrDefault(p => p.Name == "5700XT" && p.Brand == "AMD"), PartInStorage = context.amountParts.FirstOrDefault(p => p.part.Name == "5700XT" && p.part.Brand == "AMD") }),
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Today
                    });
                    orders.Add(new RepairOrder()
                    {
                        customer = context.customers.FirstOrDefault(p => p.user.Email == "cus1@localhost"),
                        repairGuy = context.repairGuys.FirstOrDefault(p => p.user.Email == "Rep1@localhost"),
                        status = Status.AwaitingParts,
                        Description = "waiting for CPU",
                        parts = context.partsNeeded.Add(new PartsNeeded() { AmountNeeded = 1, NeededPart = context.parts.FirstOrDefault(p => p.Name == "10900k" && p.Brand == "Intel"), PartInStorage = context.amountParts.FirstOrDefault(p => p.part.Name == "10900k" && p.part.Brand == "Intel") }),
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Today
                    });
                    foreach (RepairOrder order in orders)
                    {
                        context.repairOrders.Add(order);
                    }
                    base.Seed(context);
                }
            }

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}


