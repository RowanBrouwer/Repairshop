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
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Admin1";
                user.Email = "Admin1@localhost";
                user.FirstName = "Nancy";
                user.LastName = "Davolio";
                RepairGuy repairGuy = new RepairGuy();
                repairGuy.user = user;
                context.repairGuys.Add(repairGuy);

                IdentityResult result = userManager.CreateAsync
                (user, "!Admin111").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
            if (userManager.FindByEmail("Admin2@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Admin2";
                user.Email = "Admin2@localhost";
                user.FirstName = "Fancy";
                user.LastName = "Tavolio";
                RepairGuy repairGuy = new RepairGuy();
                repairGuy.user = user;
                context.repairGuys.Add(repairGuy);

                IdentityResult result = userManager.CreateAsync
                (user, "!Admin222").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            if (userManager.FindByEmail("cus1@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Cus1";
                user.Email = "cus1@localhost";
                user.FirstName = "Bob";
                user.LastName = "Builder";
                Customer customer = new Customer();
                customer.user = user;
                context.customers.Add(customer);

                IdentityResult result = userManager.CreateAsync
                (user, "!Cus111").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                }
            }

            if (userManager.FindByEmail("cus2@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Cus2";
                user.Email = "cus2@localhost";
                user.FirstName = "Tob";
                user.LastName = "Tuilder";
                Customer customer = new Customer();
                customer.user = user;
                context.customers.Add(customer);

                IdentityResult result = userManager.CreateAsync
                (user, "!Cus222").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                }
            }

            if (userManager.FindByEmail("Rep1@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Rep1";
                user.Email = "Rep1@localhost";
                user.FirstName = "Rowan";
                user.LastName = "Brouwer";
                RepairGuy repairGuy = new RepairGuy();
                repairGuy.user = user;
                context.repairGuys.Add(repairGuy);

                IdentityResult result = userManager.CreateAsync
                (user, "!Rep111").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Repairguy");
                }
            }

            if (userManager.FindByEmail("Rep2@localhost") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Rep2";
                user.Email = "Rep2@localhost";
                user.FirstName = "Luna";
                user.LastName = "Herder";
                RepairGuy repairGuy = new RepairGuy();
                repairGuy.user = user;
                context.repairGuys.Add(repairGuy);

                IdentityResult result = userManager.CreateAsync
                (user, "!Rep222").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Repairguy");
                }
            }
        }
    }
}
