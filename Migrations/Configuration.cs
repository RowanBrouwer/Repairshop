namespace Repairshop.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Repairshop.Models;
    using Repairshop.Services;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Deployment.Internal;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    internal sealed class Configuration : DbMigrationsConfiguration<Repairshop.Models.ApplicationDbContext>
    {
        public Configuration()
        {

        }

        protected override void Seed(Repairshop.Models.ApplicationDbContext context)
        {

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(RoleNames.ROLE_ADMIN))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMIN));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_CUSTOMER))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_CUSTOMER));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_REPAIRGUY))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_REPAIRGUY));
            }

            if (userManager.FindByEmail("Admin1@localhost") == null)
            {
                IdentityResult result = userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "Admin1@localhost",
                        Email = "Admin1@localhost",
                        FirstName = "Nancy",
                        LastName = "Davolio"
                    }, "!Admin111").Result;



                context.repairGuys.Add(
                    new RepairGuy
                    {
                        user = userManager.FindByEmail("Admin1@localhost"),
                    });



                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail("Admin1@localhost").Id, "Repairguy");
                }
            }

            if (userManager.FindByEmail("Admin2@localhost") == null)
            {
                IdentityResult result = userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "Admin2@localhost",
                        Email = "Admin2@localhost",
                        FirstName = "Fancy",
                        LastName = "Tavolio"
                    }, "!Admin222").Result;



                context.repairGuys.Add(
                    new RepairGuy
                    {
                        user = userManager.FindByEmail("Admin2@localhost"),
                    });



                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail("Admin2@localhost").Id, "Repairguy");
                }
            }

            if (userManager.FindByEmail("cus1@localhost") == null)
            {
                IdentityResult result = userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "cus1@localhost",
                        Email = "cus1@localhost",
                        FirstName = "Bob",
                        LastName = "Builder"
                    }, "!Cus111").Result;



                context.customers.Add(
                    new Customer
                    {
                        user = userManager.FindByEmail("cus1@localhost"),
                    });



                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail("cus1@localhost").Id, "Customer");
                }
            }

            if (userManager.FindByEmail("cus2@localhost") == null)
            {
                IdentityResult result = userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "cus2@localhost",
                        Email = "cus2@localhost",
                        FirstName = "tob",
                        LastName = "tuilder"
                    }, "!Cus222").Result;



                context.customers.Add(
                    new Customer
                    {
                        user = userManager.FindByEmail("cus2@localhost"),
                    });



                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail("cus2@localhost").Id, "Customer");
                }
            }

            if (userManager.FindByEmail("Rep1@localhost") == null)
            {
                IdentityResult result = userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "Rep1@localhost",
                        Email = "Rep1@localhost",
                        FirstName = "Rowan",
                        LastName = "Brouwer"
                    }, "!Rep111").Result;



                context.repairGuys.Add(
                    new RepairGuy
                    {
                        user = userManager.FindByEmail("Rep1@localhost"),
                    });



                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail("Rep1@localhost").Id, "Repairguy");
                }
            }

            if (userManager.FindByEmail("Rep2@localhost") == null)
            {
                IdentityResult result = userManager.CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "Rep2@localhost",
                        Email = "Rep2@localhost",
                        FirstName = "Luna",
                        LastName = "Herder"
                    }, "!Rep222").Result;



                context.repairGuys.Add(
                    new RepairGuy
                    {
                        user = userManager.FindByEmail("Rep2@localhost"),
                    });



                if (result.Succeeded)
                {
                    userManager.AddToRole(userManager.FindByEmail("Rep2@localhost").Id, "Repairguy");
                }
            }


            if (context.parts.Count() < 1)
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

            if (context.amountParts.Count() < 1)
            {
                IList<AmountPartsInStorage> amountParts = new List<AmountPartsInStorage>();

                amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 4,
                    part = context.parts.FirstOrDefault(p => p.Name == "10900k" && p.Brand == "Intel") });

                amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 6,
                    part = context.parts.FirstOrDefault(p => p.Name == "Ryzen 7 3700x" && p.Brand == "AMD") });

                amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 7,
                    part = context.parts.FirstOrDefault(p => p.Name == "5700XT" && p.Brand == "AMD") });

                amountParts.Add(new AmountPartsInStorage() { AmountInStorage = 2,
                    part = context.parts.FirstOrDefault(p => p.Name == "RTX 3080" && p.Brand == "Nvidia") });

                foreach (AmountPartsInStorage amount in amountParts)
                {
                    context.amountParts.Add(amount);
                }
                base.Seed(context);
            }

            if (context.repairOrders.Count() < 1)
            {
                IList<RepairOrder> orders = new List<RepairOrder>();
                orders.Add(new RepairOrder()
                {
                    customer = context.customers.FirstOrDefault(p => p.user.Email == "cus2@localhost"),
                    repairGuy = context.repairGuys.FirstOrDefault(p => p.user.Email == "Rep2@localhost"),

                    status = Status.AwaitingParts,
                    Description = "waiting for CPU",

                    parts = context.partsNeeded.Add(new PartsNeeded()
                    {
                        AmountNeeded = 1,
                        NeededPart = context.parts.FirstOrDefault(p => p.Name == "Ryzen 7 3700x" && p.Brand == "AMD"),
                        PartInStorage = context.amountParts.FirstOrDefault(r => r.part == context.parts.FirstOrDefault(p => p.Name == "Ryzen 7 3700x" && p.Brand == "AMD"))
                    }),

                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)

                }) ;
                orders.Add(new RepairOrder()
                {
                    customer = context.customers.FirstOrDefault(p => p.user.Email == "cus1@localhost"),
                    repairGuy = context.repairGuys.FirstOrDefault(p => p.user.Email == "Rep2@localhost"),

                    status = Status.AwaitingParts,
                    Description = "waiting for GPU",

                    parts = context.partsNeeded.Add(new PartsNeeded() { AmountNeeded = 1,
                        NeededPart = context.parts.FirstOrDefault(p => p.Name == "5700XT" && p.Brand == "AMD"),
                        PartInStorage = context.amountParts.FirstOrDefault(r => r.part == context.parts.FirstOrDefault(p => p.Name == "5700XT" && p.Brand == "AMD"))
                    }),

                    StartDate = DateTime.Now,
                    EndDate = DateTime.Today.AddDays(5)
                });
                orders.Add(new RepairOrder()
                {
                    customer = context.customers.FirstOrDefault(p => p.user.Email == "cus1@localhost"),
                    repairGuy = context.repairGuys.FirstOrDefault(p => p.user.Email == "Rep1@localhost"),

                    status = Status.AwaitingParts,
                    Description = "waiting for CPU",

                    parts = context.partsNeeded.Add(new PartsNeeded() { AmountNeeded = 1,
                        NeededPart = context.parts.FirstOrDefault(p => p.Name == "10900k" && p.Brand == "Intel"),
                        PartInStorage = context.amountParts.FirstOrDefault(r => r.part == context.parts.FirstOrDefault(p => p.Name == "10900k" && p.Brand == "Intel"))
                    }),

                    StartDate = DateTime.Now,
                    EndDate = DateTime.Today.AddDays(4)
                });
                foreach (RepairOrder order in orders)
                {
                    context.repairOrders.Add(order);
                }
                base.Seed(context);
            }
        }
    }
}
